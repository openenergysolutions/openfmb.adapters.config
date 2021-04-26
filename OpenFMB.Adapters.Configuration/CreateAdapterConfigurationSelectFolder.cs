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
            get { return pathTextBox.Text;  }
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

                if (SelectionChanged != null)
                {
                    SelectionChanged(this, EventArgs.Empty);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pathTextBox.Text = string.Empty;

            if (SelectionChanged != null)
            {
                SelectionChanged(this, EventArgs.Empty);
            }
        }
    }
}
