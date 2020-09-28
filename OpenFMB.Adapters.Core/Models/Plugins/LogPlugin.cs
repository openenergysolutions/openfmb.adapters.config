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