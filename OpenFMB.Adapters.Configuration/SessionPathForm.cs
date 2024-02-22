// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models.Plugins;
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