namespace OpenFMB.Adapters.Configuration
{
    partial class CreateMappingTemplateStep3
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.protocolTextBox = new System.Windows.Forms.TextBox();
            this.moduleTextBox = new System.Windows.Forms.TextBox();
            this.profileTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFilePathTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.errorLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Legacy Protocol:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Profile:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "OpenFMB Module:";
            // 
            // protocolTextBox
            // 
            this.protocolTextBox.BackColor = System.Drawing.Color.White;
            this.protocolTextBox.Location = new System.Drawing.Point(132, 19);
            this.protocolTextBox.Name = "protocolTextBox";
            this.protocolTextBox.ReadOnly = true;
            this.protocolTextBox.Size = new System.Drawing.Size(241, 20);
            this.protocolTextBox.TabIndex = 10;
            // 
            // moduleTextBox
            // 
            this.moduleTextBox.BackColor = System.Drawing.Color.White;
            this.moduleTextBox.Location = new System.Drawing.Point(132, 45);
            this.moduleTextBox.Name = "moduleTextBox";
            this.moduleTextBox.ReadOnly = true;
            this.moduleTextBox.Size = new System.Drawing.Size(241, 20);
            this.moduleTextBox.TabIndex = 11;
            // 
            // profileTextBox
            // 
            this.profileTextBox.BackColor = System.Drawing.Color.White;
            this.profileTextBox.Location = new System.Drawing.Point(132, 71);
            this.profileTextBox.Name = "profileTextBox";
            this.profileTextBox.ReadOnly = true;
            this.profileTextBox.Size = new System.Drawing.Size(241, 20);
            this.profileTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Save To:";
            // 
            // saveFilePathTextBox
            // 
            this.saveFilePathTextBox.Location = new System.Drawing.Point(132, 130);
            this.saveFilePathTextBox.Name = "saveFilePathTextBox";
            this.saveFilePathTextBox.Size = new System.Drawing.Size(454, 20);
            this.saveFilePathTextBox.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(594, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Browse_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "mapping-template.csv";
            this.saveFileDialog.Filter = "CSV files|*.csv|All files|*.*";
            this.saveFileDialog.Title = "Save File";
            // 
            // errorLabel
            // 
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(129, 170);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(481, 95);
            this.errorLabel.TabIndex = 16;
            this.errorLabel.Text = "Error message!!!";
            this.errorLabel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.linkLabel);
            this.groupBox1.Controls.Add(this.protocolTextBox);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.errorLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.saveFilePathTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.moduleTextBox);
            this.groupBox1.Controls.Add(this.profileTextBox);
            this.groupBox1.Location = new System.Drawing.Point(19, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 475);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generate Mapping";
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(675, 135);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(52, 13);
            this.linkLabel.TabIndex = 17;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "Open File";
            this.linkLabel.Visible = false;
            this.linkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // CreateMappingTemplateStep3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateMappingTemplateStep3";
            this.Size = new System.Drawing.Size(800, 510);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox protocolTextBox;
        private System.Windows.Forms.TextBox moduleTextBox;
        private System.Windows.Forms.TextBox profileTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox saveFilePathTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabel;
    }
}
