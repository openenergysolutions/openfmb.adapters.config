namespace OpenFMB.Adapters.Configuration
{
    partial class ViewSchemaForm
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
            this.schemaTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // schemaTextBox
            // 
            this.schemaTextBox.BackColor = System.Drawing.Color.White;
            this.schemaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.schemaTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.schemaTextBox.Location = new System.Drawing.Point(0, 0);
            this.schemaTextBox.Name = "schemaTextBox";
            this.schemaTextBox.ReadOnly = true;
            this.schemaTextBox.Size = new System.Drawing.Size(984, 761);
            this.schemaTextBox.TabIndex = 0;
            this.schemaTextBox.Text = "";
            // 
            // ViewSchemaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.schemaTextBox);
            this.Name = "ViewSchemaForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Schema";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox schemaTextBox;
    }
}