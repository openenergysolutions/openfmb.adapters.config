/*
 * Copyright 2017-2020 Duke Energy Corporation and Open Energy Solutions, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using OpenFMB.Adapters.Configuration.Properties;
using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Goose;
using OpenFMB.Adapters.Core.Parsers;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class MainForm : Form
    {
        private readonly ConfigurationManager _configManger = ConfigurationManager.Instance;
        
        private StartPageControl _startPage;        

        private readonly static ILogger _logger = MasterLogger.Instance;        

        internal SplashScreen Splash
        {
            private get;
            set;
        }
        public MainForm()
        {
            InitializeComponent();
            Text = Program.AppName;

            InitSharedControl();
            Program.Mainform = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Splash.Hide();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Splash.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // check if save is needed for active editor
            HandleActiveEditorClosing(true);
        }

        public void CreateAdapterConfiguration()
        {
            if (_configManger.HasActiveWorkspace)
            {
                throw new InvalidOperationException("Can't create configuration this way!!!!");
            }

            // To create a new configuration with no active workspace
            CreateAdapterConfigurationForm form = new CreateAdapterConfigurationForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                CreateConfigurationControl();
            }
        }

        public void CreateTemplate()
        {
            if (_configManger.HasActiveWorkspace)
            {
                throw new InvalidOperationException("Can't create template this way!!!!");
            }

            CreateTemplateConfigurationForm form = new CreateTemplateConfigurationForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                CreateConfigurationControl();
            }
        }

        public void OpenConfigurationFolder()
        {            
            folderBrowserDialog.SelectedPath = Settings.Default.PreviousWorkingFolder;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                OpenConfigurationFolder(folderBrowserDialog.SelectedPath);
            }
        }

        public void OpenConfigurationFolder(string folderPath)
        {
            try
            {
                LoadConfigurations(folderPath);
                RecentFileManager.AddFile(folderPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                MessageBox.Show("Invalid configuration folder.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Settings.Default.PreviousWorkingFolder = folderPath;
                Settings.Default.Save();
            }
        }

        public void OpenSCL()
        {
            openFileDialog.Filter = "SCL files|*.scd|All files|*.*";
            openFileDialog.Title = "SCL File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenSCL(openFileDialog.FileName);
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close workspace, not existing
            HandleActiveEditorClosing(false);
        }

        private void HandleActiveEditorClosing(bool exiting = false)
        {
            var top = FindFrontMost();

            if (top is SCLViewerControl)
            {
                placeHolder.Controls.Remove(top);
                RemoveFromWindowMenu(top);
            }
            else if (top is ConfigurationControl)
            {
                // Save

                var button = exiting? MessageBoxButtons.YesNo : MessageBoxButtons.YesNoCancel;

                if (_configManger.HasChanged())
                {
                    var result = MessageBox.Show("Do you want to save changes to your configuration files?\n\nYour changes will be lost if you don't save.", Program.AppName, button, MessageBoxIcon.Question);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            _configManger.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occured when saving the files.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _logger.Log(Level.Error, "An error occured when saving the files.", ex);
                        }
                    }
                }

                _configManger.CloseWorkspace();

                placeHolder.Controls.Remove(top);
                RemoveFromWindowMenu(top);
            }
        }

        private void CreateGUIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateGuidForm form = new CreateGuidForm();
            form.Show();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            newToolStripMenuItem.Visible = openToolStripMenuItem.Enabled = startPageStripMenuItem.Enabled = string.IsNullOrEmpty(_configManger.WorkingDirectory);
            
            var top = FindFrontMost();

            if (top is SCLViewerControl)
            {
                closeToolStripMenuItem.Text = $"Close \"{(top as SCLViewerControl).Caption}\"";
                closeToolStripMenuItem.Enabled = true;
            }
            else if (top is ConfigurationControl)
            {                
                closeToolStripMenuItem.Text = $"Close Work Folder";
                closeToolStripMenuItem.Enabled = true;
            }
            else
            {
                closeToolStripMenuItem.Text = $"Close";
                closeToolStripMenuItem.Enabled = false;
            }
            saveToolStripMenuItem.Enabled = _configManger.ActiveConfiguration != null;
        }

        private Control FindFrontMost()
        {
            foreach (Control c in placeHolder.Controls)
            {
                if (placeHolder.Controls.GetChildIndex(c) == 0)
                {
                    return c;
                }
            }
            return null;
        }

        private void InitSharedControl()
        {
            _startPage = new StartPageControl();
            _startPage.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(_startPage);
        }

        private void LoadConfigurations(string selectDirectory)
        {
            _configManger.OpenWorkspace(selectDirectory);
            CreateConfigurationControl();
        }

        private void CreateConfigurationControl()
        {
            ConfigurationControl control = new ConfigurationControl();
            control.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(control);
            control.BringToFront();

            AddToWindowMenu(control);
        }

        private void LoadIeds(List<IED> ieds, string fileName)
        {            
            var sclViewControl = new SCLViewerControl(fileName);
            sclViewControl.Dock = DockStyle.Fill;
            sclViewControl.LoadIeds(ieds);
            placeHolder.Controls.Add(sclViewControl);

            sclViewControl.BringToFront();

            AddToWindowMenu(sclViewControl);
           
        }

        private void AddToWindowMenu(IWindowViewControl control)
        {
            var item = new System.Windows.Forms.ToolStripMenuItem()
            {
                Name = control.Caption,
                Text = control.Caption,
                Checked = true,
                Tag = control
            };
            item.Click += (sender, args) => { (control as Control).BringToFront(); };

            windowToolStripMenuItem.DropDownItems.Add(item);
        }

        private void RemoveFromWindowMenu(Control control)
        {
            foreach (ToolStripItem item in windowToolStripMenuItem.DropDownItems)
            {
                if (item.Tag == control)
                {
                    windowToolStripMenuItem.DropDownItems.Remove(item);
                    break;
                }
            }
        }

        private void OpenAdapterConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenConfigurationFolder();
        }

        private void OpenSCL(string filePath)
        {
            var ieds = SCDParser.Parse(filePath);

            LoadIeds(ieds, filePath);
        }

        private void OpenSCLFileStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSCL();
        }        
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                saveToolStripMenuItem.Enabled = false;
                _configManger.Save();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured when saving the files.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Log(Level.Error, "An error occured when saving the files.", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void StartPageStripMenuItem_Click(object sender, EventArgs e)
        {
            _startPage.BringToFront();
        }

        private void CreateAdapterConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateAdapterConfiguration();
        }

        private void TemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTemplate();
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm form = new OptionsForm();
            form.ShowDialog();
        }

        private void TagsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagManagementForm form = new TagManagementForm();
            form.ShowDialog();
        }

        private void GenerateMappingTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMappingCSVForm form = new CreateMappingCSVForm();
            form.ShowDialog();
        }        
    }
}