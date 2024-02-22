// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public abstract class BaseSessionablePlugin
    {
        public abstract string SessionTagName { get; }
        public abstract string Name { get; }

        public List<Session> Sessions { get; } = new List<Session>();

        public bool Enabled { get; set; }
        public int ThreadPoolSize { get; set; } = 1;

        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode(
                new YamlScalarNode("enabled"), new YamlScalarNode(Enabled.ToString().ToLower()),
                new YamlScalarNode("thread-pool-size"), new YamlScalarNode(ThreadPoolSize.ToString()));

            var masters = new YamlSequenceNode();
            node.Add(SessionTagName, masters);

            foreach (var session in Sessions)
            {
                var yaml = session.ToYaml();
                masters.Add(yaml);
            }

            return node;
        }

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;

            if (node.ContainsKey("enabled"))
            {
                Enabled = (node["enabled"] as YamlScalarNode).Value == "true";
            }

            if (node.ContainsKey("thread-pool-size"))
            {
                ThreadPoolSize = Convert.ToInt32((node["thread-pool-size"] as YamlScalarNode).Value);
            }

            Sessions.Clear();

            var seq = node[SessionTagName] as YamlSequenceNode;
            foreach (YamlMappingNode master in seq.Children.Cast<YamlMappingNode>())
            {
                var session = Session.FromYaml(Name, master);
                Sessions.Add(session);
                session.Index = Sessions.Count - 1;
            }
        }
    }
}
