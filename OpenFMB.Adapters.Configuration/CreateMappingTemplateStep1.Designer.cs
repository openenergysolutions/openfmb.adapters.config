namespace OpenFMB.Adapters.Configuration
{
    partial class CreateMappingTemplateStep1
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
            this.protocolComboBox = new System.Windows.Forms.ComboBox();
            this.profileCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.moduleCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Legacy Protocol:";
            // 
            // protocolComboBox
            // 
            this.protocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolComboBox.FormattingEnabled = true;
            this.protocolComboBox.Location = new System.Drawing.Point(124, 30);
            this.protocolComboBox.Name = "protocolComboBox";
            this.protocolComboBox.Size = new System.Drawing.Size(203, 21);
            this.protocolComboBox.TabIndex = 1;
            // 
            // profileCombo
            // 
            this.profileCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileCombo.FormattingEnabled = true;
            this.profileCombo.Location = new System.Drawing.Point(124, 103);
            this.profileCombo.Name = "profileCombo";
            this.profileCombo.Size = new System.Drawing.Size(203, 21);
            this.profileCombo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Profile:";
            // 
            // moduleCombo
            // 
            this.moduleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleCombo.FormattingEnabled = true;
            this.moduleCombo.Location = new System.Drawing.Point(124, 67);
            this.moduleCombo.Name = "moduleCombo";
            this.moduleCombo.Size = new System.Drawing.Size(203, 21);
            this.moduleCombo.TabIndex = 6;
            this.moduleCombo.SelectedValueChanged += new System.EventHandler(this.ModuleCombo_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "OpenFMB Module:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.protocolComboBox);
            this.groupBox1.Controls.Add(this.profileCombo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.moduleCombo);
            this.groupBox1.Location = new System.Drawing.Point(19, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 477);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Profile";
            // 
            // CreateMappingTemplateStep1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateMappingTemplateStep1";
            this.Size = new System.Drawing.Size(800, 510);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox protocolComboBox;
        private System.Windows.Forms.ComboBox profileCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox moduleCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
