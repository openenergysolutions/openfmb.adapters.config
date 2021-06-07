// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public class DataTreeNode : TreeNode, IDataNode
    {        
        private Node data;

        public Node Data
        {
            get { return data; }
            private set
            {
                data = value;
                data.DataNode = this;                
                Update();
                data.OnValueChanged -= Data_OnValueChanged;
                data.OnValueChanged += Data_OnValueChanged;
            }
        }

        public bool IsArrayNode
        {
            get
            {
                if (!string.IsNullOrEmpty(Data?.Path))
                {
                    return Utils.ArrayNode.IsMatch(Data.Path);
                }
                return false;
            }
        }

        private void Data_OnValueChanged(object sender, System.EventArgs e)
        {
            Update();
        }

        internal void Update()
        {
            var val = data.Value;
            if (val != null)
            {
                Text = $"{data.Name}: {val}";
            }

            if (IsValid)
            {
                this.SetNormalForeColor();
            }
            else
            {
                this.SetErrorForeColor();
            }
        }

        public bool IsValid
        {
            get { return Data.IsValid; }            
        }

        public DataTreeNode(Node data) : base(data.Name)
        {            
            Data = data;
            ToolTipText = data.Schema?.Description;            
        }

        public IEnumerable<DataTreeNode> GetAllLeafNodes()
        {
            return GetAllNodes().Where(x => x.Nodes.Count == 0).ToList();
        }

        public IEnumerable<DataTreeNode> GetAllNodes()
        {
            var allNodes = Nodes
                .Cast<DataTreeNode>()
                .SelectMany(GetNodeBranch);

            return allNodes;
        }

        private IEnumerable<DataTreeNode> GetNodeBranch(DataTreeNode node)
        {
            yield return node;

            foreach (DataTreeNode child in node.Nodes)
                foreach (var childChild in GetNodeBranch(child))
                    yield return childChild;
        }
    }
}
