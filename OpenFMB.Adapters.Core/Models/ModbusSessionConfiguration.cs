// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace OpenFMB.Adapters.Core.Models
{
    public class ModbusSessionConfiguration : SessionConfiguration
    {
        private ISessionSpecificConfig _sessionSpecific;
        public override ISessionSpecificConfig SessionSpecificConfig
        {
            get
            {
                if (_sessionSpecific == null)
                {
                    if (PluginName == PluginsSection.ModbusMaster)
                    {
                        _sessionSpecific = new ModbusMasterSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                    else
                    {
                        _sessionSpecific = new ModbusOutstationSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                }
                return _sessionSpecific;
            }
        }

        public ModbusSessionConfiguration(string pluginName, string edition)
        {
            PluginName = pluginName;
            Edition = edition;
        }

        protected override void LoadSessionConfigurationFromJson(string json)
        {
            if (PluginName == PluginsSection.ModbusMaster)
            {
                try
                {
                    _sessionSpecific = JsonConvert.DeserializeObject<ModbusMasterSpecificConfig>(json);
                }
                catch (Exception ex)
                {
                    _logger.Log(Level.Error, ex.Message, ex);
                    _logger.Log(Level.Info, "Unable to parse session configuration.  Will try to parse manually.");

                    ModbusMasterSpecificConfig config = new ModbusMasterSpecificConfig(Edition);

                    var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

                    if (jsonObject.ContainsKey("name"))
                    {
                        config.Name = jsonObject["name"].ToString();
                    }
                    if (jsonObject.ContainsKey("log-level"))
                    {
                        if (Enum.TryParse(jsonObject["log-level"].ToString(), out LogLevel level))
                        {
                            config.LogLevel = level;
                        }
                    }
                    if (jsonObject.ContainsKey("remote-ip"))
                    {
                        config.Name = jsonObject["remote-ip"].ToString();
                    }
                    if (jsonObject.ContainsKey("port"))
                    {
                        if (int.TryParse(jsonObject["name"].ToString(), out int port))
                        {
                            config.Port = port;
                        }
                    }
                    if (jsonObject.ContainsKey("adapter"))
                    {
                        config.Name = jsonObject["adapter"].ToString();
                    }

                    if (jsonObject.ContainsKey("unit-identifier"))
                    {
                        if (int.TryParse(jsonObject["unit-identifier"].ToString(), out int id))
                        {
                            config.UnitIdentifier = id;
                        }
                    }

                    if (jsonObject.ContainsKey("response_timeout_ms"))
                    {
                        if (int.TryParse(jsonObject["response_timeout_ms"].ToString(), out int id))
                        {
                            config.ResponseTimeout = id;
                        }
                    }

                    if (jsonObject.ContainsKey("always-write-multiple-registers"))
                    {
                        config.AlwaysWriteMultipleRegisters = jsonObject["always-write-multiple-registers"].ToString().ToLower() == "true";
                    }

                    if (jsonObject.ContainsKey("auto_polling"))
                    {
                        if (jsonObject["auto_polling"] is JObject autoPolling)
                        {
                            if (autoPolling.ContainsKey("max_register_gaps"))
                            {
                                if (int.TryParse(autoPolling["max_register_gaps"].ToString(), out int id))
                                {
                                    config.AutoPollingMaxRegisterGaps = id;
                                }
                            }
                            if (autoPolling.ContainsKey("max_bit_gaps"))
                            {
                                if (int.TryParse(autoPolling["max_bit_gaps"].ToString(), out int id))
                                {
                                    config.AutoPollingMaxBitGaps = id;
                                }
                            }
                        }
                    }

                    if (jsonObject.ContainsKey("heartbeats"))
                    {
                        if (jsonObject["heartbeats"] is JArray heartbeats)
                        {
                            foreach (JObject h in heartbeats.Cast<JObject>())
                            {
                                var hb = new HeartBeat();
                                if (h.ContainsKey("index"))
                                {
                                    if (int.TryParse(h["index"].ToString(), out int id))
                                    {
                                        hb.Index = id;
                                    }
                                }
                                if (h.ContainsKey("period_ms"))
                                {
                                    if (int.TryParse(h["period_ms"].ToString(), out int id))
                                    {
                                        hb.PeriodMs = id;
                                    }
                                }
                                if (h.ContainsKey("mask"))
                                {
                                    var value = h["mask"].ToString();
                                    hb.Mask = NumberConverter.ToInteger(value);
                                }
                                config.HeartBeats.Add(hb);
                            }
                        }
                    }

                    _sessionSpecific = config;
                }
            }
            else
            {
                try
                {
                    _sessionSpecific = JsonConvert.DeserializeObject<ModbusOutstationSpecificConfig>(json);
                }
                catch (Exception ex)
                {
                    _logger.Log(Level.Error, ex.Message, ex);
                }
            }
        }

        protected override void InitDefaultProfileSettings(Profile profile)
        {

        }
    }

    public class AutoPolling
    {
        [JsonProperty("max_register_gaps")]
        public int MaxRegisterGaps { get; set; } = 0;

        [JsonProperty("max_bit_gaps")]
        public int MaxBitGaps { get; set; } = 0;
    }

    public class HeartBeat
    {
        [JsonProperty("index"), DisplayName("Register Index"), Description("Read this register, invert the masked bits, and write it back.")]
        public int Index { get; set; }
        [JsonProperty("period_ms"), DisplayName("Period (ms)"), Description("Heartbeat period (in milliseconds)")]
        public int PeriodMs { get; set; } = 1000;
        [JsonProperty("mask"), DisplayName("Masked bits"), Description("Mask specifying the bits to invert")]
        public int Mask { get; set; }

        public override string ToString()
        {
            return "Device Heartbeat";
        }
    }

    public class ModbusMasterSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string name = "device1";
        private string channelAdapter = "0.0.0.0";
        private string remoteIp = "0.0.0.0";
        private int port = 502;
        private int unitIdentifier = 1;
        private int responseTimeout = 1000;
        private bool alwaysWriteMultipleRegisters;

        public ModbusMasterSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.ModbusMaster;
        }

        [Category("General"), DisplayName("Device Name"), Description("Name of the device for logging purpose.")]
        [JsonProperty("name")]
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }

        [Category("Logging"), DisplayName("Log Level")]
        [JsonProperty("log-level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevel LogLevel { get; set; } = LogLevel.Warn;

        [Category("Protocol"), DisplayName("Network Adapter"), Description("Default is 0.0.0.0.  Override to use specific network adapter.")]
        [JsonProperty("adapter")]
        public string ChannelAdapter { get => channelAdapter; set { channelAdapter = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Device IP Address")]
        [JsonProperty("remote-ip")]
        public string RemoteIp { get => remoteIp; set { remoteIp = value; NotifyPropertyChanged(); } }
        [Category("Protocol"), DisplayName("Device TCP Port"), Description("The TCP port number.  Default port is 502")]
        [JsonProperty("port")]
        public int Port { get => port; set { port = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Device Identitifer"), Description("MODBUS unit identifier (aka slave address)")]
        [JsonProperty("unit-identifier")]
        public int UnitIdentifier { get => unitIdentifier; set { unitIdentifier = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Response Timeout (ms)"), Description("The response timeout in millisecond.")]
        [JsonProperty("response_timeout_ms")]
        public int ResponseTimeout { get => responseTimeout; set { responseTimeout = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Always Write Multiple Registers"), Description("Specify if Write Multiple Registers to be used for all request.")]
        [JsonProperty("always-write-multiple-registers")]
        public bool AlwaysWriteMultipleRegisters { get => alwaysWriteMultipleRegisters; set { alwaysWriteMultipleRegisters = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Max Register Gaps"), Description("Maximum number of unnecessary registers that can be read when polling for coils and discrete inputs")]
        [JsonIgnore]
        public int AutoPollingMaxRegisterGaps
        {
            get { return AutoPolling.MaxRegisterGaps; }
            set { AutoPolling.MaxRegisterGaps = value; NotifyPropertyChanged(); }
        }

        [Category("Protocol"), DisplayName("Max Bit Gaps"), Description("Maximum number of unnecessary bits that can be read when polling for coils and discrete inputs")]
        [JsonIgnore]
        public int AutoPollingMaxBitGaps
        {
            get { return AutoPolling.MaxBitGaps; }
            set { AutoPolling.MaxBitGaps = value; NotifyPropertyChanged(); }
        }

        [Browsable(false)]
        [JsonProperty("auto_polling")]
        public AutoPolling AutoPolling { get; set; } = new AutoPolling();

        [Category("HeartBeat"), DisplayName("HeartBeat"), Description("Periodic heartbeat configuration")]
        [JsonProperty("heartbeats")]
        public ObservableCollection<HeartBeat> HeartBeats { get; set; } = new ObservableCollection<HeartBeat>();
    }

    public class ModbusOutstationSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string name = "device1";
        private LogLevel logLevel = LogLevel.Warn;
        private string channelAdapter = "0.0.0.0";
        private int port = 502;
        private int unitIdentifier = 1;
        private int maxNumConnections = 1;

        public ModbusOutstationSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.ModbusOutstation;
        }

        [Category("General"), DisplayName("Device Name"), Description("Name of the device for logging purpose.")]
        [JsonProperty("name")]
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }

        [Category("Logging"), DisplayName("Log Level")]
        [JsonProperty("log-level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevel LogLevel { get => logLevel; set { logLevel = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Network Adapter"), Description("Default is 0.0.0.0.  Override to use specific network adapter.")]
        [JsonProperty("adapter")]
        public string ChannelAdapter { get => channelAdapter; set { channelAdapter = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Device TCP Port"), Description("The TCP port number.  Default port is 502")]
        [JsonProperty("port")]
        public int Port { get => port; set { port = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Device Identitifer"), Description("MODBUS unit identifier (aka slave address)")]
        [JsonProperty("unit-identifier")]
        public int UnitIdentifier { get => unitIdentifier; set { unitIdentifier = value; NotifyPropertyChanged(); } }

        [Category("Protocol"), DisplayName("Maximun Connections"), Description("Maximum number of concurrent TCP connections")]
        [JsonProperty("max-num-connections")]
        public int MaxNumConnections { get => maxNumConnections; set { maxNumConnections = value; NotifyPropertyChanged(); } }
    }
}