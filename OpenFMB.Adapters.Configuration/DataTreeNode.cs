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

using OpenFMB.Adapters.Core.Models;
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
