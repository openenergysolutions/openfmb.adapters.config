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

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using System.Drawing;

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
