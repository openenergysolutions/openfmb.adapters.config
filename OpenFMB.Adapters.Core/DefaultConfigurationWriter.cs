// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.IO;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core
{
    public static class DefaultConfigurationWriter
    {
        private static YamlMappingNode CreateDefaultLoggingConfig()
        {
            var node = new YamlMappingNode();

            node.Add("logger-name", "application");
            node.Add("console", new YamlMappingNode(new YamlScalarNode("enabled"), new YamlScalarNode("true")));

            node.Add("rotating-file", new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode("false"),
                new YamlScalarNode("path"), new YamlScalarNode("adapter.log"),
                new YamlScalarNode("max-size"), new YamlScalarNode("1048576"),
                new YamlScalarNode("max-files"), new YamlScalarNode("3"))
            );

            return node;
        }

        private static YamlMappingNode CreateCapturePluginNode()
        {
            return new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode("false"),
                new YamlScalarNode("file"), new YamlScalarNode("capture.txt"));
        }

        private static YamlMappingNode CreateReplayPluginNode()
        {
            return new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode("false"),
                new YamlScalarNode("file"), new YamlScalarNode("capture.txt"));
        }

        private static YamlMappingNode CreateDnp3PluginNode()
        {
            return new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode("false"),
                new YamlScalarNode("thread-pool-size"), new YamlScalarNode("capture.txt"),
                new YamlScalarNode("masters"), new YamlMappingNode(
                    new YamlScalarNode("path"), new YamlScalarNode("dnp3-master-template.yaml"),
                    new YamlScalarNode("overrides"), new YamlSequenceNode(
                        new YamlScalarNode("a.b.c = 4"))));
        }

        private static YamlMappingNode CreateGoosePluginNode()
        {
            var node = new YamlMappingNode();
            node.Add("enabled", "false");
            node.Add("goCb", new YamlSequenceNode(
                new YamlMappingNode(
                    new YamlScalarNode("path"), new YamlScalarNode("goCb-template.yaml"),
                    new YamlScalarNode("overrides"), new YamlSequenceNode(
                        new YamlScalarNode("a.b.c = 4")))));
            return node;
        }

        private static YamlMappingNode CreateLogPluginNode()
        {
            var node = new YamlMappingNode();

            return node;
        }

        private static YamlMappingNode CreateModbusPluginNode()
        {
            return new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode("false"),
                new YamlScalarNode("thread-pool-size"), new YamlScalarNode("capture.txt"),
                new YamlScalarNode("sessions"), new YamlMappingNode(
                    new YamlScalarNode("path"), new YamlScalarNode("dnp3-master-template.yaml"),
                    new YamlScalarNode("overrides"), new YamlSequenceNode(
                        new YamlScalarNode("a.b.c = 4"))));
        }

        private static YamlMappingNode CreateNatsPluginNode()
        {
            var node = new YamlMappingNode();
            node.Add("enabled", "false");
            node.Add("max-queued-messages", "100");
            node.Add("connect-url", "nats://localhost:4222");
            node.Add("connect-retry-seconds", "5");

            node.Add("security", new YamlMappingNode(
                new YamlScalarNode("security-type"), new YamlScalarNode("none"),
                new YamlScalarNode("ca-trusted-cert-file"), new YamlScalarNode("cert.pem"),
                new YamlScalarNode("client-private-key-file"), new YamlScalarNode("client_key.pem"),
                new YamlScalarNode("client-cert-chain-file"), new YamlScalarNode("client_cert.pem")));

            var publish = new YamlSequenceNode();
            node.Add("publish", publish);

            publish.Add(new YamlMappingNode(
                new YamlScalarNode("profile"), new YamlScalarNode("SwitchReadingProfile"),
                new YamlScalarNode("subject"), new YamlScalarNode("*")));
            publish.Add(new YamlMappingNode(
                new YamlScalarNode("profile"), new YamlScalarNode("SwitchStatusProfile"),
                new YamlScalarNode("subject"), new YamlScalarNode("*")));

            var subscribe = new YamlSequenceNode();
            node.Add("subscribe", subscribe);

            subscribe.Add(new YamlMappingNode(
                new YamlScalarNode("profile"), new YamlScalarNode("SwitchControlProfile"),
                new YamlScalarNode("subject"), new YamlScalarNode("*")));

            return node;
        }

        private static YamlMappingNode CreateTimescaleDbPluginNode()
        {
            var node = new YamlMappingNode();
            node.Add("enabled", "false");
            node.Add("database-url", "postgresql://user:password@localhost:5432/dbname");
            node.Add("store-measurement", "true");
            node.Add("table-name", "data");
            node.Add("store-raw-message", "false");
            node.Add("raw-table-name", "raw_data");
            node.Add("raw-data-format", "0");
            node.Add("max-queued-messages", "100");
            node.Add("connect-retry-seconds", "5");
            node.Add("data-store-interval-seconds", "0");

            return node;
        }

        private static YamlMappingNode CreateDefaultPluginConfig()
        {
            var node = new YamlMappingNode();

            // capture
            node.Add("capture", CreateCapturePluginNode());

            // dnp3
            node.Add("dnp3", CreateDnp3PluginNode());

            // goose-pub
            node.Add("goose-pub", CreateGoosePluginNode());

            // goose-sub
            node.Add("goose-sub", CreateGoosePluginNode());

            // log
            node.Add("log", CreateLogPluginNode());

            // modbus
            node.Add("modbus", CreateModbusPluginNode());

            // nats
            node.Add("nats", CreateNatsPluginNode());

            // replay
            node.Add("replay", CreateReplayPluginNode());

            // timescaledb
            node.Add("timescaledb", CreateTimescaleDbPluginNode());

            return node;
        }

        public static void WriteDefaultConfig(string filePath)
        {
            var stream = new YamlStream();
            var root = new YamlMappingNode();
            var doc = new YamlDocument(root);

            stream.Add(doc);

            // Logging
            root.Add("logging", CreateDefaultLoggingConfig());

            // Plugins
            root.Add("plugins", CreateDefaultPluginConfig());

            using (var writer = new StreamWriter(filePath))
            {
                stream.Save(writer, assignAnchors: false);
            }
        }
    }
}