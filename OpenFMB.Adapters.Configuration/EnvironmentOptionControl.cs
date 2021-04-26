// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Configuration.Properties;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class EnvironmentOptionControl : UserControl, IOptionControl
    {
        public EnvironmentOptionControl()
        {
            InitializeComponent();
            workingDir.Text = Settings.Default.PreviousWorkingFolder;

        }

        public void Save()
        {
            Settings.Default.PreviousWorkingFolder = workingDir.Text;
            Settings.Default.Save();
        }

        private void BrowseButton_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                workingDir.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
