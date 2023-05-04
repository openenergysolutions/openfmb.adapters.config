// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

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
