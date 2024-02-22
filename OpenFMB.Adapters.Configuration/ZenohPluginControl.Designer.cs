namespace OpenFMB.Adapters.Configuration
{
    partial class ZenohPluginControl
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
            this.enableCheckBox = new System.Windows.Forms.CheckBox();
            this.pluginBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.publishTab = new System.Windows.Forms.TabPage();
            this.publishPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.addPublishProfileButton = new System.Windows.Forms.Button();
            this.subscribeTab = new System.Windows.Forms.TabPage();
            this.subscribePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.addSubscribeProfileButton = new System.Windows.Forms.Button();
            this.resetPubSub = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pluginBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.publishTab.SuspendLayout();
            this.panel2.SuspendLayout();
            this.subscribeTab.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // enableCheckBox
            // 
            this.enableCheckBox.AutoSize = true;
            this.enableCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.pluginBindingSource, "Enabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.enableCheckBox.Location = new System.Drawing.Point(156, 24);
            this.enableCheckBox.Name = "enableCheckBox";
            this.enableCheckBox.Size = new System.Drawing.Size(15, 14);
            this.enableCheckBox.TabIndex = 11;
            this.enableCheckBox.UseVisualStyleBackColor = true;
            // 
            // pluginBindingSource
            // 
            this.pluginBindingSource.DataSource = typeof(OpenFMB.Adapters.Core.Models.Plugins.ZenohPlugin);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Enabled:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Max Queued Messages:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.pluginBindingSource, "MaxQueuedMessages", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown1.Location = new System.Drawing.Point(156, 43);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown1.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.enableCheckBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 115);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.publishTab);
            this.tabControl.Controls.Add(this.subscribeTab);
            this.tabControl.Location = new System.Drawing.Point(14, 200);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(598, 444);
            this.tabControl.TabIndex = 20;
            // 
            // publishTab
            // 
            this.publishTab.Controls.Add(this.publishPanel);
            this.publishTab.Controls.Add(this.panel2);
            this.publishTab.Location = new System.Drawing.Point(4, 22);
            this.publishTab.Name = "publishTab";
            this.publishTab.Padding = new System.Windows.Forms.Padding(3);
            this.publishTab.Size = new System.Drawing.Size(590, 418);
            this.publishTab.TabIndex = 0;
            this.publishTab.Text = "Publish";
            this.publishTab.UseVisualStyleBackColor = true;
            // 
            // publishPanel
            // 
            this.publishPanel.AutoScroll = true;
            this.publishPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.publishPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.publishPanel.Location = new System.Drawing.Point(3, 35);
            this.publishPanel.Name = "publishPanel";
            this.publishPanel.Size = new System.Drawing.Size(584, 380);
            this.publishPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.addPublishProfileButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 32);
            this.panel2.TabIndex = 2;
            // 
            // addPublishProfileButton
            // 
            this.addPublishProfileButton.Location = new System.Drawing.Point(5, 4);
            this.addPublishProfileButton.Name = "addPublishProfileButton";
            this.addPublishProfileButton.Size = new System.Drawing.Size(59, 23);
            this.addPublishProfileButton.TabIndex = 0;
            this.addPublishProfileButton.Text = "Add";
            this.addPublishProfileButton.UseVisualStyleBackColor = true;
            this.addPublishProfileButton.Click += new System.EventHandler(this.AddPublishProfileButton_Click);
            // 
            // subscribeTab
            // 
            this.subscribeTab.Controls.Add(this.subscribePanel);
            this.subscribeTab.Controls.Add(this.panel3);
            this.subscribeTab.Location = new System.Drawing.Point(4, 22);
            this.subscribeTab.Name = "subscribeTab";
            this.subscribeTab.Padding = new System.Windows.Forms.Padding(3);
            this.subscribeTab.Size = new System.Drawing.Size(590, 418);
            this.subscribeTab.TabIndex = 1;
            this.subscribeTab.Text = "Subscribe";
            this.subscribeTab.UseVisualStyleBackColor = true;
            // 
            // subscribePanel
            // 
            this.subscribePanel.AutoScroll = true;
            this.subscribePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subscribePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.subscribePanel.Location = new System.Drawing.Point(3, 35);
            this.subscribePanel.Name = "subscribePanel";
            this.subscribePanel.Size = new System.Drawing.Size(584, 380);
            this.subscribePanel.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.addSubscribeProfileButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 32);
            this.panel3.TabIndex = 4;
            // 
            // addSubscribeProfileButton
            // 
            this.addSubscribeProfileButton.Location = new System.Drawing.Point(5, 4);
            this.addSubscribeProfileButton.Name = "addSubscribeProfileButton";
            this.addSubscribeProfileButton.Size = new System.Drawing.Size(59, 23);
            this.addSubscribeProfileButton.TabIndex = 0;
            this.addSubscribeProfileButton.Text = "Add";
            this.addSubscribeProfileButton.UseVisualStyleBackColor = true;
            this.addSubscribeProfileButton.Click += new System.EventHandler(this.AddSubscribeProfileButton_Click);
            // 
            // resetPubSub
            // 
            this.resetPubSub.Location = new System.Drawing.Point(14, 171);
            this.resetPubSub.Name = "resetPubSub";
            this.resetPubSub.Size = new System.Drawing.Size(107, 23);
            this.resetPubSub.TabIndex = 21;
            this.resetPubSub.Text = "Reset Subjects";
            this.resetPubSub.UseVisualStyleBackColor = true;
            this.resetPubSub.Click += new System.EventHandler(this.ResetPubSub_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.pluginBindingSource, "ConnectRetrySeconds", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown2.Location = new System.Drawing.Point(156, 69);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown2.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Connection Re-try (sec):";
            // 
            // ZenohPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resetPubSub);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBox1);
            this.Name = "ZenohPluginControl";
            this.Size = new System.Drawing.Size(628, 659);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.resetPubSub, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pluginBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.publishTab.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.subscribeTab.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox enableCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage publishTab;
        private System.Windows.Forms.TabPage subscribeTab;
        private System.Windows.Forms.FlowLayoutPanel publishPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button addPublishProfileButton;
        private System.Windows.Forms.BindingSource pluginBindingSource;
        private System.Windows.Forms.FlowLayoutPanel subscribePanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button addSubscribeProfileButton;
        private System.Windows.Forms.Button resetPubSub;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label4;
    }
}
