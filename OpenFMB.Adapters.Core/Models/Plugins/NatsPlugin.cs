// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class NatsPlugin : IYamlNode, IPlugin, ITransportPlugin
    {
        public bool Enabled { get; set; }

        public string Name => PluginsSection.Nats;        

        public int MaxQueuedMessages { get; set; } = 100;

        public string ConnectUrl { get; set; } = "nats://localhost:4222";

        public int ConnectRetrySeconds { get; set; } = 5;

        public NatsSecurity Security { get; } = new NatsSecurity();

        public List<ITopic> Publishes { get; } = new List<ITopic>();

        public List<ITopic> Subscribes { get; } = new List<ITopic>();

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode();
            node.Add("enabled", Enabled.ToString().ToLower());
            node.Add("max-queued-messages", MaxQueuedMessages.ToString());
            node.Add("connect-url", ConnectUrl);
            node.Add("connect-retry-seconds", ConnectRetrySeconds.ToString());

            node.Add("security", new YamlMappingNode(
                new YamlScalarNode("security-type"), new YamlScalarNode(Security.SecurityType.ToString().ToLower()),
                new YamlScalarNode("ca-trusted-cert-file"), new YamlScalarNode(Security.CertFile),
                new YamlScalarNode("client-private-key-file"), new YamlScalarNode(Security.ClientKey),
                new YamlScalarNode("client-cert-chain-file"), new YamlScalarNode(Security.ClientCert),
                new YamlScalarNode("jwt-creds-file"), new YamlScalarNode(Security.JwtCredsFile)));

            var publish = new YamlSequenceNode();
            node.Add("publish", publish);

            foreach(var p in Publishes)
            {
                publish.Add(new YamlMappingNode(
                    new YamlScalarNode("profile"), new YamlScalarNode(p.Profile),
                    new YamlScalarNode("subject"), new YamlScalarNode(p.Subject)));
            }            

            var subscribe = new YamlSequenceNode();
            node.Add("subscribe", subscribe);

            foreach (var p in Subscribes)
            {
                subscribe.Add(new YamlMappingNode(
                    new YamlScalarNode("profile"), new YamlScalarNode(p.Profile),
                    new YamlScalarNode("subject"), new YamlScalarNode(p.Subject)));
            }

            return node;
        }

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;
            Enabled = (node["enabled"] as YamlScalarNode).Value == "true";
            MaxQueuedMessages = Convert.ToInt32((node["max-queued-messages"] as YamlScalarNode).Value);
            ConnectUrl = (node["connect-url"] as YamlScalarNode).Value;
            ConnectRetrySeconds = Convert.ToInt32((node["connect-retry-seconds"] as YamlScalarNode).Value);

            var security = node.GetValueByKey("security") as YamlMappingNode;
            if (security != null)
            { 
                try
                {
                    Security.SecurityType = (SecurityType)Enum.Parse(typeof(SecurityType), (security["security-type"] as YamlScalarNode)?.Value);
                    Security.CertFile = (security["ca-trusted-cert-file"] as YamlScalarNode)?.Value;
                    Security.ClientKey = (security["client-private-key-file"] as YamlScalarNode)?.Value;
                    Security.ClientCert = (security["client-cert-chain-file"] as YamlScalarNode)?.Value;
                    Security.JwtCredsFile = (security["jwt-creds-file"] as YamlScalarNode)?.Value;
                }
                catch { }
            }

            Publishes.Clear();

            var publishes = node["publish"] as YamlSequenceNode;
            foreach(YamlMappingNode p in publishes)
            {
                Publishes.Add(new Publish()
                {
                    Profile = (p["profile"] as YamlScalarNode).Value,
                    Subject = (p["subject"] as YamlScalarNode).Value
                });
            }

            Subscribes.Clear();

            var subscribes = node["subscribe"] as YamlSequenceNode;
            foreach (YamlMappingNode p in subscribes)
            {
                Subscribes.Add(new Subscribe()
                {
                    Profile = (p["profile"] as YamlScalarNode).Value,
                    Subject = (p["subject"] as YamlScalarNode).Value
                });
            }
        }
    }

    public class NatsSecurity
    {
        public SecurityType SecurityType { get; set; } = SecurityType.tls_mutual_auth;

        public string CertFile { get; set; } = "cert.pem";

        public string ClientKey { get; set; } = "client_key.pem";

        public string ClientCert { get; set; } = "client_cert.pem";

        public string JwtCredsFile { get; set; } = "";
    }    
}
