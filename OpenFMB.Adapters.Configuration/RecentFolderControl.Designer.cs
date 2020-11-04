namespace OpenFMB.Adapters.Configuration
{
    partial class RecentFolderControl
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
            this.folderName = new System.Windows.Forms.Label();
            this.folderPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // folderName
            // 
            this.folderName.AutoEllipsis = true;
            this.folderName.AutoSize = true;
            this.folderName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.folderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderName.ForeColor = System.Drawing.Color.Black;
            this.folderName.Location = new System.Drawing.Point(3, 2);
            this.folderName.Name = "folderName";
            this.folderName.Size = new System.Drawing.Size(67, 13);
            this.folderName.TabIndex = 1;
            this.folderName.Text = "Folder Name";
            // 
            // folderPath
            // 
            this.folderPath.AutoEllipsis = true;
            this.folderPath.AutoSize = true;
            this.folderPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.folderPath.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.folderPath.Location = new System.Drawing.Point(3, 16);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(70, 13);
            this.folderPath.TabIndex = 2;
            this.folderPath.Text = "c:/temp/path";
            // 
            // RecentFolderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.folderName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "RecentFolderControl";
            this.Size = new System.Drawing.Size(489, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label folderName;
        private System.Windows.Forms.Label folderPath;
    }
}
