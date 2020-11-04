namespace OpenFMB.Adapters.Configuration
{
    partial class StartPageControl
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
            this.appName = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.recentPanel = new System.Windows.Forms.Panel();
            this.newTemplateButton = new System.Windows.Forms.Button();
            this.openConfigurationButton = new System.Windows.Forms.Button();
            this.newConfigurationButton = new System.Windows.Forms.Button();
            this.openSclButton = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // appName
            // 
            this.appName.AutoSize = true;
            this.appName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.appName.Location = new System.Drawing.Point(33, 52);
            this.appName.Name = "appName";
            this.appName.Size = new System.Drawing.Size(44, 20);
            this.appName.TabIndex = 0;
            this.appName.Text = "Start";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(353, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Recent Work Folders";
            // 
            // recentPanel
            // 
            this.recentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recentPanel.Location = new System.Drawing.Point(358, 85);
            this.recentPanel.Name = "recentPanel";
            this.recentPanel.Size = new System.Drawing.Size(410, 436);
            this.recentPanel.TabIndex = 6;
            // 
            // newTemplateButton
            // 
            this.newTemplateButton.BackColor = System.Drawing.Color.White;
            this.newTemplateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newTemplateButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.newTemplateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newTemplateButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.newTemplateButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.template;
            this.newTemplateButton.Location = new System.Drawing.Point(152, 85);
            this.newTemplateButton.Name = "newTemplateButton";
            this.newTemplateButton.Size = new System.Drawing.Size(100, 100);
            this.newTemplateButton.TabIndex = 4;
            this.newTemplateButton.TabStop = false;
            this.newTemplateButton.Text = "New Template File";
            this.newTemplateButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.newTemplateButton.UseVisualStyleBackColor = false;
            this.newTemplateButton.Click += new System.EventHandler(this.NewTemplateButton_Click);
            // 
            // openConfigurationButton
            // 
            this.openConfigurationButton.BackColor = System.Drawing.Color.White;
            this.openConfigurationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openConfigurationButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.openConfigurationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openConfigurationButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.openConfigurationButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.open_folder;
            this.openConfigurationButton.Location = new System.Drawing.Point(37, 218);
            this.openConfigurationButton.Name = "openConfigurationButton";
            this.openConfigurationButton.Size = new System.Drawing.Size(100, 100);
            this.openConfigurationButton.TabIndex = 3;
            this.openConfigurationButton.TabStop = false;
            this.openConfigurationButton.Text = "Open Work Folder";
            this.openConfigurationButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.openConfigurationButton.UseVisualStyleBackColor = false;
            this.openConfigurationButton.Click += new System.EventHandler(this.OpenConfiguration_Click);
            // 
            // newConfigurationButton
            // 
            this.newConfigurationButton.BackColor = System.Drawing.Color.White;
            this.newConfigurationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newConfigurationButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.newConfigurationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newConfigurationButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.newConfigurationButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.config;
            this.newConfigurationButton.Location = new System.Drawing.Point(37, 85);
            this.newConfigurationButton.Name = "newConfigurationButton";
            this.newConfigurationButton.Size = new System.Drawing.Size(100, 100);
            this.newConfigurationButton.TabIndex = 2;
            this.newConfigurationButton.TabStop = false;
            this.newConfigurationButton.Text = "New Adapter Configuration";
            this.newConfigurationButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.newConfigurationButton.UseVisualStyleBackColor = false;
            this.newConfigurationButton.Click += new System.EventHandler(this.NewConfigurationButton_Click);
            // 
            // openSclButton
            // 
            this.openSclButton.BackColor = System.Drawing.Color.White;
            this.openSclButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openSclButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.openSclButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openSclButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.openSclButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.iec61850;
            this.openSclButton.Location = new System.Drawing.Point(152, 218);
            this.openSclButton.Name = "openSclButton";
            this.openSclButton.Size = new System.Drawing.Size(100, 100);
            this.openSclButton.TabIndex = 1;
            this.openSclButton.TabStop = false;
            this.openSclButton.Text = "Open SCL";
            this.openSclButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.openSclButton.UseVisualStyleBackColor = false;
            this.openSclButton.Visible = false;
            this.openSclButton.Click += new System.EventHandler(this.OpenSclButton_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFromListToolStripMenuItem,
            this.copyPathToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(170, 48);
            // 
            // removeFromListToolStripMenuItem
            // 
            this.removeFromListToolStripMenuItem.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.checkmark;
            this.removeFromListToolStripMenuItem.Name = "removeFromListToolStripMenuItem";
            this.removeFromListToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.removeFromListToolStripMenuItem.Text = "Remove From List";
            this.removeFromListToolStripMenuItem.Click += new System.EventHandler(this.RemoveFromListToolStripMenuItem_Click);
            // 
            // copyPathToolStripMenuItem
            // 
            this.copyPathToolStripMenuItem.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.copy;
            this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            this.copyPathToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyPathToolStripMenuItem.Text = "Copy Path";
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.CopyPathToolStripMenuItem_Click);
            // 
            // StartPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.recentPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newTemplateButton);
            this.Controls.Add(this.openConfigurationButton);
            this.Controls.Add(this.newConfigurationButton);
            this.Controls.Add(this.openSclButton);
            this.Controls.Add(this.appName);
            this.Name = "StartPageControl";
            this.Size = new System.Drawing.Size(784, 533);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label appName;
        private System.Windows.Forms.Button openSclButton;
        private System.Windows.Forms.Button newConfigurationButton;
        private System.Windows.Forms.Button openConfigurationButton;
        private System.Windows.Forms.Button newTemplateButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel recentPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPathToolStripMenuItem;
    }
}
