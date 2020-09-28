namespace OpenFMB.Adapters.Configuration
{
    partial class CapturePluginControl
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
            this.enableCheckBox = new System.Windows.Forms.CheckBox();
            this.capturePluginBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.capturePluginBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enabled:";
            // 
            // enableCheckBox
            // 
            this.enableCheckBox.AutoSize = true;
            this.enableCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.capturePluginBindingSource, "Enabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableCheckBox.Location = new System.Drawing.Point(78, 40);
            this.enableCheckBox.Name = "enableCheckBox";
            this.enableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.enableCheckBox.TabIndex = 9;
            this.enableCheckBox.UseVisualStyleBackColor = true;
            // 
            // capturePluginBindingSource
            // 
            this.capturePluginBindingSource.DataSource = typeof(OpenFMB.Adapters.Core.Models.Plugins.CapturePlugin);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "File:";
            // 
            // fileTextBox
            // 
            this.fileTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.capturePluginBindingSource, "File", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fileTextBox.Location = new System.Drawing.Point(78, 60);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(204, 20);
            this.fileTextBox.TabIndex = 11;
            // 
            // CapturePluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.enableCheckBox);
            this.Controls.Add(this.label1);
            this.Name = "CapturePluginControl";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.enableCheckBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.fileTextBox, 0);
            ((System.ComponentModel.ISupportInitialize)(this.capturePluginBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox enableCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.BindingSource capturePluginBindingSource;
    }
}
