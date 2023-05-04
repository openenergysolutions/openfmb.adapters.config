// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Utility;
using System.Collections;
using System.ComponentModel;
using System.IO;

namespace OpenFMB.Adapters.Configuration
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Uninstall(IDictionary savedState)
        {
            // Delete schema folder
            try
            {
                var appDataDir = FileHelper.GetAppDataFolder();
                Directory.Delete(appDataDir, true);
            }
            catch { }
            base.Uninstall(savedState);
        }
    }
}
