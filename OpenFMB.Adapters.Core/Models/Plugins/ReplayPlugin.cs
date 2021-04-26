// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class ReplayPlugin : IYamlNode, IPlugin
    {
        public bool Enabled { get; set; }
        public string Name => PluginsSection.Replay;

        public string File { get; set; } = "capture.txt";

        public YamlNode ToYaml()
        {
            return new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode(Enabled.ToString().ToLower()),
                new YamlScalarNode("file"), new YamlScalarNode(File ?? File));
        }

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;
            Enabled = (node["enabled"] as YamlScalarNode).Value == "true";
            File = (node["file"] as YamlScalarNode).Value;
        }
    }
}