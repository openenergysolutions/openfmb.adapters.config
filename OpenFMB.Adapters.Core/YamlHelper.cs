// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core
{
    public static class YamlHelper
    {
        public static object GetValueByKey(this YamlMappingNode node, string key)
        {
            object obj = null;
            if (node.Children.ContainsKey(key))
            {
                obj = node[key];
            }
            return obj;                
        }

        public static bool ContainsKey(this YamlMappingNode node, string key)
        {
            return node.Children.ContainsKey(key);
        }
    }
}
