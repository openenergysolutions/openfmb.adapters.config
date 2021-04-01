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
using OpenFMB.Adapters.Core.Utility;
using System;
using System.ComponentModel;
using System.Drawing;

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
            var val = (Data.Tag as JProperty).Value as JValue;
            if (val.Type == JTokenType.Integer)
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
