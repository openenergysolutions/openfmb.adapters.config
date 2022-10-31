// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
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
        public const string IEC61850Client = "IEC61850-client";
        public const string IEC61850Server = "IEC61850-server";
        public const string IccpClient = "iccp-client";
        public const string IccpServer = "iccp-server";
        public const string Mqtt = "mqtt";
        public const string Nats = "nats";
        public const string Zenoh = "zenoh";
        public const string TimescaleDB = "timescaledb";

        public static readonly List<string> ClientPlugins = new List<string>() { Dnp3Master, ModbusMaster, IccpClient };
        public static readonly List<string> ServerPlugins = new List<string>() { Dnp3Outstation, ModbusOutstation, IccpServer };

        public CapturePlugin CapturePlugin { get; } = new CapturePlugin();

        public ReplayPlugin ReplayPlugin { get; } = new ReplayPlugin();

        public Dnp3MasterPlugin Dnp3MasterPlugin { get; } = new Dnp3MasterPlugin();

        public Dnp3OutstationPlugin Dnp3SlavePlugin { get; } = new Dnp3OutstationPlugin();

        public ModbusMasterPlugin ModbusMasterPlugin { get; } = new ModbusMasterPlugin();
        public ModbusOutstationPlugin ModbusOutstationPlugin { get; } = new ModbusOutstationPlugin();

        //public LogPlugin LogPlugin { get; } = new LogPlugin();

        public IEC61850ClientPlugin IEC61850ClientPlugin { get; } = new IEC61850ClientPlugin();

        public IEC61850ServerPlugin IEC61850ServerPlugin { get; } = new IEC61850ServerPlugin();

        public IccpClientPlugin IccpClientPlugin { get; } = new IccpClientPlugin();

        public IccpServerPlugin IccpServerPlugin { get; } = new IccpServerPlugin();

        public NatsPlugin NatsPlugin { get; } = new NatsPlugin();

        public MqttPlugin MqttPlugin { get; } = new MqttPlugin();

        public ZenohPlugin ZenohPlugin { get; } = new ZenohPlugin();

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

            if (System.Configuration.ConfigurationManager.AppSettings["IEC61850Support"] == "True")
            {
                Plugins.Add(IEC61850ClientPlugin);
                Plugins.Add(IEC61850ServerPlugin);
            }
            if (System.Configuration.ConfigurationManager.AppSettings["ICCPSupport"] == "True") { 
                Plugins.Add(IccpClientPlugin);
                Plugins.Add(IccpServerPlugin);
            }
            Plugins.Add(MqttPlugin);
            Plugins.Add(NatsPlugin);

            if (System.Configuration.ConfigurationManager.AppSettings["ZenohSupport"] == "True")
            {
                Plugins.Add(ZenohPlugin);
            }
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

        public static bool IsClientPlugin(string pluginName)
        {
            return ClientPlugins.Contains(pluginName);
        }

        public static bool IsServerPlugin(string pluginName)
        {
            return ServerPlugins.Contains(pluginName);
        }
    }
}
