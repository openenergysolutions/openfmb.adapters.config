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
            this.newTemplateButton = new System.Windows.Forms.Button();
            this.openConfigurationButton = new System.Windows.Forms.Button();
            this.newConfigurationButton = new System.Windows.Forms.Button();
            this.openSclButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // appName
            // 
            this.appName.AutoSize = true;
            this.appName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.appName.Location = new System.Drawing.Point(33, 42);
            this.appName.Name = "appName";
            this.appName.Size = new System.Drawing.Size(305, 20);
            this.appName.TabIndex = 0;
            this.appName.Text = "OpenFMB GOOSE Adapter Configuration";
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
            // StartPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.newTemplateButton);
            this.Controls.Add(this.openConfigurationButton);
            this.Controls.Add(this.newConfigurationButton);
            this.Controls.Add(this.openSclButton);
            this.Controls.Add(this.appName);
            this.Name = "StartPageControl";
            this.Size = new System.Drawing.Size(784, 533);
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
    }
}
