// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models.Goose;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class GOOSEListControl : UserControl
    {
        public GOOSEListControl()
        {
            InitializeComponent();
        }

        public void LoadIed(IED ied)
        {
            headerLabel.Text = $"{ied.Name} - GOOSE";
            dataGrid.DataSource = ied.GseControls;
        }
    }
}