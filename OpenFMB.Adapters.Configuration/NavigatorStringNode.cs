// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorStringNode : BaseNavigatorNode, IDataNode, INavigatorNode
    {        
        public bool ReadOnly
        {
            get { return valueControl.ReadOnly; }
            set { valueControl.ReadOnly = value; }
        }

        public NavigatorStringNode()
        {
            InitializeComponent();            
        }

        public NavigatorStringNode(Node node) : this()
        {
            nodeText.Text = node.Name;
            Data = node;
            if (Data.Tag is JProperty)
            {
                valueControl.Text = (Data.Tag as JProperty).Value?.ToString();
            }
            else if (Data.Tag is JValue)
            {
                valueControl.Text = (Data.Tag as JValue).Value?.ToString();
            }
            valueControl.TextChanged += TextBox_TextChanged;

            var desc = Data.Schema?.Description;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = SchemaManager.GetDescription(node.Name);
            }
            descLabel.Text = desc;
            toolTip.SetToolTip(descLabel, desc);
        }

        public NavigatorStringNode(Node node, string description) : this(node)
        {
            descLabel.Text = description;
            toolTip.SetToolTip(descLabel, description);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Data.Tag is JProperty)
                {
                    (Data.Tag as JProperty).Value = new JValue(valueControl.Text.Trim());
                }
                else if (Data.Tag is JValue)
                {
                    (Data.Tag as JValue).Value = valueControl.Text.Trim();
                }

                ValidateData();

                RaisePropertyChangedEvent(new System.ComponentModel.PropertyChangedEventArgs(nodeText.Text));
            }
            catch { }
        }
    }
}
