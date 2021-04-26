// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace OpenFMB.Adapters.Core.Models
{
    public static class NodeExtension
    {
        public static IEnumerable<Node> Traverse(this Node root)
        {
            var stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;
                foreach (var child in current.Nodes)
                {
                    stack.Push(child);
                }
            }
        }
    }
}