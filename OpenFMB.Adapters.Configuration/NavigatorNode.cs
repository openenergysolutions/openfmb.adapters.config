// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorNode : BaseNavigatorNode, IDataNode, INavigatorNode
    { 
        public NavigatorNode()
        {
            InitializeComponent();
        }

        public NavigatorNode(Node node) : this()
        {
            nodeText.Text = node.Name;
            Data = node;

            var desc = Data.Schema?.Description;
            if (string.IsNullOrWhiteSpace(desc))
            {
                desc = SchemaManager.GetDescription(node.Name);
            }
            descLabel.Text = desc;
            toolTip.SetToolTip(descLabel, desc);

            if (navButton.Visible)
            {
                nodeText.Click += (sender, e) =>
                {
                    RaiseDrillDownEvent();
                };
            }
        }
    }
}
