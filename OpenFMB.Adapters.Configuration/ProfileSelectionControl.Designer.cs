namespace OpenFMB.Adapters.Configuration
{
    partial class ProfileSelectionControl
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Selection = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProfileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modeSelectionPanel = new System.Windows.Forms.Panel();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            this.fromFileRadio = new System.Windows.Forms.RadioButton();
            this.selectProfileRadio = new System.Windows.Forms.RadioButton();
            this.placeHolder = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.modeSelectionPanel.SuspendLayout();
            this.placeHolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selection,
            this.ProfileName});
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(561, 510);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseUp);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            // 
            // Selection
            // 
            this.Selection.FillWeight = 50F;
            this.Selection.HeaderText = "";
            this.Selection.Name = "Selection";
            // 
            // ProfileName
            // 
            this.ProfileName.FillWeight = 400F;
            this.ProfileName.HeaderText = "Profile Name";
            this.ProfileName.Name = "ProfileName";
            this.ProfileName.ReadOnly = true;
            this.ProfileName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProfileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // modeSelectionPanel
            // 
            this.modeSelectionPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.modeSelectionPanel.Controls.Add(this.versionComboBox);
            this.modeSelectionPanel.Controls.Add(this.fromFileRadio);
            this.modeSelectionPanel.Controls.Add(this.selectProfileRadio);
            this.modeSelectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.modeSelectionPanel.Location = new System.Drawing.Point(0, 0);
            this.modeSelectionPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.modeSelectionPanel.Name = "modeSelectionPanel";
            this.modeSelectionPanel.Size = new System.Drawing.Size(561, 71);
            this.modeSelectionPanel.TabIndex = 1;
            // 
            // versionComboBox
            // 
            this.versionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.versionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionComboBox.FormattingEnabled = true;
            this.versionComboBox.Location = new System.Drawing.Point(440, 22);
            this.versionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.versionComboBox.Name = "versionComboBox";
            this.versionComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.versionComboBox.Size = new System.Drawing.Size(100, 28);
            this.versionComboBox.TabIndex = 3;
            this.versionComboBox.SelectedIndexChanged += new System.EventHandler(this.VersionComboBox_SelectedIndexChanged);
            // 
            // fromFileRadio
            // 
            this.fromFileRadio.AutoSize = true;
            this.fromFileRadio.Location = new System.Drawing.Point(165, 23);
            this.fromFileRadio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fromFileRadio.Name = "fromFileRadio";
            this.fromFileRadio.Size = new System.Drawing.Size(88, 24);
            this.fromFileRadio.TabIndex = 1;
            this.fromFileRadio.Text = "From file";
            this.fromFileRadio.UseVisualStyleBackColor = true;
            this.fromFileRadio.Visible = false;
            // 
            // selectProfileRadio
            // 
            this.selectProfileRadio.AutoSize = true;
            this.selectProfileRadio.Checked = true;
            this.selectProfileRadio.Location = new System.Drawing.Point(18, 23);
            this.selectProfileRadio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectProfileRadio.Name = "selectProfileRadio";
            this.selectProfileRadio.Size = new System.Drawing.Size(128, 24);
            this.selectProfileRadio.TabIndex = 0;
            this.selectProfileRadio.TabStop = true;
            this.selectProfileRadio.Text = "Select Profiles";
            this.selectProfileRadio.UseVisualStyleBackColor = true;
            this.selectProfileRadio.Visible = false;
            this.selectProfileRadio.CheckedChanged += new System.EventHandler(this.SelectProfileRadio_CheckedChanged);
            // 
            // placeHolder
            // 
            this.placeHolder.Controls.Add(this.dataGridView);
            this.placeHolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.placeHolder.Location = new System.Drawing.Point(0, 71);
            this.placeHolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.placeHolder.Name = "placeHolder";
            this.placeHolder.Size = new System.Drawing.Size(561, 515);
            this.placeHolder.TabIndex = 2;
            // 
            // ProfileSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.placeHolder);
            this.Controls.Add(this.modeSelectionPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ProfileSelectionControl";
            this.Size = new System.Drawing.Size(561, 743);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.modeSelectionPanel.ResumeLayout(false);
            this.modeSelectionPanel.PerformLayout();
            this.placeHolder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selection;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileName;
        private System.Windows.Forms.Panel modeSelectionPanel;
        private System.Windows.Forms.ComboBox versionComboBox;
        private System.Windows.Forms.RadioButton fromFileRadio;
        private System.Windows.Forms.RadioButton selectProfileRadio;
        private System.Windows.Forms.Panel placeHolder;
    }
}