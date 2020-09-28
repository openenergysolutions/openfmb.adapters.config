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

using System;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Logging
{
    public class LoggingSection : IYamlNode
    {
        public string Name => "logging";
        public string LoggerName { get; set; } = "application";

        public bool RotatingFileEnable { get; set; } = true;

        public string RotatingFilePath { get; set; } = "adapter.log";

        public int RotatingFileMaxSize { get; set; } = 1048576;

        public int RotatingFileMaxFiles { get; set; } = 3;

        public bool ConsoleEnable { get; set; } = true;

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode();

            node.Add("logger-name", LoggerName);
            node.Add("console", new YamlMappingNode(new YamlScalarNode("enabled"), new YamlScalarNode(ConsoleEnable.ToString().ToLower())));

            node.Add("rotating-file", new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode(RotatingFileEnable.ToString().ToLower()),
                new YamlScalarNode("path"), new YamlScalarNode(RotatingFilePath),
                new YamlScalarNode("max-size"), new YamlScalarNode(RotatingFileMaxSize.ToString()),
                new YamlScalarNode("max-files"), new YamlScalarNode(RotatingFileMaxFiles.ToString()))
            );

            return node;
        }

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;
            LoggerName = (node["logger-name"] as YamlScalarNode).Value;
            ConsoleEnable = ((node["console"] as YamlMappingNode)["enabled"] as YamlScalarNode).Value == "true";

            var rotate = node["rotating-file"] as YamlMappingNode;
            RotatingFileEnable = (rotate["enabled"] as YamlScalarNode).Value == "true";
            RotatingFilePath = (rotate["path"] as YamlScalarNode).Value;
            RotatingFileMaxSize = Convert.ToInt32((rotate["max-size"] as YamlScalarNode).Value);
            RotatingFileMaxFiles = Convert.ToInt32((rotate["max-files"] as YamlScalarNode).Value);
        }
    }
}