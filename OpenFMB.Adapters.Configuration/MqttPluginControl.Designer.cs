namespace OpenFMB.Adapters.Configuration
{
    partial class MqttPluginControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.securityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.clientCertFile = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.clientKeyFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.caCertFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.securityTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.pluginBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.securityBindingSource)).BeginInit();
            this.tabControl.SuspendLayout();
            this.publishTab.SuspendLayout();
            this.panel2.SuspendLayout();
            this.subscribeTab.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.pluginBindingSource.DataSource = typeof(OpenFMB.Adapters.Core.Models.Plugins.MqttPlugin);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Connection URL:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pluginBindingSource, "ConnectUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(156, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 20);
            this.textBox1.TabIndex = 15;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.pluginBindingSource, "ConnectRetryMs", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown2.Location = new System.Drawing.Point(156, 96);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown2.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Connection Re-try (ms):";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.enableCheckBox);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 151);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pluginBindingSource, "ClientId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox5.Location = new System.Drawing.Point(156, 122);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(269, 20);
            this.textBox5.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(85, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Client ID:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.clientCertFile);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.clientKeyFile);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.caCertFile);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.securityTypeComboBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(14, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(598, 186);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Security";
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.securityBindingSource, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox7.Location = new System.Drawing.Point(156, 148);
            this.textBox7.Name = "textBox7";
            this.textBox7.PasswordChar = '*';
            this.textBox7.Size = new System.Drawing.Size(269, 20);
            this.textBox7.TabIndex = 24;
            // 
            // securityBindingSource
            // 
            this.securityBindingSource.DataSource = typeof(OpenFMB.Adapters.Core.Models.Plugins.MqttSecurity);
            // 
            // textBox6
            // 
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.securityBindingSource, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox6.Location = new System.Drawing.Point(156, 122);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(269, 20);
            this.textBox6.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(79, 151);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Password:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Username:";
            // 
            // clientCertFile
            // 
            this.clientCertFile.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.securityBindingSource, "ClientCert", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.clientCertFile.Location = new System.Drawing.Point(156, 94);
            this.clientCertFile.Name = "clientCertFile";
            this.clientCertFile.Size = new System.Drawing.Size(269, 20);
            this.clientCertFile.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Client Cert. Chain File:";
            // 
            // clientKeyFile
            // 
            this.clientKeyFile.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.securityBindingSource, "ClientKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.clientKeyFile.Location = new System.Drawing.Point(156, 70);
            this.clientKeyFile.Name = "clientKeyFile";
            this.clientKeyFile.Size = new System.Drawing.Size(269, 20);
            this.clientKeyFile.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Client Private Key File:";
            // 
            // caCertFile
            // 
            this.caCertFile.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.securityBindingSource, "CertFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.caCertFile.Location = new System.Drawing.Point(156, 46);
            this.caCertFile.Name = "caCertFile";
            this.caCertFile.Size = new System.Drawing.Size(269, 20);
            this.caCertFile.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "CA Trusted Cert File:";
            // 
            // securityTypeComboBox
            // 
            this.securityTypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.securityBindingSource, "SecurityType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.securityTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.securityTypeComboBox.FormattingEnabled = true;
            this.securityTypeComboBox.Items.AddRange(new object[] {
            "none",
            "tls_server_auth",
            "tls_mutual_auth"});
            this.securityTypeComboBox.Location = new System.Drawing.Point(156, 19);
            this.securityTypeComboBox.Name = "securityTypeComboBox";
            this.securityTypeComboBox.Size = new System.Drawing.Size(161, 21);
            this.securityTypeComboBox.TabIndex = 12;
            this.securityTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.SecurityType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Type:";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.publishTab);
            this.tabControl.Controls.Add(this.subscribeTab);
            this.tabControl.Location = new System.Drawing.Point(14, 414);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(598, 234);
            this.tabControl.TabIndex = 20;
            // 
            // publishTab
            // 
            this.publishTab.Controls.Add(this.publishPanel);
            this.publishTab.Controls.Add(this.panel2);
            this.publishTab.Location = new System.Drawing.Point(4, 22);
            this.publishTab.Name = "publishTab";
            this.publishTab.Padding = new System.Windows.Forms.Padding(3);
            this.publishTab.Size = new System.Drawing.Size(590, 208);
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
            this.publishPanel.Size = new System.Drawing.Size(584, 170);
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
            this.subscribeTab.Size = new System.Drawing.Size(590, 208);
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
            this.subscribePanel.Size = new System.Drawing.Size(584, 170);
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
            this.resetPubSub.Location = new System.Drawing.Point(13, 385);
            this.resetPubSub.Name = "resetPubSub";
            this.resetPubSub.Size = new System.Drawing.Size(107, 23);
            this.resetPubSub.TabIndex = 22;
            this.resetPubSub.Text = "Reset Topics";
            this.resetPubSub.UseVisualStyleBackColor = true;
            this.resetPubSub.Click += new System.EventHandler(this.ResetPubSub_Click);
            // 
            // MqttPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.resetPubSub);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MqttPluginControl";
            this.Size = new System.Drawing.Size(628, 662);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.resetPubSub, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pluginBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.securityBindingSource)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.publishTab.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.subscribeTab.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox enableCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox securityTypeComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox clientCertFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox clientKeyFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox caCertFile;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage publishTab;
        private System.Windows.Forms.TabPage subscribeTab;
        private System.Windows.Forms.FlowLayoutPanel publishPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button addPublishProfileButton;
        private System.Windows.Forms.BindingSource pluginBindingSource;
        private System.Windows.Forms.BindingSource securityBindingSource;
        private System.Windows.Forms.FlowLayoutPanel subscribePanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button addSubscribeProfileButton;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button resetPubSub;
    }
}
