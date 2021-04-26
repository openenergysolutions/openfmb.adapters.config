// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NodeErrorForm : Form
    {
        public NodeErrorForm()
        {
            InitializeComponent();
        }

        public NodeErrorForm(string text) : this()
        {
            errorTextBox.Text = text;
        }
    }
}
