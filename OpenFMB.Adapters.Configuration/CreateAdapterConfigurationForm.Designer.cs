namespace OpenFMB.Adapters.Configuration
{
    partial class CreateAdapterConfigurationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pluginOptionControl = new OpenFMB.Adapters.Configuration.CreateAdapterConfigurationPluginOptions();
            this.label1 = new System.Windows.Forms.Label();
            this.folderTextBox = new System.Windows.Forms.TextBox();
            this.browserButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            this.splitContainer.Size = new System.Drawing.Size(609, 382);
            this.splitContainer.SplitterDistance = 239;
            this.splitContainer.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(239, 382);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pluginOptionControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 382);
            this.panel1.TabIndex = 0;
            // 
            // pluginOptionControl
            // 
            this.pluginOptionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginOptionControl.Location = new System.Drawing.Point(0, 0);
            this.pluginOptionControl.Name = "pluginOptionControl";
            this.pluginOptionControl.Options = null;
            this.pluginOptionControl.Size = new System.Drawing.Size(364, 380);
            this.pluginOptionControl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Location:";
            // 
            // folderTextBox
            // 
            this.folderTextBox.Location = new System.Drawing.Point(12, 451);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.Size = new System.Drawing.Size(423, 20);
            this.folderTextBox.TabIndex = 3;
            // 
            // browserButton
            // 
            this.browserButton.Location = new System.Drawing.Point(436, 451);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(31, 20);
            this.browserButton.TabIndex = 4;
            this.browserButton.Text = "...";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.Browse_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(512, 449);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(12, 407);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(423, 20);
            this.nameTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name:";
            // 
            // CreateAdapterConfigurationForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 484);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.folderTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateAdapterConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Adapter Configuration";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel panel1;
        private CreateAdapterConfigurationPluginOptions pluginOptionControl;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
    }
}