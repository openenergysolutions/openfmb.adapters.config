/*
 * Copyright 2017-2020 Duke Energy Corporation and Open Energy Solutions, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
            labelCopyright.Text = $"Copyright © 2018-{DateTime.Now.Year} Duke Energy Corporation and Open Energy Solutions, Inc.";
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
