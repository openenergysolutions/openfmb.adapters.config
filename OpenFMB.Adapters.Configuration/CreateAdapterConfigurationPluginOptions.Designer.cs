namespace OpenFMB.Adapters.Configuration
{
    partial class CreateAdapterConfigurationPluginOptions
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
            this.modeSelectionPanel = new System.Windows.Forms.Panel();
            this.fromFolderRadio = new System.Windows.Forms.RadioButton();
            this.fromFileRadio = new System.Windows.Forms.RadioButton();
            this.selectProfileRadio = new System.Windows.Forms.RadioButton();
            this.placeHolder = new System.Windows.Forms.Panel();
            this.modeSelectionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // modeSelectionPanel
            // 
            this.modeSelectionPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.modeSelectionPanel.Controls.Add(this.fromFolderRadio);
            this.modeSelectionPanel.Controls.Add(this.fromFileRadio);
            this.modeSelectionPanel.Controls.Add(this.selectProfileRadio);
            this.modeSelectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.modeSelectionPanel.Location = new System.Drawing.Point(0, 0);
            this.modeSelectionPanel.Name = "modeSelectionPanel";
            this.modeSelectionPanel.Size = new System.Drawing.Size(364, 46);
            this.modeSelectionPanel.TabIndex = 0;
            // 
            // fromFolderRadio
            // 
            this.fromFolderRadio.AutoSize = true;
            this.fromFolderRadio.Location = new System.Drawing.Point(267, 15);
            this.fromFolderRadio.Name = "fromFolderRadio";
            this.fromFolderRadio.Size = new System.Drawing.Size(77, 17);
            this.fromFolderRadio.TabIndex = 2;
            this.fromFolderRadio.Text = "From folder";
            this.fromFolderRadio.UseVisualStyleBackColor = true;
            this.fromFolderRadio.Visible = false;
            this.fromFolderRadio.CheckedChanged += new System.EventHandler(this.SelectProfileRadio_CheckedChanged);
            // 
            // fromFileRadio
            // 
            this.fromFileRadio.AutoSize = true;
            this.fromFileRadio.Location = new System.Drawing.Point(151, 15);
            this.fromFileRadio.Name = "fromFileRadio";
            this.fromFileRadio.Size = new System.Drawing.Size(64, 17);
            this.fromFileRadio.TabIndex = 1;
            this.fromFileRadio.Text = "From file";
            this.fromFileRadio.UseVisualStyleBackColor = true;
            this.fromFileRadio.CheckedChanged += new System.EventHandler(this.SelectProfileRadio_CheckedChanged);
            // 
            // selectProfileRadio
            // 
            this.selectProfileRadio.AutoSize = true;
            this.selectProfileRadio.Checked = true;
            this.selectProfileRadio.Location = new System.Drawing.Point(12, 15);
            this.selectProfileRadio.Name = "selectProfileRadio";
            this.selectProfileRadio.Size = new System.Drawing.Size(92, 17);
            this.selectProfileRadio.TabIndex = 0;
            this.selectProfileRadio.TabStop = true;
            this.selectProfileRadio.Text = "Select Profiles";
            this.selectProfileRadio.UseVisualStyleBackColor = true;
            this.selectProfileRadio.CheckedChanged += new System.EventHandler(this.SelectProfileRadio_CheckedChanged);
            // 
            // placeHolder
            // 
            this.placeHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeHolder.Location = new System.Drawing.Point(0, 46);
            this.placeHolder.Name = "placeHolder";
            this.placeHolder.Size = new System.Drawing.Size(364, 334);
            this.placeHolder.TabIndex = 1;
            // 
            // CreateAdapterConfigurationPluginOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.placeHolder);
            this.Controls.Add(this.modeSelectionPanel);
            this.DoubleBuffered = true;
            this.Name = "CreateAdapterConfigurationPluginOptions";
            this.Size = new System.Drawing.Size(364, 380);
            this.modeSelectionPanel.ResumeLayout(false);
            this.modeSelectionPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel modeSelectionPanel;
        private System.Windows.Forms.RadioButton selectProfileRadio;
        private System.Windows.Forms.RadioButton fromFileRadio;
        private System.Windows.Forms.RadioButton fromFolderRadio;
        private System.Windows.Forms.Panel placeHolder;
    }
}
