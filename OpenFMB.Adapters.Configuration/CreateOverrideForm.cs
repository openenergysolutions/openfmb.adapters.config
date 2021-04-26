// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateOverrideForm : Form
    {        
        public string Key { get { return keyTextBox.Text; } }
        public string Value { get { return valueTextBox.Text; } }

        public CreateOverrideForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (keyTextBox.Text.Length == 0 || valueTextBox.Text.Length == 0)
            {
                MessageBox.Show("Key and value are required.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

    }
}