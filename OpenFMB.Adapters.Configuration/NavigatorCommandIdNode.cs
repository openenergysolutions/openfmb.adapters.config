// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorCommandIdNode : BaseNavigatorNode, IDataNode, INavigatorNode
    {
        public List<string> CommandIds { get; private set; }

        public string Value
        {
            get { return valueControl.SelectedItem?.ToString(); }
        }

        public NavigatorCommandIdNode()
        {
            InitializeComponent();
        }

        public NavigatorCommandIdNode(Node node, List<string> dropdownValues) : this()
        {
            CommandIds = dropdownValues;

            nodeText.Text = node.Name;
            Data = node;

            valueControl.Items.AddRange(dropdownValues.ToArray());
            var val = (node.Tag as JProperty).Value as JValue;

            if (CommandIds.Contains(val.ToString()))
            {
                valueControl.SelectedItem = val.ToString();

                valueControl.SelectedIndexChanged += ValueControl_SelectedIndexChanged;
            }
            else
            {
                valueControl.SelectedIndexChanged += ValueControl_SelectedIndexChanged;
                CommandIds.Add(val.ToString());
                valueControl.Items.Add(val.ToString());
                valueControl.SelectedItem = val.ToString();
            }

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

        private void AddButton_Click(object sender, EventArgs e)
        {
            CommandIdEditForm form = new CommandIdEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (CommandIds.Contains(form.CommandId))
                {
                    if (valueControl.SelectedItem != null && valueControl.SelectedItem.ToString() != form.CommandId)
                    {
                        valueControl.SelectedItem = form.CommandId;
                    }
                }
                else
                {
                    CommandIds.Add(form.CommandId);
                    valueControl.Items.Add(form.CommandId);
                    valueControl.SelectedItem = form.CommandId;
                }

            }
        }
    }
}
