using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public static class TreeNodeExtensions
    {
        public static readonly Color EnabledColor = Color.Black;
        public static readonly Color DisabledColor = Color.Gray;
        public static readonly Color ErrorColor = Color.Red;

        public static void SetErrorForeColor(this TreeNode node)
        {
            node.ForeColor = ErrorColor;
        }

        public static void SetNormalForeColor(this TreeNode node)
        {
            node.ForeColor = EnabledColor;
        }

        public static void SetDisabledForeColor(this TreeNode node)
        {
            node.ForeColor = DisabledColor;
        }

        public static IEnumerable<TreeNode> GetAllNodes(this TreeNode node)
        {
            var allNodes = node.Nodes
                .Cast<TreeNode>()
                .SelectMany(GetNodeBranch);

            return allNodes;
        }

        private static IEnumerable<TreeNode> GetNodeBranch(TreeNode node)
        {
            yield return node;

            foreach (DataTreeNode child in node.Nodes)
                foreach (var childChild in GetNodeBranch(child))
                    yield return childChild;
        }
    }
}
