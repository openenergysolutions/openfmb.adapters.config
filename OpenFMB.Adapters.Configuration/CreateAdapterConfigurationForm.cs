// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Configuration.Properties;
using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Parsers;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateAdapterConfigurationForm : Form
    {
        private readonly AdapterConfiguration _config = new AdapterConfiguration();

        private readonly Dictionary<IPlugin, PluginOptions> _sessionFiles = new Dictionary<IPlugin, PluginOptions>();

        private readonly ConfigurationManager _configManager = ConfigurationManager.Instance;

        private readonly string _initialDirectory;

        private static readonly ILogger _logger = MasterLogger.Instance;

        public Editable Output { get; private set; }

        public CreateAdapterConfigurationForm(string initialDirectory = null, bool canChooseDirectory = true)
        {
            InitializeComponent();

            _initialDirectory = initialDirectory;
            
            if (!canChooseDirectory)
            {
                folderTextBox.ReadOnly = true;
                browserButton.Visible = false;
            }

            var adapterFilePath = GetDefaultAdapterFilePath("adapter", _initialDirectory);
            folderTextBox.Text = Path.GetDirectoryName(adapterFilePath);
            nameTextBox.Text = Path.GetFileNameWithoutExtension(adapterFilePath);

            LoadTree(_config);
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            okButton.Enabled = nameTextBox.Text.Trim().Length > 0;
        }

        private string GetDefaultAdapterFilePath(string seed, string initialFolder)
        {
            var folder = !string.IsNullOrWhiteSpace(initialFolder) ? initialFolder : Settings.Default.PreviousWorkingFolder;

            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Program.AppName);
                Directory.CreateDirectory(folder);
                Settings.Default.PreviousWorkingFolder = folder;
                Settings.Default.Save();
            }

            string adapter = seed;
            int i = 1;
            while (true)
            {                
                if (!File.Exists(Path.Combine(folder, adapter + ".yml")) && !File.Exists(Path.Combine(folder, adapter + ".yaml")))
                {
                    break;
                }
                else
                {
                    adapter = $"{seed}{i++}";
                }
            }

            return Path.Combine(folder, $"{adapter}.yaml");
        }

        private void LoadTree(AdapterConfiguration a)
        {            
            TreeNode node = new TreeNode("adapter");
            node.Tag = a;

            treeView.Nodes.Add(node);

            TreeNode logging = new TreeNode(a.Logging.Name)
            {
                Tag = a.Logging
            };
            node.Nodes.Add(logging);

            TreeNode plugins = new TreeNode(a.Plugins.Name)
            {
                Tag = a.Plugins
            };
            node.Nodes.Add(plugins);

            foreach (var p in a.Plugins.Plugins)
            {
                if (p is ISessionable)
                {
                    var pNode = new TreeNode(p.Name)
                    {
                        Tag = p
                    };

                    var option = new PluginOptions();

                    if (p.Name.StartsWith("goose"))
                    {
                        option.ModeSelectionEnabled = false;
                    }

                    _sessionFiles[p] = option;

                    plugins.Nodes.Add(pNode);                    
                }
                else
                {
                    var pNode = new TreeNode(p.Name)
                    {
                        Tag = p,
                        ForeColor = Color.Gray
                    };
                    plugins.Nodes.Add(pNode);
                }
            }

            treeView.ExpandAll();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderTextBox.Text = folderBrowserDialog.SelectedPath;
                Settings.Default.PreviousWorkingFolder = folderBrowserDialog.SelectedPath;
                Settings.Default.Save();                

                var adapterFilePath = GetDefaultAdapterFilePath("adapter", folderBrowserDialog.SelectedPath);

                nameTextBox.Text = Path.GetFileName(adapterFilePath);
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is ISessionable)
            {                        
                var options = _sessionFiles[e.Node.Tag as IPlugin];
                pluginOptionControl.Options = options;
                pluginOptionControl.Visible = true;
            }
            else
            {
                pluginOptionControl.Visible = false;
            }
        }

        private string GetAdapterName()
        {
            var adapterName = nameTextBox.Text;
            var extension = Path.GetExtension(adapterName).ToLower();
            if (!extension.EndsWith(".yml") && !extension.EndsWith(".yaml"))
            {
                adapterName = adapterName + ".yaml";
            }
            return adapterName;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(folderTextBox.Text))
                {
                    Directory.CreateDirectory(folderTextBox.Text);

                    Settings.Default.PreviousWorkingFolder = folderTextBox.Text;
                    Settings.Default.Save();
                }

                var configPath = Path.Combine(folderTextBox.Text, GetAdapterName());
                if (File.Exists(configPath))
                {
                    MessageBox.Show("File with same name already exists", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _config.FullPath = configPath;                

                foreach (var plugin in _config.Plugins.Plugins)
                {
                    var p = plugin as ISessionable;
                    if (p != null)
                    {                      
                        var option = _sessionFiles[p];                        

                        if (option.Mode == ProfileCreateMode.LoadFromFile)
                        {                            
                            if (!string.IsNullOrEmpty(option.LoadFromFile))
                            {
                                // base folder: where the main adapter is
                                var folder = Path.GetDirectoryName(configPath);

                                // plugin folder: base + plugin-name
                                var pluginFolder = Path.Combine(folder, p.Name);

                                // the default template file:  search in plugin folder
                                var filePath = FileHelper.ConvertToForwardSlash(pluginFolder.GetNextConfigFileName(p.Name + "-template"));

                                // relative path to base folder
                                filePath = Path.Combine(p.Name, Path.GetFileName(filePath));

                                // local path of the session file: base + relative path
                                var fullPath = Path.Combine(folder, filePath);

                                Session session = new Session(p.Name, filePath);
                                session.FullPath = fullPath;
                                p.Sessions.Add(session);

                                try
                                {
                                    var profile = CSVParser.Parse(p.Name, option.LoadFromFile);

                                    string mrid = session.LocalFilePath;
                                    var row = profile.CsvData.FirstOrDefault(x => x.Path.EndsWith("mRID"));
                                    if (row != null)
                                    {
                                        mrid = row.Value;
                                    }

                                    session.SessionConfiguration.AddProfile(profile);
                                }
                                catch (Exception)
                                {
                                    p.Sessions.Remove(session);
                                    throw;
                                }
                            }
                        }                        
                        else // select profiles
                        {
                            if (option.SelectedProfiles.Count > 0)
                            {
                                // base folder: where the main adapter is
                                var folder = Path.GetDirectoryName(configPath);

                                // plugin folder: base + plugin-name
                                var pluginFolder = Path.Combine(folder, p.Name);

                                // the default template file:  search in plugin folder
                                var filePath = FileHelper.ConvertToForwardSlash(pluginFolder.GetNextConfigFileName(p.Name + "-template"));

                                // relative path to base folder
                                filePath = Path.Combine(p.Name, Path.GetFileName(filePath));

                                // local path of the session file: base + relative path
                                var fullPath = Path.Combine(folder, filePath);

                                Session session = new Session(p.Name, filePath);
                                session.FullPath = fullPath;
                                p.Sessions.Add(session);

                                try
                                {
                                    foreach (var selected in option.SelectedProfiles)
                                    {
                                        Profile profile = Profile.Create(selected, p.Name);
                                        session.SessionConfiguration.AddProfile(profile);
                                    }
                                }
                                catch (Exception)
                                {
                                    p.Sessions.Remove(session);
                                    throw;
                                }
                            }
                        }                        
                    }
                }
                                
                _configManager.Save(_config, false);

                if (!_configManager.HasActiveWorkspace)
                {
                    _configManager.LoadConfiguration(_config);
                }

                Output = _config;

                DialogResult = DialogResult.OK;
                Close(); 
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _logger.Log(Level.Error, "An error occured when creating configurations.", ex);
            }
        }
    }

    public enum ProfileCreateMode
    {
        SelectedProfiles,
        LoadFromFile,
        LoadFromFolder
    }

    public class PluginOptions
    {
        public bool ModeSelectionEnabled { get; set; } = true;

        public ProfileCreateMode Mode { get; set; }
        public List<string> SelectedProfiles { get; } = new List<string>();
        public string LoadFromFile { get; set; }
        public string LoadFromFolder { get; set; }
    }
}