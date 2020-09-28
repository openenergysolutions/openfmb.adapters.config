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
