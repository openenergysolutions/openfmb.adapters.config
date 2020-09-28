namespace OpenFMB.Adapters.Configuration
{
    partial class ConfigurationControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationControl));
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.directoryPage = new System.Windows.Forms.TabPage();
            this.workspaceTree = new System.Windows.Forms.TreeView();
            this.workspaceTreeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newAdapterConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.workSpaceExpandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workspaceCollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workspaceTreeImageList = new System.Windows.Forms.ImageList(this.components);
            this.activeEditorPanel = new System.Windows.Forms.Panel();
            this.activeEditorLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.adapterTreeView = new System.Windows.Forms.TreeView();
            this.adapterTreeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProfileFromCSVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderInFileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.adapterExpandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adapterCollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.saveAdapterButton = new OpenFMB.Adapters.Configuration.FlatButton();
            this.closeAdapterButton = new OpenFMB.Adapters.Configuration.FlatButton();
            this.detailSplitContainer = new System.Windows.Forms.SplitContainer();
            this.placeHolder = new System.Windows.Forms.Panel();
            this.outputControl = new OpenFMB.Adapters.Configuration.OutputControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logShowHideButton = new OpenFMB.Adapters.Configuration.FlatButton();
            this.expandButton = new OpenFMB.Adapters.Configuration.FlatButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.directoryPage.SuspendLayout();
            this.workspaceTreeContextMenu.SuspendLayout();
            this.activeEditorPanel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.adapterTreeContextMenuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailSplitContainer)).BeginInit();
            this.detailSplitContainer.Panel1.SuspendLayout();
            this.detailSplitContainer.Panel2.SuspendLayout();
            this.detailSplitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainSplitContainer.Location = new System.Drawing.Point(48, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.tabControl);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.detailSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(752, 600);
            this.mainSplitContainer.SplitterDistance = 270;
            this.mainSplitContainer.SplitterIncrement = 10;
            this.mainSplitContainer.SplitterWidth = 2;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.directoryPage);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(270, 600);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // directoryPage
            // 
            this.directoryPage.Controls.Add(this.workspaceTree);
            this.directoryPage.Controls.Add(this.activeEditorPanel);
            this.directoryPage.Location = new System.Drawing.Point(4, 4);
            this.directoryPage.Margin = new System.Windows.Forms.Padding(0);
            this.directoryPage.Name = "directoryPage";
            this.directoryPage.Size = new System.Drawing.Size(262, 574);
            this.directoryPage.TabIndex = 0;
            this.directoryPage.Text = "Workspace";
            this.directoryPage.UseVisualStyleBackColor = true;
            // 
            // workspaceTree
            // 
            this.workspaceTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.workspaceTree.ContextMenuStrip = this.workspaceTreeContextMenu;
            this.workspaceTree.Cursor = System.Windows.Forms.Cursors.Hand;
            this.workspaceTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspaceTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workspaceTree.FullRowSelect = true;
            this.workspaceTree.HideSelection = false;
            this.workspaceTree.ImageIndex = 0;
            this.workspaceTree.ImageList = this.workspaceTreeImageList;
            this.workspaceTree.Indent = 20;
            this.workspaceTree.ItemHeight = 21;
            this.workspaceTree.LabelEdit = true;
            this.workspaceTree.Location = new System.Drawing.Point(0, 55);
            this.workspaceTree.Name = "workspaceTree";
            this.workspaceTree.SelectedImageIndex = 0;
            this.workspaceTree.ShowLines = false;
            this.workspaceTree.ShowNodeToolTips = true;
            this.workspaceTree.Size = new System.Drawing.Size(262, 519);
            this.workspaceTree.TabIndex = 1;
            this.workspaceTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.WorkspaceTree_AfterLabelEdit);
            this.workspaceTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.WorkspaceTree_AfterSelect);
            this.workspaceTree.Click += new System.EventHandler(this.WorkspaceTree_NodeClick);
            this.workspaceTree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WorkspaceTree_KeyUp);
            // 
            // workspaceTreeContextMenu
            // 
            this.workspaceTreeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAdapterConfigurationToolStripMenuItem,
            this.newTemplateToolStripMenuItem,
            this.toolStripSeparator8,
            this.newFolderToolStripMenuItem,
            this.toolStripSeparator2,
            this.editToolStripMenuItem,
            this.toolStripSeparator4,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripSeparator3,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator9,
            this.reloadToolStripMenuItem,
            this.toolStripSeparator5,
            this.openContainingFolderToolStripMenuItem,
            this.toolStripSeparator6,
            this.workSpaceExpandAllToolStripMenuItem,
            this.workspaceCollapseAllToolStripMenuItem});
            this.workspaceTreeContextMenu.Name = "workspaceTreeContextMenu";
            this.workspaceTreeContextMenu.Size = new System.Drawing.Size(221, 310);
            this.workspaceTreeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.WorkspaceTreeContextMenu_Opening);
            // 
            // newAdapterConfigurationToolStripMenuItem
            // 
            this.newAdapterConfigurationToolStripMenuItem.Name = "newAdapterConfigurationToolStripMenuItem";
            this.newAdapterConfigurationToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.newAdapterConfigurationToolStripMenuItem.Text = "New Adapter Configuration";
            this.newAdapterConfigurationToolStripMenuItem.Click += new System.EventHandler(this.NewAdapterConfigurationToolStripMenuItem_Click);
            // 
            // newTemplateToolStripMenuItem
            // 
            this.newTemplateToolStripMenuItem.Name = "newTemplateToolStripMenuItem";
            this.newTemplateToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.newTemplateToolStripMenuItem.Text = "New Template";
            this.newTemplateToolStripMenuItem.Click += new System.EventHandler(this.NewTemplateToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(217, 6);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.folder_small;
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.NewFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.editToolStripMenuItem.Text = "Edit...";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(217, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(217, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(217, 6);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.refresh;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.reloadToolStripMenuItem.Text = "Refresh";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.ReloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(217, 6);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.folder_arrow;
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openContainingFolderToolStripMenuItem.Text = "Open Containing Folder";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenContainingFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(217, 6);
            // 
            // workSpaceExpandAllToolStripMenuItem
            // 
            this.workSpaceExpandAllToolStripMenuItem.Name = "workSpaceExpandAllToolStripMenuItem";
            this.workSpaceExpandAllToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.workSpaceExpandAllToolStripMenuItem.Text = "Expand All";
            this.workSpaceExpandAllToolStripMenuItem.Click += new System.EventHandler(this.ExpandAllToolStripMenuItem_Click);
            // 
            // workspaceCollapseAllToolStripMenuItem
            // 
            this.workspaceCollapseAllToolStripMenuItem.Name = "workspaceCollapseAllToolStripMenuItem";
            this.workspaceCollapseAllToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.workspaceCollapseAllToolStripMenuItem.Text = "Collapse All";
            this.workspaceCollapseAllToolStripMenuItem.Click += new System.EventHandler(this.CollapseAllToolStripMenuItem_Click);
            // 
            // workspaceTreeImageList
            // 
            this.workspaceTreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("workspaceTreeImageList.ImageStream")));
            this.workspaceTreeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.workspaceTreeImageList.Images.SetKeyName(0, "file.png");
            this.workspaceTreeImageList.Images.SetKeyName(1, "adapter_icon.png");
            this.workspaceTreeImageList.Images.SetKeyName(2, "template_icon.png");
            this.workspaceTreeImageList.Images.SetKeyName(3, "folder_small.png");
            this.workspaceTreeImageList.Images.SetKeyName(4, "active_editor.png");
            // 
            // activeEditorPanel
            // 
            this.activeEditorPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.activeEditorPanel.Controls.Add(this.activeEditorLink);
            this.activeEditorPanel.Controls.Add(this.label1);
            this.activeEditorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.activeEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.activeEditorPanel.Name = "activeEditorPanel";
            this.activeEditorPanel.Size = new System.Drawing.Size(262, 55);
            this.activeEditorPanel.TabIndex = 2;
            this.activeEditorPanel.Visible = false;
            // 
            // activeEditorLink
            // 
            this.activeEditorLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.activeEditorLink.AutoSize = true;
            this.activeEditorLink.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.activeEditorLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.activeEditorLink.Location = new System.Drawing.Point(3, 28);
            this.activeEditorLink.Name = "activeEditorLink";
            this.activeEditorLink.Size = new System.Drawing.Size(53, 13);
            this.activeEditorLink.TabIndex = 1;
            this.activeEditorLink.TabStop = true;
            this.activeEditorLink.Text = "ActiveFile";
            this.activeEditorLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.activeEditorLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ActiveEditorLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active Editor:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.adapterTreeView);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(262, 574);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuration";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // adapterTreeView
            // 
            this.adapterTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.adapterTreeView.ContextMenuStrip = this.adapterTreeContextMenuStrip;
            this.adapterTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adapterTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.adapterTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adapterTreeView.HideSelection = false;
            this.adapterTreeView.Indent = 20;
            this.adapterTreeView.ItemHeight = 21;
            this.adapterTreeView.Location = new System.Drawing.Point(0, 27);
            this.adapterTreeView.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.adapterTreeView.Name = "adapterTreeView";
            this.adapterTreeView.ShowLines = false;
            this.adapterTreeView.ShowNodeToolTips = true;
            this.adapterTreeView.Size = new System.Drawing.Size(262, 547);
            this.adapterTreeView.TabIndex = 0;
            this.adapterTreeView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TreeView_DrawNode);
            this.adapterTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // adapterTreeContextMenuStrip
            // 
            this.adapterTreeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProfileToolStripMenuItem,
            this.addProfileFromCSVFileToolStripMenuItem,
            this.deleteProfileToolStripMenuItem,
            this.toolStripSeparator1,
            this.addSessionToolStripMenuItem,
            this.deleteSessionToolStripMenuItem,
            this.openFolderInFileExplorerToolStripMenuItem,
            this.toolStripSeparator7,
            this.adapterExpandAllToolStripMenuItem,
            this.adapterCollapseAllToolStripMenuItem});
            this.adapterTreeContextMenuStrip.Name = "contextMenuStrip";
            this.adapterTreeContextMenuStrip.Size = new System.Drawing.Size(217, 192);
            this.adapterTreeContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // addProfileToolStripMenuItem
            // 
            this.addProfileToolStripMenuItem.Name = "addProfileToolStripMenuItem";
            this.addProfileToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.addProfileToolStripMenuItem.Text = "Add Profile...";
            this.addProfileToolStripMenuItem.Click += new System.EventHandler(this.AddProfileToolStripMenuItem_Click);
            // 
            // addProfileFromCSVFileToolStripMenuItem
            // 
            this.addProfileFromCSVFileToolStripMenuItem.Name = "addProfileFromCSVFileToolStripMenuItem";
            this.addProfileFromCSVFileToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.addProfileFromCSVFileToolStripMenuItem.Text = "Add Profile from CSV File...";
            this.addProfileFromCSVFileToolStripMenuItem.Click += new System.EventHandler(this.AddProfileFromCSVFileToolStripMenuItem_Click);
            // 
            // deleteProfileToolStripMenuItem
            // 
            this.deleteProfileToolStripMenuItem.Name = "deleteProfileToolStripMenuItem";
            this.deleteProfileToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.deleteProfileToolStripMenuItem.Text = "Delete Profile";
            this.deleteProfileToolStripMenuItem.Click += new System.EventHandler(this.DeleteProfileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // addSessionToolStripMenuItem
            // 
            this.addSessionToolStripMenuItem.Name = "addSessionToolStripMenuItem";
            this.addSessionToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.addSessionToolStripMenuItem.Text = "Add Session...";
            this.addSessionToolStripMenuItem.Click += new System.EventHandler(this.AddSessionToolStripMenuItem_Click);
            // 
            // deleteSessionToolStripMenuItem
            // 
            this.deleteSessionToolStripMenuItem.Name = "deleteSessionToolStripMenuItem";
            this.deleteSessionToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.deleteSessionToolStripMenuItem.Text = "Delete Session";
            this.deleteSessionToolStripMenuItem.Click += new System.EventHandler(this.DeleteSessionToolStripMenuItem_Click);
            // 
            // openFolderInFileExplorerToolStripMenuItem
            // 
            this.openFolderInFileExplorerToolStripMenuItem.Name = "openFolderInFileExplorerToolStripMenuItem";
            this.openFolderInFileExplorerToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.openFolderInFileExplorerToolStripMenuItem.Text = "Open Containing Folder";
            this.openFolderInFileExplorerToolStripMenuItem.Click += new System.EventHandler(this.OpenFolderInFileExplorerToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(213, 6);
            // 
            // adapterExpandAllToolStripMenuItem
            // 
            this.adapterExpandAllToolStripMenuItem.Name = "adapterExpandAllToolStripMenuItem";
            this.adapterExpandAllToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.adapterExpandAllToolStripMenuItem.Text = "Expand All";
            this.adapterExpandAllToolStripMenuItem.Click += new System.EventHandler(this.ExpandAllToolStripMenuItem_Click);
            // 
            // adapterCollapseAllToolStripMenuItem
            // 
            this.adapterCollapseAllToolStripMenuItem.Name = "adapterCollapseAllToolStripMenuItem";
            this.adapterCollapseAllToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.adapterCollapseAllToolStripMenuItem.Text = "Collapse All";
            this.adapterCollapseAllToolStripMenuItem.Click += new System.EventHandler(this.CollapseAllToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.saveAdapterButton);
            this.panel2.Controls.Add(this.closeAdapterButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 27);
            this.panel2.TabIndex = 2;
            // 
            // saveAdapterButton
            // 
            this.saveAdapterButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.saveAdapterButton.Enabled = false;
            this.saveAdapterButton.FlatAppearance.BorderSize = 0;
            this.saveAdapterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAdapterButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.save;
            this.saveAdapterButton.Location = new System.Drawing.Point(204, 1);
            this.saveAdapterButton.Name = "saveAdapterButton";
            this.saveAdapterButton.Size = new System.Drawing.Size(25, 25);
            this.saveAdapterButton.TabIndex = 1;
            this.saveAdapterButton.UseVisualStyleBackColor = true;
            this.saveAdapterButton.Click += new System.EventHandler(this.SaveAdapterButton_Click);
            // 
            // closeAdapterButton
            // 
            this.closeAdapterButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.closeAdapterButton.Enabled = false;
            this.closeAdapterButton.FlatAppearance.BorderSize = 0;
            this.closeAdapterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeAdapterButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.close;
            this.closeAdapterButton.Location = new System.Drawing.Point(232, 1);
            this.closeAdapterButton.Name = "closeAdapterButton";
            this.closeAdapterButton.Size = new System.Drawing.Size(25, 25);
            this.closeAdapterButton.TabIndex = 0;
            this.closeAdapterButton.UseVisualStyleBackColor = true;
            this.closeAdapterButton.Click += new System.EventHandler(this.CloseAdapterButton_Click);
            // 
            // detailSplitContainer
            // 
            this.detailSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.detailSplitContainer.Name = "detailSplitContainer";
            this.detailSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // detailSplitContainer.Panel1
            // 
            this.detailSplitContainer.Panel1.Controls.Add(this.placeHolder);
            // 
            // detailSplitContainer.Panel2
            // 
            this.detailSplitContainer.Panel2.Controls.Add(this.outputControl);
            this.detailSplitContainer.Size = new System.Drawing.Size(480, 600);
            this.detailSplitContainer.SplitterDistance = 447;
            this.detailSplitContainer.TabIndex = 1;
            // 
            // placeHolder
            // 
            this.placeHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeHolder.Location = new System.Drawing.Point(0, 0);
            this.placeHolder.Name = "placeHolder";
            this.placeHolder.Size = new System.Drawing.Size(480, 447);
            this.placeHolder.TabIndex = 0;
            // 
            // outputControl
            // 
            this.outputControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputControl.Location = new System.Drawing.Point(0, 0);
            this.outputControl.Name = "outputControl";
            this.outputControl.Size = new System.Drawing.Size(480, 149);
            this.outputControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GrayText;
            this.panel1.Controls.Add(this.logShowHideButton);
            this.panel1.Controls.Add(this.expandButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(48, 600);
            this.panel1.TabIndex = 0;
            // 
            // logShowHideButton
            // 
            this.logShowHideButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.logShowHideButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GrayText;
            this.logShowHideButton.FlatAppearance.BorderSize = 0;
            this.logShowHideButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
            this.logShowHideButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GrayText;
            this.logShowHideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logShowHideButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.logs;
            this.logShowHideButton.Location = new System.Drawing.Point(11, 563);
            this.logShowHideButton.Name = "logShowHideButton";
            this.logShowHideButton.Size = new System.Drawing.Size(25, 25);
            this.logShowHideButton.TabIndex = 1;
            this.logShowHideButton.UseVisualStyleBackColor = true;
            this.logShowHideButton.Click += new System.EventHandler(this.LogShowHideButton_Click);
            // 
            // expandButton
            // 
            this.expandButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.expandButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GrayText;
            this.expandButton.FlatAppearance.BorderSize = 0;
            this.expandButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GrayText;
            this.expandButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GrayText;
            this.expandButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expandButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.menu;
            this.expandButton.Location = new System.Drawing.Point(12, 9);
            this.expandButton.Name = "expandButton";
            this.expandButton.Size = new System.Drawing.Size(25, 25);
            this.expandButton.TabIndex = 0;
            this.expandButton.UseVisualStyleBackColor = true;
            this.expandButton.Click += new System.EventHandler(this.ExpandButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "CSV files|*.csv|All files|*.*";
            this.openFileDialog.Title = "Select Profile CSV File";
            // 
            // ConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigurationControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.directoryPage.ResumeLayout(false);
            this.workspaceTreeContextMenu.ResumeLayout(false);
            this.activeEditorPanel.ResumeLayout(false);
            this.activeEditorPanel.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.adapterTreeContextMenuStrip.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.detailSplitContainer.Panel1.ResumeLayout(false);
            this.detailSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detailSplitContainer)).EndInit();
            this.detailSplitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TreeView adapterTreeView;
        private System.Windows.Forms.Panel placeHolder;
        private System.Windows.Forms.ContextMenuStrip adapterTreeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProfileFromCSVFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem openFolderInFileExplorerToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.SplitContainer detailSplitContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage directoryPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolTip toolTip;
        private FlatButton expandButton;
        private FlatButton logShowHideButton;
        private System.Windows.Forms.TreeView workspaceTree;
        private System.Windows.Forms.ImageList workspaceTreeImageList;
        private System.Windows.Forms.ContextMenuStrip workspaceTreeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newAdapterConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem workSpaceExpandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workspaceCollapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem adapterExpandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adapterCollapseAllToolStripMenuItem;
        private System.Windows.Forms.Panel activeEditorPanel;
        private System.Windows.Forms.Panel panel2;
        private FlatButton closeAdapterButton;
        private FlatButton saveAdapterButton;
        private OutputControl outputControl;
        private System.Windows.Forms.LinkLabel activeEditorLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem newTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}
