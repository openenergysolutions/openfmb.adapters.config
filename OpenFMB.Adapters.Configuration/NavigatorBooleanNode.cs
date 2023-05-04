// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using System.ComponentModel;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorBooleanNode : BaseNavigatorNode, IDataNode, INavigatorNode
    {
        public string Value
        {
            get { return valueControl.SelectedItem?.ToString(); }
        }

        public NavigatorBooleanNode()
        {
            InitializeComponent();
        }

        public NavigatorBooleanNode(Node node) : this()
        {
            nodeText.Text = node.Name;
            Data = node;
            valueControl.BringToFront();

            var val = (node.Tag as JProperty).Value as JValue;
            valueControl.SelectedIndex = val.Value.ToString().ToLower() == "false" ? 0 : 1;

            valueControl.SelectedIndexChanged += ValueControl_SelectedIndexChanged;

            var desc = Data.Schema?.Description;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = SchemaManager.GetDescription(node.Name);
            }
            descLabel.Text = desc;
            toolTip.SetToolTip(descLabel, desc);
        }

        private void ValueControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            (Data.Tag as JProperty).Value = new JValue(valueControl.SelectedIndex != 0);

            ValidateData();

            RaisePropertyChangedEvent(new PropertyChangedEventArgs(nodeText.Text));
        }
    }
}
