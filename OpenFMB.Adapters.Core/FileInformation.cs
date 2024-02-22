// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

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
            var edition = string.IsNullOrWhiteSpace(Edition) ? SchemaManager.DefaultEdition : Edition;
            var version = string.IsNullOrWhiteSpace(Version) ? ConfigurationManager.Version : Version;

            var node = new YamlMappingNode
            {
                { "id", ConfigFileTypeString.ToString(Id) },
                { "edition", edition },
                { "version", version },
                { "plugin", Plugin }
            };

            return node;
        }
    }
}
