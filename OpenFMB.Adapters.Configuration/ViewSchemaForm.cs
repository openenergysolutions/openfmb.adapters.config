// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class ViewSchemaForm : Form
    {
        public ViewSchemaForm()
        {
            InitializeComponent();
        }

        public ViewSchemaForm(string text) : this()
        {
            schemaTextBox.Text = text;
        }
    }
}
