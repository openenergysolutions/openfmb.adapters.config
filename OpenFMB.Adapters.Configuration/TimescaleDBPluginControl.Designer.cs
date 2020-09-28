namespace OpenFMB.Adapters.Configuration
{
    partial class TimescaleDBPluginControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rawFormatCombo = new System.Windows.Forms.ComboBox();
            this.timescaleDBPluginBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.rawMessageTableName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.storeRawMessageCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.measureTableName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.storeMessageCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.enableCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timescaleDBPluginBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rawFormatCombo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rawMessageTableName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.storeRawMessageCheckBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.measureTableName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.storeMessageCheckBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.enableCheckBox);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(13, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 555);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // rawFormatCombo
            // 
            this.rawFormatCombo.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.timescaleDBPluginBindingSource, "RawDataFormat", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rawFormatCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rawFormatCombo.Enabled = false;
            this.rawFormatCombo.FormattingEnabled = true;
            this.rawFormatCombo.Location = new System.Drawing.Point(175, 265);
            this.rawFormatCombo.Name = "rawFormatCombo";
            this.rawFormatCombo.Size = new System.Drawing.Size(109, 21);
            this.rawFormatCombo.TabIndex = 27;
            // 
            // timescaleDBPluginBindingSource
            // 
            this.timescaleDBPluginBindingSource.DataSource = typeof(OpenFMB.Adapters.Core.Models.Plugins.TimescaleDBPlugin);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(57, 268);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Raw Data Format:";
            // 
            // rawMessageTableName
            // 
            this.rawMessageTableName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timescaleDBPluginBindingSource, "RawTableName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rawMessageTableName.Enabled = false;
            this.rawMessageTableName.Location = new System.Drawing.Point(175, 240);
            this.rawMessageTableName.Name = "rawMessageTableName";
            this.rawMessageTableName.Size = new System.Drawing.Size(109, 20);
            this.rawMessageTableName.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Raw Message Table Name:";
            // 
            // storeRawMessageCheckBox
            // 
            this.storeRawMessageCheckBox.AutoSize = true;
            this.storeRawMessageCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.timescaleDBPluginBindingSource, "StoreRawMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.storeRawMessageCheckBox.Location = new System.Drawing.Point(175, 220);
            this.storeRawMessageCheckBox.Name = "storeRawMessageCheckBox";
            this.storeRawMessageCheckBox.Size = new System.Drawing.Size(15, 14);
            this.storeRawMessageCheckBox.TabIndex = 23;
            this.storeRawMessageCheckBox.UseVisualStyleBackColor = true;
            this.storeRawMessageCheckBox.CheckedChanged += new System.EventHandler(this.storeRawMessageCheckBox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Store Raw Message:";
            // 
            // measureTableName
            // 
            this.measureTableName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timescaleDBPluginBindingSource, "TableName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.measureTableName.Enabled = false;
            this.measureTableName.Location = new System.Drawing.Point(175, 164);
            this.measureTableName.Name = "measureTableName";
            this.measureTableName.Size = new System.Drawing.Size(109, 20);
            this.measureTableName.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Measurement Table Name:";
            // 
            // storeMessageCheckBox
            // 
            this.storeMessageCheckBox.AutoSize = true;
            this.storeMessageCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.timescaleDBPluginBindingSource, "StoreMeasurement", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.storeMessageCheckBox.Location = new System.Drawing.Point(175, 144);
            this.storeMessageCheckBox.Name = "storeMessageCheckBox";
            this.storeMessageCheckBox.Size = new System.Drawing.Size(15, 14);
            this.storeMessageCheckBox.TabIndex = 19;
            this.storeMessageCheckBox.UseVisualStyleBackColor = true;
            this.storeMessageCheckBox.CheckedChanged += new System.EventHandler(this.StoreMessageCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Store Measurement Data:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.timescaleDBPluginBindingSource, "MaxQueuedMessages", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown1.Location = new System.Drawing.Point(175, 43);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown1.TabIndex = 13;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.timescaleDBPluginBindingSource, "ConnectRetrySeconds", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown2.Location = new System.Drawing.Point(175, 96);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown2.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Enabled:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Connection Re-try (sec):";
            // 
            // enableCheckBox
            // 
            this.enableCheckBox.AutoSize = true;
            this.enableCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.timescaleDBPluginBindingSource, "Enabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableCheckBox.Location = new System.Drawing.Point(175, 24);
            this.enableCheckBox.Name = "enableCheckBox";
            this.enableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.enableCheckBox.TabIndex = 11;
            this.enableCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.timescaleDBPluginBindingSource, "DatabaseUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(175, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 20);
            this.textBox1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Max Queued Messages:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Database URL:";
            // 
            // TimescaleDBPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TimescaleDBPluginControl";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timescaleDBPluginBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox enableCheckBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox storeMessageCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox measureTableName;
        private System.Windows.Forms.TextBox rawMessageTableName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox storeRawMessageCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox rawFormatCombo;
        private System.Windows.Forms.BindingSource timescaleDBPluginBindingSource;
    }
}
