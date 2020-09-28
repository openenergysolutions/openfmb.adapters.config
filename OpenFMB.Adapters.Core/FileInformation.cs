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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core
{
    [Serializable]
    public class FileInformation : IYamlNode
    {       
        [JsonProperty("id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConfigFileType Id { get; set; }

        [JsonProperty("edition")]
        public string Edition { get; set; } = string.Empty;

        [JsonProperty("version")]
        public string Version { get; set; } = string.Empty;    

        [JsonProperty("plugin")]
        public string Plugin { get; set; } = string.Empty;

        [JsonIgnore]
        public string Name => "file";

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;

            if (node.ContainsKey("id"))
            {
                Id = ConfigFileTypeString.Convert((node["id"] as YamlScalarNode).Value);
            }

            if (node.ContainsKey("edition"))
            {                
                Edition = (node["edition"] as YamlScalarNode).Value;                
            }

            if (node.ContainsKey("version"))
            {                
                Version = (node["version"] as YamlScalarNode).Value;
            }

            if (node.ContainsKey("plugin"))
            {
                Plugin = (node["plugin"] as YamlScalarNode).Value;
            }
        }

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode();

            node.Add("id", ConfigFileTypeString.ToString(Id));

            var edition = string.IsNullOrWhiteSpace(Edition) ? SchemaManager.DefaultVersion : Edition;
            var version = string.IsNullOrWhiteSpace(Version) ? ConfigurationManager.Version : Version;

            node.Add("edition", edition);
            node.Add("version", version);
            node.Add("plugin", Plugin);

            return node;
        }
    }
}
