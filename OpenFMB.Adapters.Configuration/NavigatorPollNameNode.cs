// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OpenFMB.Adapters.Core.Models;
using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models.Schemas;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorPollNameNode : BaseNavigatorNode, IDataNode, INavigatorNode
    { 
        public string Value
        {
            get { return valueControl.SelectedItem?.ToString(); }
        }

        public NavigatorPollNameNode()
        {
            InitializeComponent();            
        }

        public NavigatorPollNameNode(Node node, IEnumerable<string> dropdownValues) : this()
        {
            nodeText.Text = node.Name;
            Data = node;                                                      

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

        private void ValueControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            (Data.Tag as JProperty).Value = new JValue(valueControl.SelectedItem.ToString());

            ValidateData();

            RaisePropertyChangedEvent(new PropertyChangedEventArgs(nodeText.Text));
        }
    }
}
