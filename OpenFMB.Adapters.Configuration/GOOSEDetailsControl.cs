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