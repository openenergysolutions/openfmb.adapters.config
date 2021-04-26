// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Models;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SessionSettingsControl : UserControl, INotifyPropertyChanged
    {       
        public class SessionOverrides
        {
            [Category("Tag Overrides"), DisplayName("Override List")]
            public List<Override> Overrides { get; set; } 

            public SessionOverrides(List<Override> overrides)
            {
                Overrides = overrides;
            }
        }               

        private Session _session;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<FileChangedEventArgs> OnLocalFilePathChanged;

        public object DataSource
        {
            get { return _session; }
            set
            {
                Session s = value as Session;
                if (_session != s)
                {                                        
                    _session = s;                    
                    LoadData();
                    _session.PropertyChanged += Session_PropertyChanged;
                    _session.SessionConfiguration.PropertyChanged += Session_PropertyChanged;

                    headerLabel.Text = _session.Name?.ToUpper();
                }
            }
        }

        public TreeNode SelectedTreeNode { get; set; }

        public SessionSettingsControl()
        {
            InitializeComponent();            
        }

        private void Session_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

            headerLabel.Text = _session.Name;
            if (SelectedTreeNode != null && SelectedTreeNode.Text != _session.Name)
            {
                SelectedTreeNode.Text = _session.Name;
            }
        }

        private void LoadData()
        {
            if (_session != null)
            {
                splitContainer1.Panel1.Controls.Clear();

                if (_session.IsStandAlone)
                {                   
                    // protocol session specific settings
                    SessionSettingsItemControl settingsNode = new SessionSettingsItemControl("Network and Protocol Settings")
                    {
                        Tag = _session.SessionConfiguration.SessionSpecificConfig,
                        Dock = DockStyle.Top,
                        Editable = false
                    };

                    settingsNode.OnSelected += SessionNode_OnSelected;
                    splitContainer1.Panel1.Controls.Add(settingsNode);
                                       
                    settingsNode.BringToFront();
                    settingsNode.Selected = true;

                }
                else
                {
                    // session general settings (session name, path)
                    SessionSettingsItemControl sessionNode = new SessionSettingsItemControl("Session Name and File Path")
                    {
                        Tag = _session,
                        Dock = DockStyle.Top,
                        EditButtonText = "Change Local Path"
                    };
                    sessionNode.OnEditRequested += SessionNode_OnEditRequested;
                    sessionNode.OnSelected += SessionNode_OnSelected;
                    splitContainer1.Panel1.Controls.Add(sessionNode);

                    SessionSettingsItemControl overrideNode = new SessionSettingsItemControl("Tag Overrides")
                    {
                        Tag = new SessionOverrides(_session.Overrides),
                        Dock = DockStyle.Top,
                        EditButtonText = "Edit Tag Overrides",
                        Editable = false
                    };
                    
                    overrideNode.OnSelected += SessionNode_OnSelected;
                    splitContainer1.Panel1.Controls.Add(overrideNode);

                    // protocol session specific settings
                    SessionSettingsItemControl settingsNode = new SessionSettingsItemControl("Network and Protocol Settings")
                    {
                        Tag = _session.SessionConfiguration.SessionSpecificConfig,
                        Dock = DockStyle.Top,
                        Editable = false
                    };

                    if (_session.PluginName == PluginsSection.Dnp3Master)
                    {
                        var dnp3 = _session.SessionConfiguration.SessionSpecificConfig as Dnp3MasterSpecificConfig;
                        dnp3.Polls.CollectionChanged += Polls_CollectionChanged;                       
                    }
                    else if (_session.PluginName == PluginsSection.ModbusMaster)
                    {
                        var modbus = _session.SessionConfiguration.SessionSpecificConfig as ModbusMasterSpecificConfig;
                        modbus.HeartBeats.CollectionChanged += Polls_CollectionChanged;
                    }

                    settingsNode.OnSelected += SessionNode_OnSelected;
                    splitContainer1.Panel1.Controls.Add(settingsNode);

                   
                    sessionNode.BringToFront();
                    overrideNode.BringToFront();
                    settingsNode.BringToFront();

                    sessionNode.Selected = true;
                }
                
            }
        }

        private void Polls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void SessionNode_OnSelected(object sender, EventArgs e)
        {          

            foreach (var control in splitContainer1.Panel1.Controls)
            {
                var settings = control as SessionSettingsItemControl;
                if (settings != null)
                {
                    if (settings != sender)
                    {
                        settings.Selected = false;
                    }
                    else
                    {
                        propertyGrid.SelectedObject = settings.Tag;
                    }
                }
            }
        }

        private void SessionNode_OnEditRequested(object sender, EventArgs e)
        {
            var workingDir = ConfigurationManager.Instance.WorkingDirectory;

            if (string.IsNullOrWhiteSpace(openFileDialog.InitialDirectory))
            {
                openFileDialog.InitialDirectory = workingDir;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {                
                // Check if the selected file is within the working directory
                var file = openFileDialog.FileName;

                // check if it is a template file for this plugin
                var fileInformation = ConfigurationManager.GetFileInformation(file);

                if (fileInformation.Id != ConfigFileType.Template)
                {
                    MessageBox.Show($"'{file}' is not a template file.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (fileInformation.Plugin != _session.PluginName)
                {
                    MessageBox.Show($"'{file}' is not a template file for '{_session.PluginName}' plugin", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!workingDir.IsInFolder(file))
                {
                    SessionPathForm form = new SessionPathForm(_session);
                    if (form.ShowDialog() == DialogResult.OK)
                    {   
                        // need to copy file, set both source and dest files
                        OnLocalFilePathChanged?.Invoke(this, new FileChangedEventArgs(file, form.FileName));
                    }
                }
                else
                {
                    // no need to copy file, set file as destination
                    OnLocalFilePathChanged?.Invoke(this, new FileChangedEventArgs(null, file));                    
                }
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid.SelectedObject = e.Node.Tag;
        }

        private static string ShortenKey(Override o)
        {
            string key = o.Key.ToUpper();

            const string template0 = ".conductingEquipment.namedObject.name.value.value";
            const string template1 = ".conductingEquipment.mRID.value";

            if (o.Key.IndexOf(template0) > 0)
            {
                key = "NAMED OBJECT";
            }
            else if (o.Key.IndexOf(template1) > 0)
            {
                key = "MRID";
            }
            return key;
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.ChangedItem.Label));
        }
    }

    public class FileChangedEventArgs : EventArgs
    {
        public string SourceFilePath { get; private set; }
        public string DestFilePath { get; private set; }

        public FileChangedEventArgs(string filePath, string destFilePath) : base()
        {
            SourceFilePath = filePath;
            DestFilePath = destFilePath;
        }
    }
}
