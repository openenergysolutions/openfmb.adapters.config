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

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            try
            {
                var path = Context.Parameters["targetdir"].TrimEnd('\\');                
                var appDataDir = FileHelper.GetAppDataFolder();
                File.Copy(Path.Combine(path, "OpenFMB.Models.xml"), Path.Combine(appDataDir, "OpenFMB.Models.xml"));
            }
            catch { }
            
        }
    }
}
