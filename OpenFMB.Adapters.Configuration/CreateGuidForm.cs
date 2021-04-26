// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateGuidForm : Form
    {
        public CreateGuidForm()
        {
            InitializeComponent();
            CreateNewGuid();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(guidLabel.Text);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateNewGuid();
        }

        private void CreateNewGuid()
        {
            guidLabel.Text = Guid.NewGuid().ToString().ToLower();
        }
    }
}