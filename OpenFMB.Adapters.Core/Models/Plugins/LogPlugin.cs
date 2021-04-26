// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class LogPlugin : IYamlNode, IPlugin
    {
        public bool Enabled { get; set; }
        public bool LogDebugString { get; set; }
        public List<LogFilter> Filters { get; } = new List<LogFilter>();
        public string Name => PluginsSection.Log;        

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode();
            node.Add("enabled", Enabled.ToString().ToLower());
            node.Add("log_debug_string", LogDebugString.ToString().ToLower());
            node.Add("filters", new YamlSequenceNode());

            return node;
        }

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;
            if (node.GetValueByKey("enabled") is YamlScalarNode)
            {
                Enabled = (node.GetValueByKey("enabled") as YamlScalarNode).Value == "true";
            }
            if (node.GetValueByKey("log_debug_string") is YamlScalarNode)
            {
                LogDebugString = (node.GetValueByKey("log_debug_string") as YamlScalarNode).Value == "true";
            }
        }
    }

    public class LogFilter
    {
    }
}