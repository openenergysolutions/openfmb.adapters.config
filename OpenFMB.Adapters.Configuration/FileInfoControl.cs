// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class FileInfoControl : BaseDetailControl
    {
        public event EventHandler OnEditFileRequested;

        private readonly ConfigurationManager _configurationManager = ConfigurationManager.Instance;
        private static readonly ILogger _logger = MasterLogger.Instance;

        public DataNode DataNode
        {
            get; private set;
        }

        public FileInfoControl()
        {
            InitializeComponent();
        }

        public override object DataSource
        {
            get { return DataNode; }
            set
            {
                if (DataNode != value)
                {
                    DataNode = value as DataNode;

                    headerLabel.Text = Path.GetFileName(DataNode.Path);

                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            if (DataNode is FolderNode)
            {
                openButton.Text = "Explore";

                var info = new DirectoryInfo(DataNode.Path);
                nodeTypeTextBox.Text = "Folder";
                fullPathTextBox.Text = DataNode.Path;
                createdDateTextBox.Text = info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                lastModifiedTextBox.Text = info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                versionLabel.Visible = versionTextBox.Visible = false;
                editionLabel.Visible = editionTextBox.Visible = false;
                pluginLabel.Visible = plugInTypeTextBox.Visible = false;
            }
            else
            {
                var info = new FileInfo(DataNode.Path);

                var fileNode = DataNode as FileNode;


                nodeTypeTextBox.Text = fileNode.FileInformation.Id.ToString();

                fullPathTextBox.Text = DataNode.Path;
                createdDateTextBox.Text = info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                lastModifiedTextBox.Text = info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (fileNode.FileInformation.Id == ConfigFileType.MainAdapter)
                {
                    openButton.Text = "Edit";                    

                    versionLabel.Visible = versionTextBox.Visible = true;
                    editionLabel.Visible = editionTextBox.Visible = true;

                    versionTextBox.Text = fileNode.FileInformation.Version ?? fileNode.FileInformation.Version;
                    editionTextBox.Text = fileNode.FileInformation.Edition ?? fileNode.FileInformation.Edition;

                    pluginLabel.Visible = plugInTypeTextBox.Visible = false;
                }
                else if (fileNode.FileInformation.Id == ConfigFileType.Template)
                {
                    openButton.Text = "Edit";                    

                    versionLabel.Visible = versionTextBox.Visible = true;
                    editionLabel.Visible = editionTextBox.Visible = true;
                    pluginLabel.Visible = plugInTypeTextBox.Visible = true;

                    versionTextBox.Text = fileNode.FileInformation.Version ?? fileNode.FileInformation.Version;
                    editionTextBox.Text = fileNode.FileInformation.Edition ?? fileNode.FileInformation.Edition;
                    plugInTypeTextBox.Text = fileNode.FileInformation.Plugin ?? fileNode.FileInformation.Plugin;
                }
                else
                {
                    openButton.Text = "Open";
                    
                    versionLabel.Visible = versionTextBox.Visible = false;
                    editionLabel.Visible = editionTextBox.Visible = false;
                    pluginLabel.Visible = plugInTypeTextBox.Visible = false;
                }
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataNode is FolderNode)
                {
                    Process.Start(DataNode.Path);
                }
                else
                {
                    FileNode fileNode = DataNode as FileNode;
                    if (fileNode?.FileInformation.Id == ConfigFileType.MainAdapter || fileNode?.FileInformation.Id == ConfigFileType.Template)
                    {
                        OnEditFileRequested?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        Process.Start(DataNode.Path);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
                MessageBox.Show(this, ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
