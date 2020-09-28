namespace OpenFMB.Adapters.Configuration
{
    partial class NavigatorGuidNode
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
            this.nodeText = new System.Windows.Forms.Label();
            this.valueControl = new System.Windows.Forms.TextBox();
            this.setValueButton = new OpenFMB.Adapters.Configuration.FlatButton();
            this.descLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // navButton
            // 
            this.navButton.FlatAppearance.BorderSize = 0;
            // 
            // nodeText
            // 
            this.nodeText.BackColor = System.Drawing.Color.White;
            this.nodeText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nodeText.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeText.ForeColor = System.Drawing.Color.Black;
            this.nodeText.Location = new System.Drawing.Point(0, 0);
            this.nodeText.Name = "nodeText";
            this.nodeText.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.nodeText.Size = new System.Drawing.Size(244, 23);
            this.nodeText.TabIndex = 0;
            this.nodeText.Text = "Text";
            this.nodeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueControl
            // 
            this.valueControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.valueControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueControl.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.valueControl.Location = new System.Drawing.Point(6, 59);
            this.valueControl.Name = "valueControl";
            this.valueControl.ReadOnly = true;
            this.valueControl.Size = new System.Drawing.Size(198, 23);
            this.valueControl.TabIndex = 3;
            // 
            // setValueButton
            // 
            this.setValueButton.Location = new System.Drawing.Point(210, 59);
            this.setValueButton.Name = "setValueButton";
            this.setValueButton.Size = new System.Drawing.Size(25, 23);
            this.setValueButton.TabIndex = 4;
            this.setValueButton.Text = "...";
            this.setValueButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.setValueButton.UseVisualStyleBackColor = true;
            this.setValueButton.Click += new System.EventHandler(this.SetValueButton_Click);
            // 
            // descLabel
            // 
            this.descLabel.AutoEllipsis = true;
            this.descLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.descLabel.Location = new System.Drawing.Point(6, 31);
            this.descLabel.Name = "descLabel";
            this.descLabel.Padding = new System.Windows.Forms.Padding(3);
            this.descLabel.Size = new System.Drawing.Size(229, 25);
            this.descLabel.TabIndex = 10;
            this.descLabel.Text = "no description";
            // 
            // NavigatorGuidNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.setValueButton);
            this.Controls.Add(this.valueControl);
            this.Controls.Add(this.nodeText);
            this.Name = "NavigatorGuidNode";
            this.Controls.SetChildIndex(this.nodeText, 0);
            this.Controls.SetChildIndex(this.valueControl, 0);
            this.Controls.SetChildIndex(this.setValueButton, 0);
            this.Controls.SetChildIndex(this.pictureBox, 0);
            this.Controls.SetChildIndex(this.navButton, 0);
            this.Controls.SetChildIndex(this.descLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nodeText;        
        private System.Windows.Forms.TextBox valueControl;
        private FlatButton setValueButton;
        private System.Windows.Forms.Label descLabel;
    }
}
