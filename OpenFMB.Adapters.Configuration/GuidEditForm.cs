// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class GuidEditForm : Form
    {
        public string Output
        {
            get; private set;
        }

        public GuidEditForm()
        {
            InitializeComponent();
        }

        public GuidEditForm(string s) : this()
        {
            idTextBox.Text = s;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Guid.TryParse(idTextBox.Text.Trim(), out Guid guid))
            {
                Output = guid.ToString().ToLower();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid uuid", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateNewGuid();
        }

        private void CreateNewGuid()
        {
            idTextBox.Text = Guid.NewGuid().ToString().ToLower();
        }

        private void EmptyButton_Click(object sender, EventArgs e)
        {
            idTextBox.Text = Guid.Empty.ToString().ToLower();
        }
    }
}