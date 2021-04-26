// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models.Goose;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class GOOSEDetailsControl : UserControl
    {
        public GOOSEDetailsControl()
        {
            InitializeComponent();
        }

        public void LoadGseControl(GseControl gse)
        {
            headerLabel.Text = $"{gse.IED.Name} - GOOSE - {gse.LogicalDevice} - {gse.LogicalNode} - {gse.Name}";

            gooseControlReference.Text = gse.GseControlReference;
            destMacAddress.Text = gse.DestinationMACAddress;
            appId.Text = gse.AppId.ToString();
            gooseId.Text = gse.GooseId;
            datasetReference.Text = gse.DataSetReference;
            configurationRevision.Text = gse.ConfRev;

            dataGrid.DataSource = gse.Dataset.FCDAs;
        }
    }
}