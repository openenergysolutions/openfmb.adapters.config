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
using System.Text;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class PluginsSection : IYamlNode
    {
        public const string Capture = "capture";
        public const string Replay = "replay";
        public const string Dnp3Master = "dnp3-master";
        public const string Dnp3Outstation = "dnp3-outstation";
        public const string ModbusMaster = "modbus-master";
        public const string ModbusOutstation = "modbus-outstation";
        public const string Log = "log";
        public const string GoosePub = "goose-pub";
        public const string GooseSub = "goose-sub";
        public const string IccpClient = "iccp-client";
        public const string IccpServer = "iccp-server";
        public const string Mqtt = "mqtt";
        public const string Nats = "nats";
        public const string TimescaleDB = "timescaledb";

        public CapturePlugin CapturePlugin { get; } = new CapturePlugin();

        public ReplayPlugin ReplayPlugin { get; } = new ReplayPlugin();

        public Dnp3MasterPlugin Dnp3MasterPlugin { get; } = new Dnp3MasterPlugin();

        public Dnp3OutstationPlugin Dnp3SlavePlugin { get; } = new Dnp3OutstationPlugin();

        public ModbusMasterPlugin ModbusMasterPlugin { get; } = new ModbusMasterPlugin();
        public ModbusOutstationPlugin ModbusOutstationPlugin { get; } = new ModbusOutstationPlugin();

        //public LogPlugin LogPlugin { get; } = new LogPlugin();

        public GoosePubPlugin GoosePubPlugin { get; } = new GoosePubPlugin();

        public GooseSubPlugin GooseSubPlugin { get; } = new GooseSubPlugin();

        public IccpClientPlugin IccpClientPlugin { get; } = new IccpClientPlugin();

        public IccpServerPlugin IccpServerPlugin { get; } = new IccpServerPlugin();

        public NatsPlugin NatsPlugin { get; } = new NatsPlugin();

        public MqttPlugin MqttPlugin { get; } = new MqttPlugin();

        public TimescaleDBPlugin TimescaleDBPlugin { get; } = new TimescaleDBPlugin();

        public string Name => "plugins";

        [Newtonsoft.Json.JsonIgnore]
        public readonly IList<IPlugin> Plugins = new List<IPlugin>();
       

        public PluginsSection()
        {
            Plugins.Add(CapturePlugin);
            Plugins.Add(ReplayPlugin);
            Plugins.Add(Dnp3MasterPlugin);
            Plugins.Add(Dnp3SlavePlugin);
            Plugins.Add(ModbusMasterPlugin);
            Plugins.Add(ModbusOutstationPlugin);
            //Plugins.Add(LogPlugin);
            Plugins.Add(GoosePubPlugin);
            Plugins.Add(GooseSubPlugin);
            Plugins.Add(IccpClientPlugin);
            Plugins.Add(IccpServerPlugin);
            Plugins.Add(MqttPlugin);
            Plugins.Add(NatsPlugin);
            Plugins.Add(TimescaleDBPlugin);
        }


        public YamlNode ToYaml()
        {
            var node = new YamlMappingNode();

            foreach(var p in Plugins)
            {
                node.Add(p.Name, p.ToYaml());
            }           

            return node;
        }       

        public void FromYaml(YamlNode yamlNode)
        {
            YamlMappingNode node = yamlNode as YamlMappingNode;

            foreach(var p in Plugins)
            {
                if (p.Name == ModbusMaster)
                {
                    if (node.ContainsKey(p.Name))
                    {
                        p.FromYaml(node[p.Name]);
                    }
                    else if (node.ContainsKey("modbus"))
                    {
                        p.FromYaml(node["modbus"]);
                    }
                }
                else if (p.Name == Dnp3Master)
                {
                    if (node.ContainsKey(p.Name))
                    {
                        p.FromYaml(node[p.Name]);
                    }
                    else if (node.ContainsKey("dnp3"))
                    {
                        p.FromYaml(node["dnp3"]);
                    }
                }
                else
                {
                    if (node.ContainsKey(p.Name))
                    {
                        p.FromYaml(node[p.Name]);
                    }
                }
            }                         
        }
    }
}
