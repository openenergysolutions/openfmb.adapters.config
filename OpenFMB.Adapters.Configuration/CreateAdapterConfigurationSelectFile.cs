// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateAdapterConfigurationSelectFile : UserControl
    {
        public EventHandler<EventArgs> SelectionChanged;

        public string Path
        {
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; }
        }

        public CreateAdapterConfigurationSelectFile()
        {
            InitializeComponent();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = openFileDialog.FileName;

                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            pathTextBox.Text = string.Empty;

            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
