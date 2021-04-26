// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class TimescaleDBPlugin : IYamlNode, IPlugin
    {        
        public bool Enabled { get; set; }

        public string Name => PluginsSection.TimescaleDB;

        public int MaxQueuedMessages { get; set; } = 100;

        public string DatabaseUrl { get; set; } = "postgresql://user:password@localhost:5432/dbname";

        public int ConnectRetrySeconds { get; set; } = 5;

        public bool StoreMeasurement { get; set; } = true;

        public string TableName { get; set; } = "data";

        public bool StoreRawMessage { get; set; }

        public string RawTableName { get; set; } = "raw_data";

        public RawDataFormat RawDataFormat { get; set; }

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode();
            node.Add("enabled", Enabled.ToString().ToLower());
            node.Add("database-url", DatabaseUrl);
            node.Add("store-measurement", StoreMeasurement.ToString().ToLower());
            node.Add("table-name", TableName);
            node.Add("store-raw-message", StoreRawMessage.ToString().ToLower());
            node.Add("raw-table-name", RawTableName);
            node.Add("raw-data-format", ((int)RawDataFormat).ToString());
            node.Add("max-queued-messages", MaxQueuedMessages.ToString());
            node.Add("connect-retry-seconds", ConnectRetrySeconds.ToString());

            return node;
        }

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;
            Enabled = (node["enabled"] as YamlScalarNode).Value == "true";
            MaxQueuedMessages = Convert.ToInt32((node["max-queued-messages"] as YamlScalarNode).Value);
            DatabaseUrl = (node["database-url"] as YamlScalarNode).Value;
            ConnectRetrySeconds = Convert.ToInt32((node["connect-retry-seconds"] as YamlScalarNode).Value);
            StoreMeasurement = (node["store-measurement"] as YamlScalarNode).Value == "true";
            StoreRawMessage = (node["store-raw-message"] as YamlScalarNode).Value == "true";
            TableName = (node["table-name"] as YamlScalarNode).Value;
            RawTableName = (node["raw-table-name"] as YamlScalarNode).Value;
            RawDataFormat = (RawDataFormat)Convert.ToInt32((node["raw-data-format"] as YamlScalarNode).Value);
        }
    }

    public enum RawDataFormat
    {
        JSON,
        Protobuf
    }
}