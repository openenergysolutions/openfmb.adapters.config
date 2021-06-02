namespace OpenFMB.Adapters.Configuration
{
    partial class CreateSessionForm
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
            this.templateFileName = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.namedTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.copyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // templateFileName
            // 
            this.templateFileName.Location = new System.Drawing.Point(91, 44);
            this.templateFileName.Name = "templateFileName";
            this.templateFileName.Size = new System.Drawing.Size(279, 20);
            this.templateFileName.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(377, 96);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(96, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Session File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Session Name:";
            // 
            // namedTextBox
            // 
            this.namedTextBox.Location = new System.Drawing.Point(91, 70);
            this.namedTextBox.Name = "namedTextBox";
            this.namedTextBox.Size = new System.Drawing.Size(279, 20);
            this.namedTextBox.TabIndex = 4;
            this.namedTextBox.Text = "Session";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(376, 44);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(97, 22);
            this.browseButton.TabIndex = 6;
            this.browseButton.Text = "Select Template";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Yaml file|*.yaml|Yml file|*.yml|All files|*.*";
            this.openFileDialog.Title = "Select Session File";
            // 
            // copyLabel
            // 
            this.copyLabel.AutoSize = true;
            this.copyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyLabel.ForeColor = System.Drawing.Color.Red;
            this.copyLabel.Location = new System.Drawing.Point(12, 9);
            this.copyLabel.Name = "copyLabel";
            this.copyLabel.Size = new System.Drawing.Size(120, 13);
            this.copyLabel.TabIndex = 7;
            this.copyLabel.Text = "Copy file to work folder?";
            this.copyLabel.Visible = false;
            // 
            // CreateSessionForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(485, 127);
            this.Controls.Add(this.copyLabel);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.namedTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.templateFileName);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateSessionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Session";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox templateFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox namedTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label copyLabel;
    }
}