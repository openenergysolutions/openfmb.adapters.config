// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CommandIdEditForm : Form
    {
        public string CommandId
        {
            get { return idTextBox.Text; }
        }
        public CommandIdEditForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text.Trim().Length > 0)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
