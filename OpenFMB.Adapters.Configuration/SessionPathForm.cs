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
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility;
using System;
using System.IO;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SessionPathForm : Form
    {               
        private readonly ConfigurationManager _configurationManager = ConfigurationManager.Instance;

        public string FileName
        {
            get 
            { 
                return Path.Combine(_configurationManager.WorkingDirectory, templateFileName.Text.Trim()); 
            }
        }

        public SessionPathForm()
        {
            InitializeComponent();            
        }

        public SessionPathForm(Session session) : this()
        {                        
            templateFileName.Text = session.LocalFilePath;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(templateFileName.Text.Trim()))
            {
                MessageBox.Show("Please specify file name for this session.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}