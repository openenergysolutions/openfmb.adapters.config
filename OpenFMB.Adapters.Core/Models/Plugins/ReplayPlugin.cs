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