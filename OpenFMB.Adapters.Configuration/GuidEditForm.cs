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
    public partial class GuidEditForm : Form
    {
        public string Output
        {
            get; private set;
        }

        public GuidEditForm()
        {
            InitializeComponent();           
        }

        public GuidEditForm(string s) : this()
        {
            idTextBox.Text = s;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Guid guid;
            if (Guid.TryParse(idTextBox.Text.Trim(), out guid))
            {
                Output = guid.ToString().ToLower();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid uuid", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateNewGuid();
        }

        private void CreateNewGuid()
        {
            idTextBox.Text = Guid.NewGuid().ToString().ToLower();
        }

        private void EmptyButton_Click(object sender, EventArgs e)
        {
            idTextBox.Text = Guid.Empty.ToString().ToLower();
        }
    }
}