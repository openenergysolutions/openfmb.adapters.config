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

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class StartPageControl : UserControl
    {
        public StartPageControl()
        {
            InitializeComponent();
            appName.Text = Program.AppName;

            toolTip.SetToolTip(newConfigurationButton, "Create a main adapter configuration.");
            toolTip.SetToolTip(newTemplateButton, "Create a template file that can be referenced by a plug-in session.");
            toolTip.SetToolTip(openConfigurationButton, "Open a working folder where adapter configuration files are located.");
        }       

        private void OpenSclButton_Click(object sender, EventArgs e)
        {
            Program.Mainform.OpenSCL();
        }

        private void OpenConfiguration_Click(object sender, EventArgs e)
        {
            Program.Mainform.OpenConfigurationFolder();
        }

        private void NewConfigurationButton_Click(object sender, EventArgs e)
        {
            Program.Mainform.CreateAdapterConfiguration();
        }

        private void NewTemplateButton_Click(object sender, EventArgs e)
        {
            Program.Mainform.CreateTemplate();
        }
    }
}
