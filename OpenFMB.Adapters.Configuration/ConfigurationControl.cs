// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Logging;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Parsers;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class ConfigurationControl : UserControl, IWindowViewControl
    {
        private readonly string LoggingSectionKey = "LoggingSectionControl";
        private readonly string PluginsSectionKey = "PluginsSectionControl";
        private readonly string PluginKey = "PluginControl";
        private readonly string SessionSettingsControlKey = "SessionSettingsControl";
        private readonly string ProfileSettingsControlKey = "ProfileSettingsControl";
        private readonly string AdapterConfigurationKey = "AdapterConfiguration";
        private readonly string FileInfoControlKey = "FileInfoControl";

        private readonly ConfigurationManager _configurationManager = ConfigurationManager.Instance;

        private const int FolderIcon = 3;

        private readonly ILogger _logger = MasterLogger.Instance;

        private DataNode _copiedDataNode;

        public string Caption
        {
            get { return _configurationManager.WorkingDirectory.TruncateFile(); }
        }

        public string WorkspaceDir
        {
            get { return _configurationManager.WorkingDirectory; }
        }

        public ConfigurationControl()
        {
            InitializeComponent();
            toolTip.SetToolTip(expandButton, "Hide/Show Navigation Pane");
            toolTip.SetToolTip(logShowHideButton, "Hide/Show Log Pane");
            toolTip.SetToolTip(saveAdapterButton, "Save");
            toolTip.SetToolTip(closeAdapterButton, "Close");
            toolTip.SetToolTip(newConfigButton, "New Adapter Configuration");
            toolTip.SetToolTip(newTemplateButton, "New Adapter Configuration");

            _configurationManager.OnConfigurationSaved += OnConfigurationSaved;

            LoadFile(_configurationManager.ActiveConfiguration);
            LoadWorkspaceTree();

            if (adapterTreeView.Nodes.Count > 0)
            {
                tabControl.SelectedIndex = 1;
            }
            else
            {
                tabControl.SelectedIndex = 0;
            }

            _configurationManager.OnFileSystemChanged += FileSystemWatcher_Changed;

            _configurationManager.OnFileSystemDeleted += FileSystemWatcher_DeletedOrRenamed;
            _configurationManager.OnFileSystemRenamed += FileSystemWatcher_DeletedOrRenamed;

            _configurationManager.OnFileSystemCreated += FileSystemWatcher_Created;


            // Hide logs
            detailSplitContainer.Panel2Collapsed = true;
        }

        private void LoadFile(IEditable editable)
        {
            if (editable is AdapterConfiguration)
            {
                LoadFile(editable as AdapterConfiguration);
            }
            else
            {
                LoadFile(editable as Session);
            }
        }

        private void LoadFile(AdapterConfiguration adapterConfiguration)
        {
            adapterTreeView.Nodes.Clear();

            activeEditorPanel.Visible = false;

            if (adapterConfiguration == null)
            {
                return;
            }

            activeFileNameLabel.Text = adapterConfiguration.FullPath;
            activeFileTypeLabel.Text = "Type: Adapter Config";

            var adapterName = string.IsNullOrEmpty(adapterConfiguration.FullPath) ? "adapter" : Path.GetFileNameWithoutExtension(adapterConfiguration.FullPath);
            TreeNode node = new TreeNode(adapterName)
            {
                ToolTipText = adapterConfiguration.FullPath,
                Tag = adapterConfiguration
            };

            adapterTreeView.Nodes.Add(node);

            TreeNode logging = new TreeNode(adapterConfiguration.Logging.Name)
            {
                Tag = adapterConfiguration.Logging
            };
            node.Nodes.Add(logging);

            TreeNode plugins = new TreeNode(adapterConfiguration.Plugins.Name)
            {
                Tag = adapterConfiguration.Plugins
            };
            node.Nodes.Add(plugins);

            foreach (var p in adapterConfiguration.Plugins.Plugins)
            {
                var pNode = new TreeNode(p.Name)
                {
                    Tag = p
                };
                plugins.Nodes.Add(pNode);

                if (p is ISessionable)
                {
                    foreach (var session in (p as ISessionable).Sessions)
                    {
                        var sNode = new TreeNode(session.Name)
                        {
                            Tag = session
                        };
                        pNode.Nodes.Add(sNode);

                        foreach (var profile in session.SessionConfiguration?.GetProfiles())
                        {
                            var profileNode = new ProfileTreeNode(profile)
                            {
                                Tag = profile
                            };
                            sNode.Nodes.Add(profileNode);
                        }
                    }
                }
            }

            adapterTreeView.ExpandAll();

            if (adapterTreeView.Nodes.Count > 0)
            {
                activeEditorLink.Text = Path.GetFileName(adapterConfiguration.FullPath);

                toolTip.SetToolTip(activeEditorLink, $"{adapterConfiguration.FullPath}");
                activeEditorLink.Tag = new FileNode
                {
                    Path = adapterConfiguration.FullPath,
                    FileInformation = ConfigurationManager.GetFileInformation(adapterConfiguration.FullPath),
                };
                activeEditorPanel.Visible = true;

                // enable save and close buttons
                saveAdapterButton.Enabled = closeAdapterButton.Enabled = true;
            }
        }

        private void LoadFile(Session session)
        {
            adapterTreeView.Nodes.Clear();

            activeEditorPanel.Visible = false;

            if (session == null)
            {
                return;
            }

            activeFileNameLabel.Text = session.FullPath;
            activeFileTypeLabel.Text = "Type: Device Template";

            session.IsStandAlone = true;

            var adapterName = session.LocalFilePath;
            TreeNode node = new TreeNode(adapterName)
            {
                ToolTipText = session.FullPath,
                Tag = session
            };

            adapterTreeView.Nodes.Add(node);

            foreach (var profile in session.SessionConfiguration?.GetProfiles())
            {
                var profileNode = new ProfileTreeNode(profile)
                {
                    Tag = profile
                };
                node.Nodes.Add(profileNode);
            }

            adapterTreeView.ExpandAll();

            if (adapterTreeView.Nodes.Count > 0)
            {
                activeEditorLink.Text = Path.GetFileName(session.FullPath);
                toolTip.SetToolTip(activeEditorLink, $"{session.FullPath}");
                activeEditorLink.Tag = new FileNode
                {
                    Path = session.FullPath,
                    FileInformation = ConfigurationManager.GetFileInformation(session.FullPath),
                };

                activeEditorPanel.Visible = true;

                // enable save and close buttons
                saveAdapterButton.Enabled = closeAdapterButton.Enabled = true;
            }
        }

        private Control FindControl(string key)
        {
            var controls = placeHolder.Controls.Find(key, true);
            return controls != null && controls.Length > 0 ? controls[0] : null;
        }

        private Control FindSessionControl(string key, object session)
        {
            var controls = placeHolder.Controls.Find(key, true);
            if (controls != null)
            {
                foreach (Control c in controls)
                {
                    if (c is SessionSettingsControl sc && sc.DataSource == session)
                    {
                        return sc;
                    }
                }
            }
            return null;
        }

        private Control FindProfileControl(string key, object profile)
        {
            var controls = placeHolder.Controls.Find(key, true);
            if (controls != null)
            {
                foreach (Control c in controls)
                {
                    if (c is ProfileTreeControl sc && sc.DataSource == profile)
                    {
                        return sc;
                    }
                }
            }
            return null;
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is LoggingSection)
            {
                if (!(FindControl(LoggingSectionKey) is LoggingSectionControl c))
                {
                    c = new LoggingSectionControl()
                    {
                        Name = LoggingSectionKey,
                        Dock = DockStyle.Fill
                    };
                    c.PropertyChanged += OnPropertyChanged;
                    placeHolder.Controls.Add(c);
                }
                c.DataSource = e.Node.Tag;
                c.BringToFront();
            }
            else if (e.Node.Tag is PluginsSection)
            {
                if (!(FindControl(PluginsSectionKey) is PluginsSectionControl c))
                {
                    c = new PluginsSectionControl()
                    {
                        Name = PluginsSectionKey,
                        Dock = DockStyle.Fill
                    };
                    c.PropertyChanged += OnPropertyChanged;
                    c.OnPluginSelected += OnPluginSelected;
                    placeHolder.Controls.Add(c);
                }
                c.DataSource = e.Node.Tag;
                c.BringToFront();
            }
            else if (e.Node.Tag is IPlugin)
            {
                var pluginType = e.Node.Tag.GetType().Name + "Control";

                BaseDetailControl c = FindControl(pluginType) as BaseDetailControl;
                if (c == null)
                {
                    Type type = Type.GetType("OpenFMB.Adapters.Configuration." + pluginType);
                    if (type != null)
                    {
                        var temp = Activator.CreateInstance(type);
                        c = Activator.CreateInstance(type) as BaseDetailControl;
                        c.Name = pluginType;
                        c.Dock = DockStyle.Fill;
                        c.PropertyChanged += OnPropertyChanged;
                        placeHolder.Controls.Add(c);
                    }
                }
                if (c == null)
                {
                    c = FindControl(PluginKey) as BaseDetailControl;
                    if (c == null)
                    {
                        c = new PluginControl()
                        {
                            Name = PluginKey,
                            Dock = DockStyle.Fill
                        };
                        c.PropertyChanged += OnPropertyChanged;
                        placeHolder.Controls.Add(c);
                    }
                }
                c.DataSource = e.Node.Tag;
                c.BringToFront();
            }
            else if (e.Node.Tag is Session)
            {
                if (!(FindSessionControl(SessionSettingsControlKey, e.Node.Tag) is SessionSettingsControl c))
                {
                    c = new SessionSettingsControl
                    {
                        Name = SessionSettingsControlKey,
                        Dock = DockStyle.Fill,
                        DataSource = e.Node.Tag,
                        SelectedTreeNode = e.Node
                    };
                    c.PropertyChanged += OnPropertyChanged;
                    c.OnLocalFilePathChanged += OnSessionLocalFilePathChanged;
                    placeHolder.Controls.Add(c);
                }
                c.BringToFront();
            }
            else if (e.Node.Tag is Profile)
            {
                if (!(FindProfileControl(ProfileSettingsControlKey, e.Node.Tag) is ProfileTreeControl c))
                {
                    c = new ProfileTreeControl
                    {
                        Name = ProfileSettingsControlKey,
                        Dock = DockStyle.Fill,
                        DataSource = e.Node.Tag
                    };
                    c.PropertyChanged += OnPropertyChanged;
                    placeHolder.Controls.Add(c);
                }
                c.BringToFront();
            }
            else
            {
                if (!(FindControl(AdapterConfigurationKey) is AdapterConfigurationDetailControl c))
                {
                    c = new AdapterConfigurationDetailControl()
                    {
                        Name = AdapterConfigurationKey,
                        Dock = DockStyle.Fill
                    };
                    c.PropertyChanged += OnPropertyChanged;
                    placeHolder.Controls.Add(c);
                }

                c.DataSource = e.Node.Tag;
                c.BringToFront();
            }
        }

        private void OnSessionLocalFilePathChanged(object sender, FileChangedEventArgs e)
        {
            // Check if session is dirty
            SessionSettingsControl control = sender as SessionSettingsControl;
            var session = control.DataSource as Session;

            if (session != null)
            {
                var result = MessageBox.Show(this, $"You are about apply new session file.{Environment.NewLine}Click YES to proceed.", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                adapterTreeView.BeginUpdate();

                // update 
                if (!string.IsNullOrWhiteSpace(e.SourceFilePath))
                {
                    // need to copy to working dir
                    _configurationManager.CopyFile(e.SourceFilePath, e.DestFilePath);
                }

                var relative = FileHelper.MakeRelativePath(ConfigurationManager.Instance.WorkingDirectory, e.DestFilePath);
                relative = FileHelper.ConvertToForwardSlash(relative);

                session.Reload(_configurationManager.WorkingDirectory, relative);

                TreeNode node = control.SelectedTreeNode;
                node.Nodes.Clear();

                foreach (var profile in session.SessionConfiguration?.GetProfiles())
                {
                    var profileNode = new ProfileTreeNode(profile)
                    {
                        Tag = profile
                    };
                    node.Nodes.Add(profileNode);
                }

                node.Expand();

                if (!string.IsNullOrWhiteSpace(e.SourceFilePath))
                {
                    LoadWorkspaceTree();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "An unexpected error occurred when trying to change session file path.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Log(Level.Error, ex.Message, ex);
            }
            finally
            {
                adapterTreeView.EndUpdate();
            }
        }

        private void OnPluginSelected(object sender, EventArgs e)
        {
            // navigate to plugin
            if (sender is PluginsSectionControl section)
            {
                foreach (TreeNode node in adapterTreeView.Nodes[0].Nodes)
                {
                    if (node.Tag == section.DataSource)
                    {
                        foreach (TreeNode n in node.Nodes)
                        {
                            if (n.Tag == section.Plugin)
                            {
                                adapterTreeView.SelectedNode = n;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MarkDirty(true);
        }

        private void OnConfigurationSaved(object sender, EventArgs e)
        {
            MarkDirty(false);
        }

        private void MarkDirty(bool dirty)
        {
            if (adapterTreeView.Nodes.Count > 0)
            {
                MarkDirty(dirty, adapterTreeView.Nodes[0]);

            }

            MarkDirty(dirty, activeEditorLink);
        }

        private void MarkDirty(bool dirty, TreeNode node)
        {
            const string txt = " [UNSAVED]";

            if (dirty)
            {
                if (!node.Text.EndsWith(txt))
                {
                    node.Text += txt;
                }
            }
            else
            {
                node.Text = node.Text.Replace(txt, "");
            }
        }

        private void MarkDirty(bool dirty, LinkLabel node)
        {
            const string txt = " [UNSAVED]";

            if (dirty)
            {
                if (!node.Text.EndsWith(txt))
                {
                    node.Text += txt;
                }
            }
            else
            {
                node.Text = node.Text.Replace(txt, "");
            }
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var selectedNode = adapterTreeView.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Tag is AdapterConfiguration)
                {
                    openFolderInFileExplorerToolStripMenuItem.Visible = true;
                    openFolderInFileExplorerToolStripMenuItem.Enabled = true;
                    addSessionToolStripMenuItem.Visible = false;
                    deleteSessionToolStripMenuItem.Visible = false;
                    addProfileToolStripMenuItem.Visible = false;
                    addProfileFromCSVFileToolStripMenuItem.Visible = false;
                    deleteProfileToolStripMenuItem.Visible = false;
                    toolStripSeparator1.Visible = false;
                    return;
                }
                else
                {
                    openFolderInFileExplorerToolStripMenuItem.Visible = false;
                    openFolderInFileExplorerToolStripMenuItem.Enabled = false;
                    addSessionToolStripMenuItem.Visible = true;
                    deleteSessionToolStripMenuItem.Visible = true;
                    addProfileToolStripMenuItem.Visible = true;
                    addProfileFromCSVFileToolStripMenuItem.Visible = true;
                    deleteProfileToolStripMenuItem.Visible = true;
                    toolStripSeparator1.Visible = true;
                }

                if (selectedNode.Tag is ISessionable)
                {
                    addSessionToolStripMenuItem.Enabled = true;
                    deleteSessionToolStripMenuItem.Enabled = false;
                    addProfileToolStripMenuItem.Enabled = false;
                    addProfileFromCSVFileToolStripMenuItem.Enabled = false;
                    deleteProfileToolStripMenuItem.Enabled = false;
                }
                else if (selectedNode.Tag is Session)
                {
                    if (selectedNode == adapterTreeView.Nodes[0])
                    {
                        addSessionToolStripMenuItem.Enabled = false;
                        deleteSessionToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        addSessionToolStripMenuItem.Enabled = false;
                        deleteSessionToolStripMenuItem.Enabled = true;
                    }
                    addProfileToolStripMenuItem.Enabled = true;
                    addProfileFromCSVFileToolStripMenuItem.Enabled = true;
                    deleteProfileToolStripMenuItem.Enabled = false;
                }
                else if (selectedNode.Tag is Profile)
                {
                    addSessionToolStripMenuItem.Enabled = false;
                    deleteSessionToolStripMenuItem.Enabled = false;
                    addProfileToolStripMenuItem.Enabled = false;
                    addProfileFromCSVFileToolStripMenuItem.Enabled = false;
                    deleteProfileToolStripMenuItem.Enabled = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void AddProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adapterTreeView.SelectedNode?.Tag is Session session)
            {
                ProfileSelectionForm form = new ProfileSelectionForm(session.Edition, false);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sessionNode = adapterTreeView.SelectedNode;

                        foreach (var p in form.SelectedProfiles)
                        {
                            MarkDirty(true);

                            if (adapterTreeView.SelectedNode.Parent?.Tag is ISessionable plugin)
                            {
                                plugin.Enabled = true;
                            }

                            Profile profile = Profile.Create(p, session.PluginName, null);
                            session.SessionConfiguration.AddProfile(profile);

                            TreeNode pNode = new TreeNode(profile.ProfileName)
                            {
                                Tag = profile
                            };
                            sessionNode.Nodes.Add(pNode);
                            if (sessionNode.Nodes.Count > 0)
                            {
                                adapterTreeView.SelectedNode = sessionNode.Nodes[sessionNode.Nodes.Count - 1];
                            }
                        }

                        sessionNode.ExpandAll();

                        _configurationManager.UpdatePubSubTopics();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AddProfileFromCSVFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adapterTreeView.SelectedNode?.Tag is Session session)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        if (adapterTreeView.SelectedNode.Parent?.Tag is ISessionable plugin)
                        {
                            plugin.Enabled = true;
                        }

                        var profile = CSVParser.Parse(session.PluginName, openFileDialog.FileName);

                        string mrid = session.LocalFilePath;
                        var row = profile.CsvData.FirstOrDefault(x => x.Path.EndsWith("mRID"));
                        if (row != null)
                        {
                            mrid = row.Value;
                        }

                        session.SessionConfiguration.AddProfile(profile);

                        TreeNode pNode = new TreeNode(profile.ProfileName)
                        {
                            Tag = profile
                        };
                        adapterTreeView.SelectedNode.Nodes.Add(pNode);
                        adapterTreeView.SelectedNode.Expand();
                        adapterTreeView.SelectedNode = pNode;

                        _configurationManager.UpdatePubSubTopics();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void DeleteProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = adapterTreeView.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Tag is Profile profile)
                {
                    var parent = selectedNode.Parent;
                    var session = parent.Tag as Session;
                    session.SessionConfiguration.DeleteProfile(profile);

                    selectedNode.Parent.Nodes.Remove(selectedNode);
                    adapterTreeView.SelectedNode = parent;
                    parent.Expand();

                    MarkDirty(true);

                    var control = FindProfileControl(ProfileSettingsControlKey, profile);
                    if (control != null)
                    {
                        placeHolder.Controls.Remove(control);
                    }
                }
            }
        }

        private void AddSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adapterTreeView.SelectedNode.Tag is ISessionable plugin)
            {
                CreateSessionForm form = new CreateSessionForm(plugin);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Session session = form.Output;
                    var sNode = new TreeNode(session.Name)
                    {
                        Tag = session
                    };

                    adapterTreeView.SelectedNode.Nodes.Add(sNode);
                    adapterTreeView.SelectedNode.Expand();

                    foreach (var profile in session.SessionConfiguration?.GetProfiles())
                    {
                        var profileNode = new ProfileTreeNode(profile)
                        {
                            Tag = profile
                        };
                        sNode.Nodes.Add(profileNode);
                    }

                    LoadWorkspaceTree();

                    MarkDirty(true);


                }
            }
        }

        private void DeleteSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adapterTreeView.SelectedNode?.Tag is Session session)
            {
                var result = MessageBox.Show(this, $"Session will be deleted.{Environment.NewLine}Click YES to also delete the referenced template file.{Environment.NewLine}Click NO to keep the referenced template file.{Environment.NewLine}Click CANCEL to abort.", Program.AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    return;
                }

                var parent = adapterTreeView.SelectedNode.Parent;
                var plugin = parent.Tag as ISessionable;
                plugin.Sessions.Remove(session);
                adapterTreeView.SelectedNode.Remove();
                adapterTreeView.SelectedNode = parent;

                MarkDirty(true);

                var control = FindSessionControl(SessionSettingsControlKey, session);
                if (control != null)
                {
                    placeHolder.Controls.Remove(control);
                }

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(Path.Combine(WorkspaceDir, session.LocalFilePath));
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(Level.Error, ex.Message, ex);
                        MessageBox.Show(this, $"Unable to delete {session.LocalFilePath}.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void OpenFolderInFileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var node = adapterTreeView.SelectedNode;
                if (node != null)
                {
                    if (node.Tag is AdapterConfiguration config)
                    {
                        Process.Start(Path.GetDirectoryName(config.FullPath));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExpandButton_Click(object sender, EventArgs e)
        {
            mainSplitContainer.Panel1Collapsed = !mainSplitContainer.Panel1Collapsed;
        }

        private void LogShowHideButton_Click(object sender, EventArgs e)
        {
            detailSplitContainer.Panel2Collapsed = !detailSplitContainer.Panel2Collapsed;
        }

        private void LoadWorkspaceTree()
        {
            try
            {
                if (workspaceTree.InvokeRequired)
                {
                    workspaceTree.Invoke(new Action(() =>
                    {
                        LoadWorkspaceTree();
                    }));
                }
                else
                {
                    try
                    {
                        workspaceTree.BeginUpdate();

                        workspaceTree.Nodes.Clear();
                        var rootDirectoryInfo = new DirectoryInfo(WorkspaceDir);
                        workspaceTree.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
                        workspaceTree.ExpandAll();
                    }
                    finally
                    {
                        workspaceTree.EndUpdate();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Debug, ex.Message, ex);
            }
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name)
            {
                Name = directoryInfo.FullName,
                Tag = new FolderNode()
                {
                    Path = directoryInfo.FullName
                }
            };
            directoryNode.ImageIndex = directoryNode.SelectedImageIndex = FolderIcon;
            directoryNode.ToolTipText = directoryInfo.FullName;

            foreach (var directory in directoryInfo.GetDirectories())
            {
                if (!directory.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
                }
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                var treeNode = new TreeNode(file.Name)
                {
                    Name = file.FullName
                };
                var fileType = ConfigurationManager.GetFileInformation(file.FullName);
                treeNode.Tag = new FileNode
                {
                    Path = file.FullName,
                    FileInformation = fileType
                };
                var text = fileType.Id == ConfigFileType.MainAdapter ? "Adapter Configuration" : fileType.Id == ConfigFileType.Template ? "Template file" : "Not an OpenFMB config file";
                treeNode.ToolTipText = $"{file.FullName} ({text})";
                treeNode.ImageIndex = treeNode.SelectedImageIndex = (int)fileType.Id;
                directoryNode.Nodes.Add(treeNode);
            }
            return directoryNode;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleWorkspaceItemDeleting();
        }

        private void HandleWorkspaceItemDeleting()
        {
            if (workspaceTree.Nodes.Count > 0)
            {
                TreeNode node = workspaceTree.SelectedNode;

                if (node == workspaceTree.Nodes[0])
                {
                    // Can't delete root node
                    return;
                }

                if (node != null && node.Tag is DataNode)
                {
                    var dataNode = node.Tag as DataNode;

                    string message;
                    if (dataNode is FolderNode)
                    {
                        message = $"Are you sure that you want to delete '{Path.GetFileName(dataNode.Path)}' and its contents?";
                    }
                    else
                    {
                        message = $"Are you sure that you want to delete '{Path.GetFileName(dataNode.Path)}'?";
                    }

                    if (MessageBox.Show(this, message, Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            _configurationManager.SuspendFileWatcher();

                            if (node.Tag is FolderNode)
                            {
                                Directory.Delete((node.Tag as FolderNode).Path, true);
                                node.Remove();
                            }
                            else
                            {
                                File.Delete((node.Tag as DataNode).Path);
                                node.Remove();
                            }
                        }
                        finally
                        {
                            _configurationManager.ResumeFileWatcher();
                        }

                        // Check if active file path is still valid

                        if (_configurationManager.ActiveConfiguration != null)
                        {
                            if (!File.Exists(_configurationManager.ActiveConfiguration.FullPath))
                            {
                                // close it
                                CloseActiveConfiguration();
                            }
                        }
                    }
                }
            }
        }

        private void NewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = workspaceTree.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Tag is FolderNode folder)
                {
                    NewFolderForm form = new NewFolderForm();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(form.FolderName))
                        {
                            var path = Path.Combine(folder.Path, form.FolderName);
                            if (Directory.Exists(path))
                            {
                                MessageBox.Show(this, "Folder already exists", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                try
                                {
                                    _configurationManager.SuspendFileWatcher();
                                    var directoryInfo = Directory.CreateDirectory(path);
                                    var directoryNode = new TreeNode(directoryInfo.Name)
                                    {
                                        Tag = new FolderNode()
                                        {
                                            Path = directoryInfo.FullName
                                        }
                                    };
                                    directoryNode.ImageIndex = directoryNode.SelectedImageIndex = FolderIcon;
                                    selectedNode.Nodes.Add(directoryNode);
                                    selectedNode.Expand();
                                }
                                finally
                                {
                                    _configurationManager.ResumeFileWatcher();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadWorkspaceTree();
        }

        internal bool IsFolderSelected()
        {
            return workspaceTree.SelectedNode?.Tag is FolderNode;
        }

        private void WorkspaceTreeContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var selectedNode = workspaceTree.SelectedNode;
            if (selectedNode == null)
            {
                e.Cancel = true;
            }
            else
            {
                deleteToolStripMenuItem.Enabled = renameToolStripMenuItem.Enabled = selectedNode != workspaceTree.Nodes[0];  // can't delete/rename root node
                newFolderToolStripMenuItem.Enabled = selectedNode.Tag is FolderNode;
                editToolStripMenuItem.Enabled = ((selectedNode.Tag as FileNode)?.FileInformation.Id == ConfigFileType.MainAdapter || (selectedNode.Tag as FileNode)?.FileInformation.Id == ConfigFileType.Template);
                pasteToolStripMenuItem.Visible = _copiedDataNode != null;
            }
        }

        private void NewConfigButton_Click(object sender, EventArgs e)
        {
            var selectedNode = workspaceTree.SelectedNode ?? workspaceTree.Nodes[0];
            if (!(selectedNode.Tag is FolderNode folder))
            {
                selectedNode = selectedNode.Parent;
                folder = selectedNode.Tag as FolderNode;
            }

            CreateNewConfiguration(folder.Path, selectedNode);

        }

        private void NewTemplateButton_Click(object sender, EventArgs e)
        {
            var selectedNode = workspaceTree.SelectedNode ?? workspaceTree.Nodes[0];
            if (!(selectedNode.Tag is FolderNode folder))
            {
                selectedNode = selectedNode.Parent;
                folder = selectedNode.Tag as FolderNode;
            }

            CreateNewTemplate(folder.Path, selectedNode);
        }

        private void CreateNewConfiguration(string folderPath, TreeNode selectedNode)
        {
            try
            {
                _configurationManager.SuspendFileWatcher();

                CreateAdapterConfigurationForm form = new CreateAdapterConfigurationForm(folderPath, false);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IEditable editable = form.Output;
                    HandleFileAddedToWorkspace(selectedNode, editable);
                }
            }
            finally
            {
                Thread.Sleep(100);
                _configurationManager.ResumeFileWatcher();
            }
        }

        private void CreateNewTemplate(string folderPath, TreeNode selectedNode)
        {
            try
            {
                _configurationManager.SuspendFileWatcher();

                CreateTemplateConfigurationForm form = new CreateTemplateConfigurationForm(folderPath, false);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IEditable editable = form.Output;
                    HandleFileAddedToWorkspace(selectedNode, editable);
                }
            }
            finally
            {
                Thread.Sleep(100);
                _configurationManager.ResumeFileWatcher();
            }
        }

        private void HandleFileAddedToWorkspace(TreeNode selectedNode, IEditable editable)
        {
            var directory = Path.GetDirectoryName(editable.FullPath);
            try
            {
                workspaceTree.BeginUpdate();
                var parent = selectedNode.Parent;

                var rootDirectoryInfo = new DirectoryInfo(directory);
                var node = CreateDirectoryNode(rootDirectoryInfo);
                if (parent != null)
                {
                    selectedNode.Remove();
                    parent.Nodes.Add(node);
                }
                else
                {
                    selectedNode.Remove();
                    workspaceTree.Nodes.Add(node);
                }
                workspaceTree.SelectedNode = node;
                node.ExpandAll();
            }
            finally
            {
                workspaceTree.EndUpdate();
            }
        }

        private void HandleOnEditFileRequested(FileNode fileNode)
        {
            try
            {
                if (_configurationManager.ActiveConfiguration != null)
                {
                    if (FileHelper.FileEquals(_configurationManager.ActiveConfiguration.FullPath, fileNode.Path))
                    {
                        tabControl.SelectedIndex = 1;
                        return;
                    }
                    else
                    {
                        var result = MessageBox.Show(this, $"A file is already open in editor.{Environment.NewLine}Click YES to save and close it.{Environment.NewLine}Click NO to ignore all changes and close it.{Environment.NewLine}Click CANCEL to abort.", Program.AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel)
                        {
                            return;
                        }
                        else if (result == DialogResult.Yes)
                        {
                            Save();
                        }
                    }
                }

                // open it
                EditFile(fileNode);
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, "Unable to open configuration file or its referencing template files.", ex);
                detailSplitContainer.Panel2Collapsed = false;
            }
        }

        private void OnEditFileRequested(object sender, EventArgs e)
        {
            if (sender is FileInfoControl control)
            {
                if (control.DataNode is FileNode)
                {
                    HandleOnEditFileRequested(control.DataNode as FileNode);
                }
            }
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = workspaceTree.SelectedNode;
            if (selectedNode != null)
            {
                HandleOnEditFileRequested(selectedNode.Tag as FileNode);
            }
        }

        private void EditFile(FileNode fileNode)
        {
            try
            {
                _configurationManager.LoadConfiguration(fileNode.Path, fileNode?.FileInformation.Id);

                LoadFile(_configurationManager.ActiveConfiguration);

                tabControl.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
                MessageBox.Show(this, "Failed to open file.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            var changedFile = FileHelper.ConvertToForwardSlash(e.Name);

            if (_configurationManager.ActiveConfiguration != null)
            {
                var activeConfig = _configurationManager.ActiveConfiguration;
                if (activeConfig is AdapterConfiguration)
                {
                    var active = activeConfig as AdapterConfiguration;

                    if (FileHelper.FileEquals(active.FullPath, e.FullPath))
                    {
                        HandleActiveConfigFileChanged(e);
                    }
                    else
                    {
                        foreach (var p in active.Plugins.Plugins)
                        {
                            if (p is ISessionable)
                            {
                                foreach (var session in (p as ISessionable).Sessions)
                                {
                                    if (FileHelper.FileEquals(session.LocalFilePath, changedFile))
                                    {
                                        HandleActiveConfigFileChanged(e);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var session = activeConfig as Session;
                    if (FileHelper.FileEquals(session.LocalFilePath, changedFile))
                    {
                        HandleActiveConfigFileChanged(e);
                    }
                }
            }
            LoadWorkspaceTree();
        }

        private void HandleActiveConfigFileChanged(FileSystemEventArgs e)
        {
            Invoke(new Action(() =>
            {
                var activeFilePath = _configurationManager.ActiveConfiguration.FullPath;

                var changedFile = Path.Combine(_configurationManager.WorkingDirectory, e.Name);

                var result = MessageBox.Show(this, $"\"{changedFile}\"{Environment.NewLine}{Environment.NewLine}This file has been modified by another program.{Environment.NewLine}Do you want to reload it?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _configurationManager.UnloadConfiguration();
                        adapterTreeView.Nodes.Clear();

                        activeEditorPanel.Visible = false;

                        placeHolder.Controls.Clear();

                        // disable save and close buttons
                        saveAdapterButton.Enabled = closeAdapterButton.Enabled = false;

                        _configurationManager.LoadConfiguration(activeFilePath);

                        LoadFile(_configurationManager.ActiveConfiguration);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(Level.Error, ex.Message, ex);
                        MessageBox.Show(this, "Failed to open file.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }));
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            LoadWorkspaceTree();
        }

        private void FileSystemWatcher_DeletedOrRenamed(object sender, FileSystemEventArgs e)
        {
            if (_configurationManager.ActiveConfiguration != null)
            {
                if (!File.Exists(_configurationManager.ActiveConfiguration.FullPath))
                {
                    Invoke(new Action(() =>
                    {
                        var result = MessageBox.Show(this, $"File \"{_configurationManager.ActiveConfiguration.FullPath}\" no longer exists.  Keep it in editor?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            MarkDirty(true);
                        }
                        else
                        {
                            CloseActiveConfiguration();
                        }
                    }));
                }
            }
            LoadWorkspaceTree();
        }

        private void OpenContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (workspaceTree.SelectedNode?.Tag is DataNode selectedNode)
            {
                try
                {
                    Process.Start(Path.GetDirectoryName(selectedNode.Path));
                }
                catch (Exception ex)
                {
                    _logger.Log(Level.Error, ex.Message, ex);
                }
            }
        }

        private void ExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == workSpaceExpandAllToolStripMenuItem)
            {
                workspaceTree.ExpandAll();
            }
            else if (sender == adapterExpandAllToolStripMenuItem)
            {
                adapterTreeView.ExpandAll();
            }
        }

        private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == workspaceCollapseAllToolStripMenuItem)
            {
                workspaceTree.CollapseAll();
            }
            else if (sender == adapterCollapseAllToolStripMenuItem)
            {
                adapterTreeView.CollapseAll();
            }
        }

        private void CloseAdapterButton_Click(object sender, EventArgs e)
        {
            if (_configurationManager.HasChanged())
            {
                var result = MessageBox.Show(this, "Do you want to save changes to your configuration files?\n\nYour changes will be lost if you don't save.", Program.AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    Save();
                }
                // NO, just close it
            }

            CloseActiveConfiguration();

            // clear logs
            outputControl.ClearAll();

            // switch tab page
            tabControl.SelectedIndex = 0;
        }

        private void CloseActiveConfiguration()
        {
            _configurationManager.UnloadConfiguration();
            adapterTreeView.Nodes.Clear();

            activeEditorPanel.Visible = false;
            activeEditorLink.Text = string.Empty;
            activeFileTypeLabel.Text = string.Empty;
            activeFileNameLabel.Text = string.Empty;

            placeHolder.Controls.Clear();

            // disable save and close buttons
            saveAdapterButton.Enabled = closeAdapterButton.Enabled = false;
        }

        private void SaveAdapterButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                saveAdapterButton.Enabled = false;
                _configurationManager.Save();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "An error occured when saving the files.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Log(Level.Error, "An error occured when saving the files.", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
                saveAdapterButton.Enabled = true;
            }
        }

        private void WorkspaceTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectWorkspaceTreeNode(e.Node);
        }

        private void WorkspaceTree_NodeClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = workspaceTree.SelectedNode;
            if (selectedNode != null)
            {
                SelectWorkspaceTreeNode(selectedNode);
            }
        }

        private void SelectWorkspaceTreeNode(TreeNode selectedNode)
        {
            if (!(FindControl(FileInfoControlKey) is FileInfoControl c))
            {
                c = new FileInfoControl()
                {
                    Name = FileInfoControlKey,
                    Dock = DockStyle.Fill
                };
                c.OnEditFileRequested += OnEditFileRequested;
                placeHolder.Controls.Add(c);
            }

            var tag = selectedNode.Tag;
            if (tag is FolderNode)
            {
                var folder = tag as FolderNode;
                c.DataSource = folder;
                c.BringToFront();
            }
            else if (tag is FileNode)
            {
                var file = tag as FileNode;
                c.DataSource = file;
                c.BringToFront();
            }
        }

        private void WorkspaceTree_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                HandleWorkspaceItemDeleting();
            }
            else if (e.KeyData == (Keys.Control | Keys.C))
            {
                HandleWorkspaceNodeCopying();
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.V))
            {
                HandleWorkspaceNodePasting();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FindControl(FileInfoControlKey) is FileInfoControl c)
            {
                if (tabControl.SelectedIndex == 0)
                {
                    c.BringToFront();
                }
                else
                {
                    c.SendToBack();
                }
            }
            newConfigButton.Visible = newTemplateButton.Visible = tabControl.SelectedIndex == 0;
        }

        private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == e.Node.TreeView.SelectedNode)
            {
                Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                Rectangle r = e.Bounds;
                r.Offset(0, 1);
                Brush brush = SystemBrushes.Highlight;
                e.Graphics.FillRectangle(brush, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, r, e.Node.ForeColor, TextFormatFlags.GlyphOverhangPadding);
            }
            else
                e.DrawDefault = true;
        }

        private void ActiveEditorLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl.SelectedTab = tabPage2;
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = workspaceTree.SelectedNode;
            node?.BeginEdit();
        }

        private void WorkspaceTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e.Label))
                {
                    e.CancelEdit = true;
                    return;
                }

                var dataNode = e.Node.Tag as DataNode;

                if (dataNode is FolderNode)
                {
                    var newPath = Path.Combine(Path.GetDirectoryName(dataNode.Path), e.Label);

                    if (Directory.Exists(newPath))
                    {
                        e.CancelEdit = true;
                        MessageBox.Show(this, $"Folder '{e.Label}' already exists.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Directory.Move(dataNode.Path, newPath);
                        dataNode.Path = newPath;
                    }
                }
                else if (dataNode is FileNode)
                {
                    var newPath = Path.Combine(Path.GetDirectoryName(dataNode.Path), e.Label);

                    if (Directory.Exists(newPath))
                    {
                        e.CancelEdit = true;
                        MessageBox.Show(this, $"File '{e.Label}' already exists.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        File.Move(dataNode.Path, newPath);
                        dataNode.Path = newPath;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
                e.CancelEdit = true;
            }
        }

        private void HandleWorkspaceNodeCopying()
        {
            TreeNode node = workspaceTree.SelectedNode;
            if (node != null)
            {
                _copiedDataNode = node.Tag as DataNode;
            }
        }

        private void HandleWorkspaceNodePasting()
        {
            TreeNode node = workspaceTree.SelectedNode;
            if (node != null)
            {
                string parentFolder;
                if (node.Tag is FileNode)
                {
                    parentFolder = Path.GetDirectoryName((node.Tag as FileNode).Path);
                }
                else
                {
                    parentFolder = (node.Tag as DataNode).Path;
                }

                var tag = _copiedDataNode;
                if (tag != null)
                {
                    if (tag is FolderNode)
                    {
                        string name = Path.GetFileName(tag.Path);
                        // Get next folder name
                        string newPath = parentFolder.GetNextFolderName(name);

                        FileHelper.DirectoryCopy(tag.Path, newPath);
                    }
                    else if (tag is FileNode)
                    {
                        string name = Path.GetFileNameWithoutExtension(tag.Path);
                        string extension = Path.GetExtension(tag.Path);

                        string newPath = parentFolder.GetNextFileName(name, extension);
                        File.Copy(tag.Path, newPath);
                    }
                }
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleWorkspaceNodeCopying();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleWorkspaceNodePasting();
        }
    }

    [Serializable]
    public abstract class DataNode
    {
        public string Path { get; set; }
        public bool Selected { get; set; }
        public bool Expanded { get; set; }

        public string Name
        {
            get { return System.IO.Path.GetFileName(Path); }
        }
    }

    [Serializable]
    public class FileNode : DataNode
    {
        public FileInformation FileInformation { get; set; } = new FileInformation();
    }

    [Serializable]
    public class FolderNode : DataNode
    {
        public bool CanDelete { get; set; } = true;
    }

}