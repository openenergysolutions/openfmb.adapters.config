// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorStringSelectionNode : BaseNavigatorNode, IDataNode, INavigatorNode
    {
        public string Value
        {
            get { return valueControl.SelectedItem?.ToString(); }
        }

        public NavigatorStringSelectionNode()
        {
            InitializeComponent();
        }

        public NavigatorStringSelectionNode(Node node) : this()
        {
            nodeText.Text = node.Name;
            Data = node;

            var dropdownValues = new List<string>();
            if (node.HasEnums)
            {
                var schema = node.Schema;
                dropdownValues.AddRange(schema.Enum.Select(x => x.ToString()));
            }
            else
            {
                var parentSchemaNode = node.Parent;
                if (parentSchemaNode != null)
                {
                    var schema = parentSchemaNode.Schema;
                    if (schema != null)
                    {
                        Node.GetOptionsForKey(node.Name, schema, dropdownValues);
                    }
                }
            }

            valueControl.Items.AddRange(dropdownValues.ToArray());
            var val = (node.Tag as JProperty).Value as JValue;
            valueControl.SelectedItem = val.ToString();

            valueControl.SelectedIndexChanged += ValueControl_SelectedIndexChanged;

            var desc = Data.Schema?.Description;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = SchemaManager.GetDescription(node.Name);
            }
            descLabel.Text = desc;
            toolTip.SetToolTip(descLabel, desc);
        }

        public NavigatorStringSelectionNode(Node node, IEnumerable<string> dropdownValues) : this()
        {
            nodeText.Text = node.Name;
            Data = node;

            valueControl.Items.AddRange(dropdownValues.ToArray());
            var val = (node.Tag as JProperty).Value as JValue;
            valueControl.SelectedItem = val.ToString();

            valueControl.SelectedIndexChanged += ValueControl_SelectedIndexChanged;

            toolTip.SetToolTip(nodeText, Data.Schema?.Description);
        }

        private void ValueControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            (Data.Tag as JProperty).Value = new JValue(valueControl.SelectedItem.ToString());

            ValidateData();

            RaisePropertyChangedEvent(new PropertyChangedEventArgs(nodeText.Text));
        }
    }
}
