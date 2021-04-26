// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Text = String.Format("About {0}", Program.AppName);
            labelProductName.Text = Program.AppName;
            labelVersion.Text = string.Format("Version {0}", AssemblyVersion);
            labelCopyright.Text = $"Copyright © 2018-{DateTime.Now.Year} Open Energy Solutions, Inc.";
            Assembly creditAssm = Assembly.GetExecutingAssembly();
            using (Stream stream = creditAssm.GetManifestResourceStream("OpenFMB.Adapters.Configuration.Resources.Credits.rtf"))
            {
                openSource.LoadFile(stream, RichTextBoxStreamType.PlainText);
            }
        }       

        public string AssemblyVersion
        {
            get
            {
                return ConfigurationManager.Version;
            }
        }
    }
}
