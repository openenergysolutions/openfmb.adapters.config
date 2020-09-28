namespace OpenFMB.Adapters.Configuration
{
    partial class BaseNavigatorNode
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
            this.navButton = new OpenFMB.Adapters.Configuration.FlatButton();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // navButton
            // 
            this.navButton.FlatAppearance.BorderSize = 0;
            this.navButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navButton.Image = global::OpenFMB.Adapters.Configuration.Properties.Resources.arrow;
            this.navButton.Location = new System.Drawing.Point(222, 84);
            this.navButton.Name = "navButton";
            this.navButton.Size = new System.Drawing.Size(20, 20);
            this.navButton.TabIndex = 6;
            this.navButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navButton.UseVisualStyleBackColor = true;
            this.navButton.Click += new System.EventHandler(this.NavButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BackgroundImage = global::OpenFMB.Adapters.Configuration.Properties.Resources.checkmark;
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox.Location = new System.Drawing.Point(0, 85);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(20, 20);
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            this.pictureBox.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 20000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipTitle = "Description";
            // 
            // BaseNavigatorNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.navButton);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "BaseNavigatorNode";
            this.Size = new System.Drawing.Size(244, 105);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FlatButton navButton;
        protected System.Windows.Forms.PictureBox pictureBox;
        protected System.Windows.Forms.ToolTip toolTip;
    }
}
