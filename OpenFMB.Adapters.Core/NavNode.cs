// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenFMB.Adapters.Core
{
    public class NavNode
    {
        public NavNode(string name)
        {
            Name = name;
        }

        [ReadOnly(true)]
        [Description("Name of the node")]
        public string Name { get; set; }

        [Browsable(false)]
        public NavNode Parent { get; set; }


        [Browsable(false)]
        public List<NavNode> Nodes { get; } = new List<NavNode>();

        [Browsable(false)]
        public object Tag { get; set; }

        [Browsable(false)]
        public Node SchemaNode { get; set; }

        public string Path
        {
            get
            {
                var p = Name;
                if (Parent != null)
                {
                    p = $"{Parent.Path}.{p}";
                }
                return p;
            }
        }

        public void AddNode(NavNode node)
        {
            Nodes.Add(node);
            node.Parent = this;
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
