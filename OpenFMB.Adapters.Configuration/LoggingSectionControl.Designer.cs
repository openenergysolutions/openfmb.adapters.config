namespace OpenFMB.Adapters.Configuration
{
    partial class LoggingSectionControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loggerNameTextBox = new System.Windows.Forms.TextBox();
            this.loggingSectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logToConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rotatingFileCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maxSize = new System.Windows.Forms.NumericUpDown();
            this.maxFiles = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.loggingSectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Logger Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Log to Console:";
            // 
            // loggerNameTextBox
            // 
            this.loggerNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.loggingSectionBindingSource, "LoggerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.loggerNameTextBox.Location = new System.Drawing.Point(113, 38);
            this.loggerNameTextBox.Name = "loggerNameTextBox";
            this.loggerNameTextBox.Size = new System.Drawing.Size(196, 20);
            this.loggerNameTextBox.TabIndex = 4;
            this.loggerNameTextBox.Text = "application";
            // 
            // loggingSectionBindingSource
            // 
            this.loggingSectionBindingSource.DataSource = typeof(OpenFMB.Adapters.Core.Models.Logging.LoggingSection);
            // 
            // logToConsoleCheckBox
            // 
            this.logToConsoleCheckBox.AutoSize = true;
            this.logToConsoleCheckBox.Checked = true;
            this.logToConsoleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logToConsoleCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.loggingSectionBindingSource, "ConsoleEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.logToConsoleCheckBox.Location = new System.Drawing.Point(113, 68);
            this.logToConsoleCheckBox.Name = "logToConsoleCheckBox";
            this.logToConsoleCheckBox.Size = new System.Drawing.Size(15, 14);
            this.logToConsoleCheckBox.TabIndex = 5;
            this.logToConsoleCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Rotating File:";
            // 
            // rotatingFileCheckBox
            // 
            this.rotatingFileCheckBox.AutoSize = true;
            this.rotatingFileCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.loggingSectionBindingSource, "RotatingFileEnable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rotatingFileCheckBox.Location = new System.Drawing.Point(113, 95);
            this.rotatingFileCheckBox.Name = "rotatingFileCheckBox";
            this.rotatingFileCheckBox.Size = new System.Drawing.Size(15, 14);
            this.rotatingFileCheckBox.TabIndex = 7;
            this.rotatingFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Max Size:";
            // 
            // maxSize
            // 
            this.maxSize.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.loggingSectionBindingSource, "RotatingFileMaxSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxSize.Location = new System.Drawing.Point(113, 119);
            this.maxSize.Maximum = new decimal(new int[] {
            1500000,
            0,
            0,
            0});
            this.maxSize.Name = "maxSize";
            this.maxSize.Size = new System.Drawing.Size(100, 20);
            this.maxSize.TabIndex = 9;
            this.maxSize.Value = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            // 
            // maxFiles
            // 
            this.maxFiles.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.loggingSectionBindingSource, "RotatingFileMaxFiles", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxFiles.Location = new System.Drawing.Point(113, 142);
            this.maxFiles.Name = "maxFiles";
            this.maxFiles.Size = new System.Drawing.Size(100, 20);
            this.maxFiles.TabIndex = 11;
            this.maxFiles.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Max Files:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Path:";
            // 
            // pathTextBox
            // 
            this.pathTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.loggingSectionBindingSource, "RotatingFilePath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pathTextBox.Location = new System.Drawing.Point(113, 164);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(196, 20);
            this.pathTextBox.TabIndex = 13;
            this.pathTextBox.Text = "adapter.log";
            // 
            // LoggingSectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.maxFiles);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maxSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rotatingFileCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logToConsoleCheckBox);
            this.Controls.Add(this.loggerNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoggingSectionControl";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.loggerNameTextBox, 0);
            this.Controls.SetChildIndex(this.logToConsoleCheckBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.rotatingFileCheckBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.maxSize, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.maxFiles, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.pathTextBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.loggingSectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox loggerNameTextBox;
        private System.Windows.Forms.CheckBox logToConsoleCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox rotatingFileCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown maxSize;
        private System.Windows.Forms.NumericUpDown maxFiles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.BindingSource loggingSectionBindingSource;
    }
}
