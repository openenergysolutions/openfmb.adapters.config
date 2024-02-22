// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorGuidNode : BaseNavigatorNode, IDataNode, INavigatorNode
    {
        public NavigatorGuidNode()
        {
            InitializeComponent();
        }

        public NavigatorGuidNode(Node node) : this()
        {
            nodeText.Text = node.Name;
            Data = node;
            valueControl.Text = (Data.Tag as JProperty)?.Value?.ToString();
            valueControl.ReadOnly = true;

            var desc = Data.Schema?.Description;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = SchemaManager.GetDescription(node.Name);
            }
            descLabel.Text = desc;
            toolTip.SetToolTip(descLabel, desc);
        }

        private void SetValueButton_Click(object sender, EventArgs e)
        {
            GuidEditForm form = new GuidEditForm((Data.Tag as JProperty).Value.ToString());
            if (form.ShowDialog() == DialogResult.OK)
            {
                (Data.Tag as JProperty).Value = new JValue(form.Output);
                valueControl.Text = form.Output;

                ValidateData();

                RaisePropertyChangedEvent(new PropertyChangedEventArgs(nodeText.Text));
            }
        }
    }
}
