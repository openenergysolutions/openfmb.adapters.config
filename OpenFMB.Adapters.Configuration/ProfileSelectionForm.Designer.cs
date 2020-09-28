namespace OpenFMB.Adapters.Configuration
{
    partial class ProfileSelectionForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.profileSelectionControl = new OpenFMB.Adapters.Configuration.ProfileSelectionControl();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(287, 366);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // profileSelectionControl
            // 
            this.profileSelectionControl.BackColor = System.Drawing.Color.White;
            this.profileSelectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileSelectionControl.Location = new System.Drawing.Point(0, 0);
            this.profileSelectionControl.Name = "profileSelectionControl";
            this.profileSelectionControl.Size = new System.Drawing.Size(374, 401);
            this.profileSelectionControl.TabIndex = 2;
            // 
            // ProfileSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(374, 401);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.profileSelectionControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileSelectionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select OpenFMB Profile";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private ProfileSelectionControl profileSelectionControl;
    }
}