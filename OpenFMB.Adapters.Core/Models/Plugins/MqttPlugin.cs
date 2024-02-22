// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class MqttPlugin : IYamlNode, IPlugin, ITransportPlugin
    {
        public bool Enabled { get; set; }

        public string Name => PluginsSection.Mqtt;

        public int MaxQueuedMessages { get; set; } = 100;

        public string ConnectUrl { get; set; } = "127.0.0.1";

        public string ClientId { get; set; } = "client1";

        public int ConnectRetryMs { get; set; } = 5000;

        public MqttSecurity Security { get; } = new MqttSecurity();

        public List<ITopic> Publishes { get; } = new List<ITopic>();

        public List<ITopic> Subscribes { get; } = new List<ITopic>();

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode
            {
                { "enabled", Enabled.ToString().ToLower() },
                { "max-queued-messages", MaxQueuedMessages.ToString() },
                { "server-address", ConnectUrl },
                { "client-id", ClientId },
                { "connect-retry-delay-ms", ConnectRetryMs.ToString() },

                {
                    "security",
                    new YamlMappingNode(
                        new YamlScalarNode("security-type"), new YamlScalarNode(Security.SecurityType.ToString().ToLower()),
                        new YamlScalarNode("ca-trusted-cert-file"), new YamlScalarNode(Security.CertFile),
                        new YamlScalarNode("client-private-key-file"), new YamlScalarNode(Security.ClientKey),
                        new YamlScalarNode("client-cert-chain-file"), new YamlScalarNode(Security.ClientCert),
                        new YamlScalarNode("username"), new YamlScalarNode(Security.Username),
                        new YamlScalarNode("password"), new YamlScalarNode(Security.Password))
                }
            };

            var publish = new YamlSequenceNode();
            node.Add("publish", publish);

            foreach (var p in Publishes)
            {
                publish.Add(new YamlMappingNode(
                    new YamlScalarNode("profile"), new YamlScalarNode(p.Profile),
                    new YamlScalarNode("topic-suffix"), new YamlScalarNode(p.Subject)));
            }

            var subscribe = new YamlSequenceNode();
            node.Add("subscribe", subscribe);

            foreach (var p in Subscribes)
            {
                subscribe.Add(new YamlMappingNode(
                    new YamlScalarNode("profile"), new YamlScalarNode(p.Profile),
                    new YamlScalarNode("topic-suffix"), new YamlScalarNode(p.Subject)));
            }

            return node;
        }

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;
            Enabled = (node["enabled"] as YamlScalarNode).Value == "true";
            MaxQueuedMessages = Convert.ToInt32((node["max-queued-messages"] as YamlScalarNode).Value);
            ConnectUrl = (node["server-address"] as YamlScalarNode).Value;
            ClientId = (node["client-id"] as YamlScalarNode).Value;
            ConnectRetryMs = Convert.ToInt32((node["connect-retry-delay-ms"] as YamlScalarNode).Value);

            if (node.GetValueByKey("security") is YamlMappingNode security)
            {
                try
                {
                    Security.SecurityType = (SecurityType)Enum.Parse(typeof(SecurityType), (security["security-type"] as YamlScalarNode).Value);
                    Security.CertFile = (security["ca-trusted-cert-file"] as YamlScalarNode).Value;
                    Security.ClientKey = (security["client-private-key-file"] as YamlScalarNode).Value;
                    Security.ClientCert = (security["client-cert-chain-file"] as YamlScalarNode).Value;
                    Security.Username = (security["username"] as YamlScalarNode).Value;
                    Security.Password = (security["password"] as YamlScalarNode).Value;
                }
                catch { }
            }

            Publishes.Clear();

            var publishes = node["publish"] as YamlSequenceNode;
            foreach (YamlMappingNode p in publishes.Cast<YamlMappingNode>())
            {
                Publishes.Add(new Publish()
                {
                    Profile = (p["profile"] as YamlScalarNode).Value,
                    Subject = (p["topic-suffix"] as YamlScalarNode).Value
                });
            }

            Subscribes.Clear();

            var subscribes = node["subscribe"] as YamlSequenceNode;
            foreach (YamlMappingNode p in subscribes.Cast<YamlMappingNode>())
            {
                Subscribes.Add(new Subscribe()
                {
                    Profile = (p["profile"] as YamlScalarNode).Value,
                    Subject = (p["topic-suffix"] as YamlScalarNode).Value
                });
            }
        }
    }

    public class MqttSecurity
    {
        public SecurityType SecurityType { get; set; } = SecurityType.tls_mutual_auth;

        public string CertFile { get; set; } = "cert.pem";

        public string ClientKey { get; set; } = "client_key.pem";

        public string ClientCert { get; set; } = "client_cert.pem";

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";
    }
}
