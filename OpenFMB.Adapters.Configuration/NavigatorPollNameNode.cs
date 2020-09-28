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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenFMB.Adapters.Core.Models;
using System.Text.RegularExpressions;
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
