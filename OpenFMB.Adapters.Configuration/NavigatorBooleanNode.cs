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
            (Data.Tag as JProperty).Value = new JValue(valueControl.SelectedIndex == 0 ? false : true);

            ValidateData();

            RaisePropertyChangedEvent(new PropertyChangedEventArgs(nodeText.Text));
        }
    }
}
