// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class ZenohPlugin : IYamlNode, IPlugin, ITransportPlugin
    {
        public bool Enabled { get; set; }

        public string Name => PluginsSection.Zenoh;        

        public int MaxQueuedMessages { get; set; } = 100;

        public int ConnectRetrySeconds { get; set; } = 5;

        public List<ITopic> Publishes { get; } = new List<ITopic>();

        public List<ITopic> Subscribes { get; } = new List<ITopic>();

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode();
            node.Add("enabled", Enabled.ToString().ToLower());
            node.Add("max-queued-messages", MaxQueuedMessages.ToString());
            node.Add("connect-retry-seconds", ConnectRetrySeconds.ToString());

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
            ConnectRetrySeconds = Convert.ToInt32((node["connect-retry-seconds"] as YamlScalarNode).Value);

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
}
