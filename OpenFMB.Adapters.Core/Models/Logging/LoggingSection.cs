// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

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
            var node = new YamlMappingNode
            {
                { "logger-name", LoggerName },
                { "console", new YamlMappingNode(new YamlScalarNode("enabled"), new YamlScalarNode(ConsoleEnable.ToString().ToLower())) },

                {
                    "rotating-file",
                    new YamlMappingNode(
                        new YamlScalarNode("enabled"), new YamlScalarNode(RotatingFileEnable.ToString().ToLower()),
                        new YamlScalarNode("path"), new YamlScalarNode(RotatingFilePath),
                        new YamlScalarNode("max-size"), new YamlScalarNode(RotatingFileMaxSize.ToString()),
                        new YamlScalarNode("max-files"), new YamlScalarNode(RotatingFileMaxFiles.ToString()))
                }
            };

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