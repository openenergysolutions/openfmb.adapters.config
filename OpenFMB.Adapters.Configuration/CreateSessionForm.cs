// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility;
using System;
using System.IO;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateSessionForm : Form
    {
        private enum Action
        {
            CreateNew,
            ImportTemplate,
            SelectTemplateInWorkspace
        }

        private readonly ISessionable _plugin;

        private readonly ConfigurationManager _configurationManager = ConfigurationManager.Instance;

        private string _fileToImport;

        private Action _action = Action.CreateNew;

        public Session Output { get; private set; }

        public CreateSessionForm()
        {
            InitializeComponent();            
        }

        public CreateSessionForm(ISessionable plugin) : this()
        {
            _plugin = plugin;
            var folder = Path.Combine(_configurationManager.WorkingDirectory, _plugin.Name);
            var file = folder.GetNextConfigFileName(plugin.Name + "-template");
            file = Path.Combine(_plugin.Name, Path.GetFileName(file));

            templateFileName.Text = FileHelper.ConvertToForwardSlash(file);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(templateFileName.Text.Trim()))
            {
                MessageBox.Show("Please specify file name for this session.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);               
            }
            else
            {
                var fileName = Path.Combine(_configurationManager.WorkingDirectory, templateFileName.Text.Trim());

                switch (_action)
                {
                    case Action.CreateNew:
                        {
                            if (File.Exists(fileName))
                            {
                                var result = MessageBox.Show($"'{fileName}' already exist.  Replace it?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (result == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            
                            Session session = new Session(_plugin.Name, FileHelper.ConvertToForwardSlash(templateFileName.Text.Trim())); // Relative path               
                            session.Name = string.IsNullOrWhiteSpace(namedTextBox.Text.Trim()) ? "Session" : namedTextBox.Text.Trim();
                            _plugin.Sessions.Add(session);
                            session.Index = _plugin.Sessions.Count - 1;
                            Output = session;

                            try
                            {
                                _configurationManager.SuspendFileWatcher();                                
                                session.SessionConfiguration.Save(session.FullPath);
                            }
                            finally
                            {
                                _configurationManager.ResumeFileWatcher();
                            }                            
                        }
                        break;
                    case Action.ImportTemplate:
                    case Action.SelectTemplateInWorkspace:
                        {
                            if (!string.IsNullOrWhiteSpace(_fileToImport))
                            {
                                _configurationManager.CopyFile(_fileToImport, fileName);                                
                            }

                            var relative = FileHelper.MakeRelativePath(_configurationManager.WorkingDirectory, fileName);
                            Session session = Session.FromFile(_configurationManager.WorkingDirectory, relative);
                            session.Name = string.IsNullOrWhiteSpace(namedTextBox.Text.Trim()) ? "Session" : namedTextBox.Text.Trim();
                            _plugin.Sessions.Add(session);
                            session.Index = _plugin.Sessions.Count - 1;
                            Output = session;
                        }
                        break;
                    
                }
                
                DialogResult = DialogResult.OK;
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var workingDir = _configurationManager.WorkingDirectory;

            if (string.IsNullOrWhiteSpace(openFileDialog.InitialDirectory))
            {
                openFileDialog.InitialDirectory = workingDir;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                copyLabel.Visible = false;

                // Check if the selected file is within the working directory
                var file = openFileDialog.FileName;

                // check if it is a template file for this plugin
                var fileInformation = ConfigurationManager.GetFileInformation(file);

                if (fileInformation.Id != ConfigFileType.Template)
                {
                    MessageBox.Show($"'{file}' is not a template file.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (fileInformation.Plugin != _plugin.Name)
                {
                    MessageBox.Show($"'{file}' is not a template file for '{_plugin.Name}' plugin", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!workingDir.IsInFolder(file))
                {
                    _fileToImport = file;

                    var fileName = Path.GetFileNameWithoutExtension(file);

                    var temp = FileHelper.ConvertToForwardSlash(_plugin.Name.GetNextConfigFileName(fileName));

                    templateFileName.Text = temp;

                    copyLabel.Visible = true;
                    copyLabel.Text = $"Copy file '{fileName}' to work folder?";

                    _action = Action.ImportTemplate;
                }
                else
                {
                    var relative = FileHelper.MakeRelativePath(workingDir, file);
                    templateFileName.Text = FileHelper.ConvertToForwardSlash(relative);

                    _action = Action.SelectTemplateInWorkspace;
                }
            }
        }
    }
}