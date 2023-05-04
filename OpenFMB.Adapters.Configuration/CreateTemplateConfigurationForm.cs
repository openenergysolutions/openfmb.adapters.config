// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Configuration.Properties;
using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateTemplateConfigurationForm : Form
    {
        private readonly AdapterConfiguration _config = new AdapterConfiguration();

        private readonly Dictionary<IPlugin, PluginOptions> _sessionFiles = new Dictionary<IPlugin, PluginOptions>();

        private readonly ConfigurationManager _configManager = ConfigurationManager.Instance;

        private readonly string _initialDirectory;

        private static readonly ILogger _logger = MasterLogger.Instance;

        public IEditable Output { get; private set; }

        public CreateTemplateConfigurationForm(string initialDirectory = null, bool canChooseDirectory = true)
        {
            InitializeComponent();

            _initialDirectory = initialDirectory;

            if (!canChooseDirectory)
            {
                folderTextBox.ReadOnly = true;
                browserButton.Visible = false;
            }

            var adapterFilePath = GetDefaultAdapterFilePath("template", _initialDirectory);
            folderTextBox.Text = Path.GetDirectoryName(adapterFilePath);
            nameTextBox.Text = Path.GetFileNameWithoutExtension(adapterFilePath);

            LoadTree(_config);

            pluginOptionControl.Visible = false;

            okButton.Enabled = false;
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
            List<Control> tempList = new List<Control>();
            foreach (var p in a.Plugins.Plugins)
            {
                if (p is ISessionable)
                {
                    RadioButton b = new RadioButton()
                    {
                        Text = p.Name,
                        Tag = p,
                        Dock = DockStyle.Top
                    };
                    b.BringToFront();
                    b.CheckedChanged += OnCheckedChanged;

                    placeHolder.Controls.Add(b);
                    tempList.Add(b);

                    var option = new PluginOptions
                    {
                        ModeSelectionEnabled = false
                    };

                    _sessionFiles[p] = option;
                }
            }

            foreach (var c in tempList)
            {
                c.BringToFront();
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderTextBox.Text = folderBrowserDialog.SelectedPath;
                Settings.Default.PreviousWorkingFolder = folderBrowserDialog.SelectedPath;
                Settings.Default.Save();

                var adapterFilePath = GetDefaultAdapterFilePath("template", folderBrowserDialog.SelectedPath);

                nameTextBox.Text = Path.GetFileName(adapterFilePath);
            }
        }

        private string GetFileName()
        {
            var adapterName = nameTextBox.Text;
            var extension = Path.GetExtension(adapterName).ToLower();
            if (!extension.EndsWith(".yml") && !extension.EndsWith(".yaml"))
            {
                adapterName += ".yaml";
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

                var templateName = GetFileName();
                var configPath = Path.Combine(folderTextBox.Text, templateName);
                if (File.Exists(configPath))
                {
                    MessageBox.Show("File with same name already exists", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool pluginSelected = false;

                foreach (RadioButton button in placeHolder.Controls)
                {
                    if (button.Checked)
                    {
                        pluginSelected = true;
                        var p = button.Tag as IPlugin;
                        var option = _sessionFiles[p];


                        Session session = new Session(p.Name, templateName, pluginOptionControl.SelectedVersion)
                        {
                            FullPath = configPath
                        };

                        if (option.SelectedProfiles.Count > 0)
                        {
                            foreach (var selected in option.SelectedProfiles)
                            {
                                Profile profile = Profile.Create(selected, p.Name, pluginOptionControl.SelectedVersion);
                                session.SessionConfiguration.AddProfile(profile);
                            }
                        }

                        _configManager.Save(session, false);

                        if (!_configManager.HasActiveWorkspace)
                        {
                            _configManager.LoadConfiguration(session);
                        }

                        Output = session;

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }

                if (!pluginSelected)
                {
                    MessageBox.Show(this, "Please select a plugin type", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Log(Level.Error, "An error occured when creating configurations.", ex);
            }
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;

            var options = _sessionFiles[button.Tag as IPlugin];
            pluginOptionControl.Options = options;
            pluginOptionControl.Visible = true;

            okButton.Enabled = true;
        }
    }
}