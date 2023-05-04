// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using OpenFMB.Adapters.Core.Utility;
using System;
using System.ComponentModel;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorNumericNode : BaseNavigatorNode, IDataNode, INavigatorNode
    {
        public NavigatorNumericNode()
        {
            InitializeComponent();
        }

        public NavigatorNumericNode(Node node) : this()
        {
            nodeText.Text = node.Name;
            Data = node;

            if (node.Schema.Minimum.HasValue)
            {
                valueControl.Minimum = (decimal)node.Schema.Minimum.Value;
            }
            if (node.Schema.Maximum.HasValue)
            {
                valueControl.Maximum = (decimal)node.Schema.Maximum.Value;
            }

            var val = (Data.Tag as JProperty).Value as JValue;

            if (node.Schema?.Type == Newtonsoft.Json.Schema.JSchemaType.Integer)
            {
                valueControl.DecimalPlaces = 0;
                valueControl.Value = NumberConverter.ToInteger(val.Value);
            }
            else
            {
                valueControl.Value = NumberConverter.ToDecimal(val.Value);
            }

            valueControl.ValueChanged += ValueControl_ValueChanged;

            var desc = Data.Schema?.Description;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = SchemaManager.GetDescription(node.Name);
            }
            descLabel.Text = desc;
            toolTip.SetToolTip(descLabel, desc);
        }

        private void ValueControl_ValueChanged(object sender, EventArgs e)
        {
            if (Data.Schema?.Type == Newtonsoft.Json.Schema.JSchemaType.Integer)
            {
                (Data.Tag as JProperty).Value = new JValue((int)valueControl.Value);
            }
            else
            {
                (Data.Tag as JProperty).Value = new JValue(((double)valueControl.Value));
            }

            ValidateData();

            RaisePropertyChangedEvent(new PropertyChangedEventArgs(nodeText.Text));
        }
    }
}
