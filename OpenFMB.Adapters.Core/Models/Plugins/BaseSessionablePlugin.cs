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

using System;
using System.Collections.Generic;
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
            foreach (YamlMappingNode master in seq.Children)
            {
                var session = Session.FromYaml(Name, master);
                Sessions.Add(session);
                session.Index = Sessions.Count - 1;
            }
        }
    }
}
