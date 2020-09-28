﻿namespace OpenFMB.Adapters.Configuration
{
    partial class FileInfoControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.versionTextBox = new System.Windows.Forms.TextBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.editionTextBox = new System.Windows.Forms.TextBox();
            this.editionLabel = new System.Windows.Forms.Label();
            this.openButton = new OpenFMB.Adapters.Configuration.FlatButton();
            this.lastModifiedTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.createdDateTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nodeTypeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fullPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.plugInTypeTextBox = new System.Windows.Forms.TextBox();
            this.pluginLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.plugInTypeTextBox);
            this.groupBox1.Controls.Add(this.pluginLabel);
            this.groupBox1.Controls.Add(this.versionTextBox);
            this.groupBox1.Controls.Add(this.versionLabel);
            this.groupBox1.Controls.Add(this.editionTextBox);
            this.groupBox1.Controls.Add(this.editionLabel);
            this.groupBox1.Controls.Add(this.openButton);
            this.groupBox1.Controls.Add(this.lastModifiedTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.createdDateTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nodeTypeTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.fullPathTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 543);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // versionTextBox
            // 
            this.versionTextBox.BackColor = System.Drawing.Color.White;
            this.versionTextBox.Location = new System.Drawing.Point(358, 128);
            this.versionTextBox.Name = "versionTextBox";
            this.versionTextBox.ReadOnly = true;
            this.versionTextBox.Size = new System.Drawing.Size(129, 20);
            this.versionTextBox.TabIndex = 6;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(267, 131);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(85, 13);
            this.versionLabel.TabIndex = 11;
            this.versionLabel.Text = "Adapter Version:";
            // 
            // editionTextBox
            // 
            this.editionTextBox.BackColor = System.Drawing.Color.White;
            this.editionTextBox.Location = new System.Drawing.Point(119, 128);
            this.editionTextBox.Name = "editionTextBox";
            this.editionTextBox.ReadOnly = true;
            this.editionTextBox.Size = new System.Drawing.Size(129, 20);
            this.editionTextBox.TabIndex = 5;
            // 
            // editionLabel
            // 
            this.editionLabel.AutoSize = true;
            this.editionLabel.Location = new System.Drawing.Point(6, 131);
            this.editionLabel.Name = "editionLabel";
            this.editionLabel.Size = new System.Drawing.Size(93, 13);
            this.editionLabel.TabIndex = 9;
            this.editionLabel.Text = "OpenFMB Edition:";
            // 
            // openButton
            // 
            this.openButton.BackColor = System.Drawing.Color.Gainsboro;
            this.openButton.FlatAppearance.BorderSize = 0;
            this.openButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openButton.Location = new System.Drawing.Point(119, 185);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(79, 32);
            this.openButton.TabIndex = 7;
            this.openButton.Text = "Edit";
            this.openButton.UseVisualStyleBackColor = false;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // lastModifiedTextBox
            // 
            this.lastModifiedTextBox.BackColor = System.Drawing.Color.White;
            this.lastModifiedTextBox.Location = new System.Drawing.Point(119, 76);
            this.lastModifiedTextBox.Name = "lastModifiedTextBox";
            this.lastModifiedTextBox.ReadOnly = true;
            this.lastModifiedTextBox.Size = new System.Drawing.Size(462, 20);
            this.lastModifiedTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Last Modified:";
            // 
            // createdDateTextBox
            // 
            this.createdDateTextBox.BackColor = System.Drawing.Color.White;
            this.createdDateTextBox.Location = new System.Drawing.Point(119, 50);
            this.createdDateTextBox.Name = "createdDateTextBox";
            this.createdDateTextBox.ReadOnly = true;
            this.createdDateTextBox.Size = new System.Drawing.Size(462, 20);
            this.createdDateTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Created Date:";
            // 
            // nodeTypeTextBox
            // 
            this.nodeTypeTextBox.BackColor = System.Drawing.Color.White;
            this.nodeTypeTextBox.Location = new System.Drawing.Point(119, 102);
            this.nodeTypeTextBox.Name = "nodeTypeTextBox";
            this.nodeTypeTextBox.ReadOnly = true;
            this.nodeTypeTextBox.Size = new System.Drawing.Size(462, 20);
            this.nodeTypeTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "File Type:";
            // 
            // fullPathTextBox
            // 
            this.fullPathTextBox.BackColor = System.Drawing.Color.White;
            this.fullPathTextBox.Location = new System.Drawing.Point(119, 25);
            this.fullPathTextBox.Name = "fullPathTextBox";
            this.fullPathTextBox.ReadOnly = true;
            this.fullPathTextBox.Size = new System.Drawing.Size(462, 20);
            this.fullPathTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Full Path:";
            // 
            // plugInTypeTextBox
            // 
            this.plugInTypeTextBox.BackColor = System.Drawing.Color.White;
            this.plugInTypeTextBox.Location = new System.Drawing.Point(119, 154);
            this.plugInTypeTextBox.Name = "plugInTypeTextBox";
            this.plugInTypeTextBox.ReadOnly = true;
            this.plugInTypeTextBox.Size = new System.Drawing.Size(129, 20);
            this.plugInTypeTextBox.TabIndex = 12;
            // 
            // pluginLabel
            // 
            this.pluginLabel.AutoSize = true;
            this.pluginLabel.Location = new System.Drawing.Point(56, 157);
            this.pluginLabel.Name = "pluginLabel";
            this.pluginLabel.Size = new System.Drawing.Size(43, 13);
            this.pluginLabel.TabIndex = 13;
            this.pluginLabel.Text = "Plug-In:";
            // 
            // FileInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FileInfoControl";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fullPathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lastModifiedTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox createdDateTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nodeTypeTextBox;
        private FlatButton openButton;
        private System.Windows.Forms.TextBox versionTextBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.TextBox editionTextBox;
        private System.Windows.Forms.Label editionLabel;
        private System.Windows.Forms.TextBox plugInTypeTextBox;
        private System.Windows.Forms.Label pluginLabel;
    }
}
