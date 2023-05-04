// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateAdapterConfigurationSelectFolder : UserControl
    {
        public EventHandler<EventArgs> SelectionChanged;

        public string Path
        {
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; }
        }

        public CreateAdapterConfigurationSelectFolder()
        {
            InitializeComponent();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog.SelectedPath;

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
