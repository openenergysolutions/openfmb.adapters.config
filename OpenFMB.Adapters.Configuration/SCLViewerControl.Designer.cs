namespace OpenFMB.Adapters.Configuration
{
    partial class SCLViewerControl
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
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createOpenFMBAdapterConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iedCombo = new System.Windows.Forms.ComboBox();
            this.createConfigButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.treeView);
            this.mainSplitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.mainSplitContainer.Size = new System.Drawing.Size(800, 600);
            this.mainSplitContainer.SplitterDistance = 175;
            this.mainSplitContainer.TabIndex = 2;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.ContextMenuStrip = this.contextMenuStrip;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.HideSelection = false;
            this.treeView.Indent = 35;
            this.treeView.ItemHeight = 23;
            this.treeView.Location = new System.Drawing.Point(0, 128);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(175, 472);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createOpenFMBAdapterConfigurationToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(296, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // createOpenFMBAdapterConfigurationToolStripMenuItem
            // 
            this.createOpenFMBAdapterConfigurationToolStripMenuItem.Name = "createOpenFMBAdapterConfigurationToolStripMenuItem";
            this.createOpenFMBAdapterConfigurationToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.createOpenFMBAdapterConfigurationToolStripMenuItem.Text = "Create OpenFMB Adapter Configuration...";
            this.createOpenFMBAdapterConfigurationToolStripMenuItem.Click += new System.EventHandler(this.CreateOpenFMBAdapterConfigurationToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.iedCombo);
            this.panel1.Controls.Add(this.createConfigButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 128);
            this.panel1.TabIndex = 3;
            // 
            // iedCombo
            // 
            this.iedCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iedCombo.BackColor = System.Drawing.Color.White;
            this.iedCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iedCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iedCombo.FormattingEnabled = true;
            this.iedCombo.Location = new System.Drawing.Point(15, 35);
            this.iedCombo.Name = "iedCombo";
            this.iedCombo.Size = new System.Drawing.Size(144, 23);
            this.iedCombo.TabIndex = 2;
            this.iedCombo.SelectedValueChanged += new System.EventHandler(this.IedCombo_SelectedValueChanged);
            // 
            // createConfigButton
            // 
            this.createConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createConfigButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.createConfigButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createConfigButton.Location = new System.Drawing.Point(15, 66);
            this.createConfigButton.Name = "createConfigButton";
            this.createConfigButton.Size = new System.Drawing.Size(144, 44);
            this.createConfigButton.TabIndex = 4;
            this.createConfigButton.Text = "Create OpenFMB Adapter Configuration";
            this.createConfigButton.UseVisualStyleBackColor = true;
            this.createConfigButton.Visible = false;
            this.createConfigButton.Click += new System.EventHandler(this.CreateConfigButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "IEDs:";
            // 
            // SCLViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "SCLViewerControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox iedCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createOpenFMBAdapterConfigurationToolStripMenuItem;
        private System.Windows.Forms.Button createConfigButton;
    }
}
