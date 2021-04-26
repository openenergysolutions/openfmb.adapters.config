// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class StackTraceViewerForm : Form
    {
        public string Content
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }
        public StackTraceViewerForm()
        {
            InitializeComponent();
        }
    }
}
