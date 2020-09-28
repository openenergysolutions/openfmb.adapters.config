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
using System.Drawing;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SessionSettingsItemControl : UserControl
    {
        public event EventHandler<EventArgs> OnSelected;
        public event EventHandler<EventArgs> OnEditRequested;

        private bool _selected;

        public bool Editable
        {
            get { return editButton.Visible; }
            set { editButton.Visible = value; }
        }

        public string EditButtonText
        {
            get { return editButton.Text; }
            set { editButton.Text = value ?? value; }
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    if (_selected)
                    {
                        label.BackColor = SystemColors.Highlight;
                        label.ForeColor = Color.White;

                        OnSelected?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        label.BackColor = SystemColors.Control;
                        label.ForeColor = Color.Black;
                    }
                }
            }
        }

        public SessionSettingsItemControl()
        {
            InitializeComponent();
        }

        public SessionSettingsItemControl(string text) : this()
        {
            label.Text = text;
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Selected = true;            
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            OnEditRequested?.Invoke(this, e);
        }
    }
}
