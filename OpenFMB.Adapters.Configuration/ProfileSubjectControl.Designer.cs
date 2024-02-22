namespace OpenFMB.Adapters.Configuration
{
    partial class ProfileSubjectControl
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.profileName = new System.Windows.Forms.Label();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.13158F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.86842F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel.Controls.Add(this.profileName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.subjectTextBox, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.deleteButton, 2, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(506, 28);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // profileName
            // 
            this.profileName.AutoSize = true;
            this.profileName.BackColor = System.Drawing.Color.White;
            this.profileName.Dock = System.Windows.Forms.DockStyle.Left;
            this.profileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profileName.Location = new System.Drawing.Point(4, 1);
            this.profileName.Name = "profileName";
            this.profileName.Size = new System.Drawing.Size(67, 26);
            this.profileName.TabIndex = 0;
            this.profileName.Text = "Profile Name";
            this.profileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectTextBox.Location = new System.Drawing.Point(187, 4);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(266, 20);
            this.subjectTextBox.TabIndex = 1;
            this.subjectTextBox.Enter += new System.EventHandler(this.SubjectTextBox_Enter);
            this.subjectTextBox.Leave += new System.EventHandler(this.SubjectTextBox_Leave);
            // 
            // deleteButton
            // 
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.delete;
            this.deleteButton.Location = new System.Drawing.Point(460, 4);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(35, 19);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ProfileSubjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ProfileSubjectControl";
            this.Size = new System.Drawing.Size(506, 28);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label profileName;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.Button deleteButton;
    }
}
