// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace OpenFMB.Adapters.Core.Models
{
    public class Dnp3SessionConfiguration : SessionConfiguration
    {
        private ISessionSpecificConfig _sessionSpecific;
        
        public override ISessionSpecificConfig SessionSpecificConfig
        {
            get
            {
                if (_sessionSpecific == null)
                {
                    if (PluginName == PluginsSection.Dnp3Master)
                    {
                        _sessionSpecific = new Dnp3MasterSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                    else
                    {
                        _sessionSpecific = new Dnp3OutstationSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                }
                return _sessionSpecific;
            }
        }        

        public Dnp3SessionConfiguration(string pluginName, string edition)
        {
            PluginName = pluginName;
            Edition = edition;
            PropertyChanged += (sender, e) =>
            {
                NotifyPropertyChanged(e.PropertyName);
            };            
        }        

        protected override void LoadSessionConfigurationFromJson(string json)
        {
            if (PluginName == PluginsSection.Dnp3Master)
            {
                try
                {
                    _sessionSpecific = JsonConvert.DeserializeObject<Dnp3MasterSpecificConfig>(json);
                }
                catch (Exception ex)
                {
                    // older version
                    _logger.Log(Level.Error, ex.Message, ex);
                    _logger.Log(Level.Info, "Unable to parse session configuration.  Will try to parse manually.");

                    var jsonObject = JsonConvert.DeserializeObject(json) as JObject;

                    if (jsonObject.ContainsKey("channel"))
                    {
                        var channel = jsonObject["channel"] as JObject;
                        if (channel != null)
                        {
                            if (channel.ContainsKey("port"))
                            {
                                var port = channel["port"].ToString();
                                int temp;
                                if (!int.TryParse(port, out temp))
                                {
                                    channel["port"] = new JValue(20000);
                                }
                            }
                        }
                    }

                    json = JsonConvert.SerializeObject(jsonObject);
                    try
                    {
                        _sessionSpecific = JsonConvert.DeserializeObject<Dnp3MasterSpecificConfig>(json);
                    }
                    catch { }
                }
            }
            else
            {
                try
                {
                    _sessionSpecific = JsonConvert.DeserializeObject<Dnp3OutstationSpecificConfig>(json);
                }
                catch (Exception ex)
                {
                    _logger.Log(Level.Error, ex.Message, ex);
                }
            }
           
        }

        protected override void InitDefaultProfileSettings(Profile profile)
        {
            try
            {
                var type = ProfileRegistry.GetProfileType(profile.ProfileName);

                var poll = GetPollByType(type);
                if (poll != null)
                {
                    var obj = profile.Token as JObject;
                    if (obj != null)
                    {
                        if (obj.ContainsKey("poll-name"))
                        {
                            var val = obj["poll-name"] as JValue;
                            if (val != null && val.ToString() == "")
                            {
                                obj["poll-name"] = new JValue(poll.Name);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
            }
        }

        private Dnp3Poll GetPollByType(ProfileType type)
        {
            if (PluginName == PluginsSection.Dnp3Master)
            {
                if (type == ProfileType.Reading || type == ProfileType.Status)
                {
                    Dnp3MasterSpecificConfig specific = this.SessionSpecificConfig as Dnp3MasterSpecificConfig;

                    var poll = specific.Polls.FirstOrDefault(x => x.IsStaticScan());
                    if (poll == null)
                    {
                        poll = Dnp3Poll.CreateStaticScan();
                        specific.Polls.Add(poll);
                    }
                    return poll;
                }
                else if (type == ProfileType.Event)
                {
                    Dnp3MasterSpecificConfig specific = this.SessionSpecificConfig as Dnp3MasterSpecificConfig;

                    var poll = specific.Polls.FirstOrDefault(x => x.IsEventScan());
                    if (poll == null)
                    {
                        poll = Dnp3Poll.CreateEventScan();
                        specific.Polls.Add(poll);
                    }
                    return poll;
                }
            }
            return null;
        }
    }

    public class StartupIntegrity
    {
        [JsonProperty("class0")]
        public bool Class0 { get; set; }
        [JsonProperty("class1")]
        public bool Class1 { get; set; }
        [JsonProperty("class2")]
        public bool Class2 { get; set; }
        [JsonProperty("class3")]
        public bool Class3 { get; set; }
    }

    public class Unsolicited
    {
        [JsonProperty("class1")]
        public bool Class1 { get; set; }
        [JsonProperty("class2")]
        public bool Class2 { get; set; }
        [JsonProperty("class3")]
        public bool Class3 { get; set; }
    }

    public class Dnp3MasterChannel
    {
        [Category("Channel"), DisplayName("Network Adapter")]
        [JsonProperty("adapter")]
        public string Adapter { get; set; } = "0.0.0.0";

        [Category("Channel"), DisplayName("Outstation IP Address")]
        [JsonProperty("outstation-ip")]
        public string OutstationIp { get; set; } = "127.0.0.1";

        [Category("Channel"), DisplayName("TCP Port")]
        [JsonProperty("port")]
        public int Port { get; set; } = 20000;
    }

    public class Dnp3OutstationChannel
    {
        [Category("Channel"), DisplayName("Network Adapter")]
        [JsonProperty("listen-adapter")]
        public string Adapter { get; set; } = "127.0.0.1";        

        [Category("Channel"), DisplayName("TCP Port")]
        [JsonProperty("port")]
        public int Port { get; set; } = 20000;
    }

    public class Dnp3MasterProtocol
    {
        [Category("Protocol"), DisplayName("Master Address")]
        [JsonProperty("master-address")]
        public int MasterAddress { get; set; } = 1;

        [Category("Protocol"), DisplayName("Outstation Address")]
        [JsonProperty("outstation-address")]
        public int OutstationAddress { get; set; } = 10;

        [Browsable(false)]
        [Category("Protocol"), DisplayName("Startup Integrity")]
        [JsonProperty("startup-integrity")]
        public StartupIntegrity StartupIntegrity { get; set; } = new StartupIntegrity();

        [Browsable(false)]
        [Category("Protocol"), DisplayName("Unsolicited")]
        [JsonProperty("unsolicited")]
        public Unsolicited Unsolicited { get; set; } = new Unsolicited();
    }

    public enum BIVariationsStatic
    {
        Group1Var1,
        Group1Var2
    }

    public enum BIVariationsEvent
    {
        Group2Var1, 
        Group2Var2, 
        Group2Var3
    }

    public enum AIVariationsStatic
    {
        Group30Var1,
        Group30Var2,
        Group30Var3,
        Group30Var4,
        Group30Var5,
        Group30Var6
    }

    public enum AIVariationsEvent
    {
        Group32Var1,
        Group32Var2,
        Group32Var3,
        Group32Var4,
        Group32Var5,
        Group32Var6,
        Group32Var7,
        Group32Var8
    }

    public enum CounterVariationsStatic
    {
        Group20Var1,
        Group20Var2,
        Group20Var5,
        Group20Var6
    }

    public enum CounterVariationsEvent
    {
        Group22Var1,
        Group22Var2,
        Group22Var5,
        Group22Var6
    }

    public class StaticVariations
    {
        [Description("Binary input default static variation")]
        [JsonProperty("binary-input")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BIVariationsStatic BinaryInput { get; set; }

        [Description("Analog input default static variation")]
        [JsonProperty("analog-input")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AIVariationsStatic AnalogInput { get; set; }

        [Description("Counter default static variation")]        
        [JsonProperty("counter")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CounterVariationsStatic Counter { get; set; }
    }

    public class EventVariations
    {
        [Description("Binary input default static variation")]
        [JsonProperty("binary-input")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BIVariationsEvent BinaryInput { get; set; }

        [Description("Analog input default static variation")]
        [JsonProperty("analog-input")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AIVariationsEvent AnalogInput { get; set; }

        [Description("Counter default static variation")]
        [JsonProperty("counter")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CounterVariationsEvent Counter { get; set; }
    }

    public class Dnp3OutstationProtocol
    {        
        [Category("Protocol"), DisplayName("Remote Address")]
        [JsonProperty("master-address")]
        public int MasterAddress { get; set; } = 1;

        [Category("Protocol"), DisplayName("Local Address")]
        [JsonProperty("outstation-address")]
        public int OutstationAddress { get; set; } = 10;

        [Category("Protocol"), DisplayName("Enable Solicited")]
        [JsonProperty("enable-unsolicited")]
        public bool EnableSolicited { get; set; }

        [Category("Protocol"), DisplayName("Confirm Timeout (ms)")]
        [JsonProperty("confirm-timeout-ms")]
        public int ConfirmTimeOutMs { get; set; } = 1000;

        [Category("Protocol"), DisplayName("Default Static Variations"), Description("DNP3 default static variations")]
        [JsonProperty("default-static-variations")]
        public StaticVariations StaticVariations { get; set; } = new StaticVariations();

        [Category("Protocol"), DisplayName("Default Event Variations"), Description("DNP3 default event variations")]
        [JsonProperty("default-event-variations")]
        public EventVariations EventVariations { get; set; } = new EventVariations();
    }

    public class DnpClasses
    {
        [JsonProperty("class0")]
        public bool Class0 { get; set; } = true;
        [JsonProperty("class1")]
        public bool Class1 { get; set; }
        [JsonProperty("class2")]
        public bool Class2 { get; set; }
        [JsonProperty("class3")]
        public bool Class3 { get; set; }
    }

    public class Dnp3Poll
    {
        private string name = "static_data_scan";
        private int intervalMs = 1000;

        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                if (value != "")
                {
                    name = value;                    
                }
            }
        }
        [JsonProperty("interval-ms")]
        public int IntervalMs
        {
            get { return intervalMs; }
            set
            {
                intervalMs = value;                
            }
        }

        [Browsable(false)]
        [JsonProperty("classes")]
        public DnpClasses DnpClasses { get; set; } = new DnpClasses();

        [JsonIgnore]
        public bool Class0
        {
            get { return DnpClasses.Class0; }
            set { DnpClasses.Class0 = value; }
        }
        [JsonIgnore]
        public bool Class1
        {
            get { return DnpClasses.Class1; }
            set { DnpClasses.Class1 = value; }
        }
        [JsonIgnore]
        public bool Class2
        {
            get { return DnpClasses.Class2; }
            set { DnpClasses.Class2 = value; }
        }
        [JsonIgnore]
        public bool Class3
        {
            get { return DnpClasses.Class3; }
            set { DnpClasses.Class3 = value;  }
        }        

        public bool IsStaticScan()
        {
            return Class0;
        }

        public bool IsEventScan()
        {
            return Class0 == false && (Class1 || Class2 || Class3);
        }

        public static Dnp3Poll CreateStaticScan()
        {
            return new Dnp3Poll();            
        }

        public static Dnp3Poll CreateEventScan()
        {
            return new Dnp3Poll()
            {
                Name = "event_data_scan",
                Class0 = false,
                Class1 = true,
                Class2 = false,
                Class3 = false
            };
        }
    }

    public class Dnp3MasterSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        public Dnp3MasterSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.Dnp3Master;                        
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged();
        }

        private string name = "device1";

        [Category("General"), DisplayName("Device Name"), Description("Name of the device for logging purpose.")]
        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
        [Category("Channel"), DisplayName("Network Adapter"), Description("Default is 0.0.0.0.  Override to use specific network adapter.")]
        [JsonIgnore]
        public string ChannelAdapter
        {
            get { return Channel.Adapter; }
            set 
            { 
                Channel.Adapter = value;
                NotifyPropertyChanged();
            }
        }

        [Category("Channel"), DisplayName("Outstation IP Address"), Description("IP address of the DNP3 outstation.")]
        [JsonIgnore]
        public string ChannelOutstationIp
        {
            get { return Channel.OutstationIp; }
            set { Channel.OutstationIp = value; NotifyPropertyChanged(); }
        }

        [Category("Channel"), DisplayName("TCP Port"), Description("The TCP port number.  Default port is 20000")]
        [JsonIgnore]
        public int ChannelPort
        {
            get { return Channel.Port; }
            set { Channel.Port = value; NotifyPropertyChanged(); }
        }

        [Category("Protocol"), DisplayName("Master Address"), Description("DNP3 master link-layer address")]
        [JsonIgnore]
        public int ProtocolMasterAddress
        {
            get { return Protocol.MasterAddress; }
            set { Protocol.MasterAddress = value; NotifyPropertyChanged(); }
        }

        [Category("Protocol"), DisplayName("Outstation Address"), Description("DNP3 outstation link-layer address")]
        [JsonIgnore]
        public int ProtocolOutstationAddress
        {
            get { return Protocol.OutstationAddress; }
            set { Protocol.OutstationAddress = value; NotifyPropertyChanged(); }
        }

        [Category("Startup Integrity"), DisplayName("Class 0 (Starup)"), Description("Configures whether the class is read during the startup integrity scan")]
        [JsonIgnore]
        public bool StartupIntegrityClass0
        {
            get { return Protocol.StartupIntegrity.Class0; }
            set { Protocol.StartupIntegrity.Class0 = value; NotifyPropertyChanged(); }
        }

        [Category("Startup Integrity"), DisplayName("Class 1 (Starup)"), Description("Configures whether the class is read during the startup integrity scan")]
        [JsonIgnore]
        public bool StartupIntegrityClass1
        {
            get { return Protocol.StartupIntegrity.Class1; }
            set { Protocol.StartupIntegrity.Class1 = value; NotifyPropertyChanged(); }
        }


        [Category("Startup Integrity"), DisplayName("Class 2 (Starup)"), Description("Configures whether the class is read during the startup integrity scan")]
        [JsonIgnore]
        public bool StartupIntegrityClass2
        {
            get { return Protocol.StartupIntegrity.Class2; }
            set { Protocol.StartupIntegrity.Class2 = value; NotifyPropertyChanged(); }
        }


        [Category("Startup Integrity"), DisplayName("Class 3 (Starup)"), Description("Configures whether the class is read during the startup integrity scan")]
        [JsonIgnore]
        public bool StartupIntegrityClass3
        {
            get { return Protocol.StartupIntegrity.Class3; }
            set { Protocol.StartupIntegrity.Class3 = value; NotifyPropertyChanged(); }
        }

        [Category("Unsolicited"), DisplayName("Class 1 (unsolicited)"), Description("Configures whether the event class will be reported via unsolicited")]
        [JsonIgnore]
        public bool UnsolicitedClass1
        {
            get { return Protocol.Unsolicited.Class1; }
            set { Protocol.Unsolicited.Class1 = value; NotifyPropertyChanged(); }
        }

        [Category("Unsolicited"), DisplayName("Class 2 (unsolicited)"), Description("Configures whether the event class will be reported via unsolicited")]
        [JsonIgnore]
        public bool UnsolicitedClass2
        {
            get { return Protocol.Unsolicited.Class2; }
            set { Protocol.Unsolicited.Class2 = value; NotifyPropertyChanged(); }
        }

        [Category("Unsolicited"), DisplayName("Class 3 (unsolicited)"), Description("Configures whether the event class will be reported via unsolicited")]
        [JsonIgnore]
        public bool UnsolicitedClass3
        {
            get { return Protocol.Unsolicited.Class3; }
            set { Protocol.Unsolicited.Class3 = value; NotifyPropertyChanged(); }
        }

        [Category("Polls"), DisplayName("Poll Policy"), Description("Specify DNP3 periodic poll policy that will be used by specific profile mapping.")]
        [JsonProperty("polls")]
        public ObservableCollection<Dnp3Poll> Polls { get; set; } = new ObservableCollection<Dnp3Poll>();

        [Browsable(false)]
        [JsonProperty("channel")]
        public Dnp3MasterChannel Channel { get; set; } = new Dnp3MasterChannel();

        [Browsable(false)]
        [JsonProperty("protocol")]
        public Dnp3MasterProtocol Protocol { get; set; } = new Dnp3MasterProtocol();        
    }

    public class Dnp3OutstationSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        public Dnp3OutstationSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.Dnp3Outstation;
        }

        private string name = "device1";

        [Category("General"), DisplayName("Device Name"), Description("Name of the device for logging purpose.")]
        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
        [Category("Channel"), DisplayName("Network Adapter"), Description("Default is 0.0.0.0.  Override to use specific network adapter.")]
        [JsonIgnore]
        public string ChannelAdapter
        {
            get { return Channel.Adapter; }
            set { Channel.Adapter = value; NotifyPropertyChanged(); }
        }

        [Category("Channel"), DisplayName("TCP Port"), Description("The TCP port number.  Default port is 20000")]
        [JsonIgnore]
        public int ChannelPort
        {
            get { return Channel.Port; }
            set { Channel.Port = value; NotifyPropertyChanged(); }
        }

        [Category("Protocol"), DisplayName("Master Address"), Description("DNP3 master link-layer address")]
        [JsonIgnore]
        public int ProtocolMasterAddress
        {
            get { return Protocol.MasterAddress; }
            set { Protocol.MasterAddress = value; NotifyPropertyChanged(); }
        }

        [Category("Protocol"), DisplayName("Outstation Address"), Description("DNP3 outstation link-layer address")]
        [JsonIgnore]
        public int ProtocolOutstationAddress
        {
            get { return Protocol.OutstationAddress; }
            set { Protocol.OutstationAddress = value; NotifyPropertyChanged(); }
        }

        [Category("Protocol"), DisplayName("Enable Solicited"), Description("Enable unsolicited responses")]
        [JsonIgnore]
        public bool EnableSolicited
        {
            get { return Protocol.EnableSolicited; }
            set { Protocol.EnableSolicited = value; NotifyPropertyChanged(); }
        }

        [Category("Protocol"), DisplayName("Confirm Timeout (ms)"), Description("DNP3 confirmation timeout (in milliseconds)")]
        [JsonIgnore]
        public int ConfirmTimeOutMs
        {
            get { return Protocol.ConfirmTimeOutMs; }
            set { Protocol.ConfirmTimeOutMs = value; NotifyPropertyChanged(); }
        }

        [Category("Static Variations"), DisplayName("Binary Input (static)"), Description("Binary input default static variation")]
        [JsonIgnore]
        public BIVariationsStatic BIStatic
        {
            get { return Protocol.StaticVariations.BinaryInput; }
            set { Protocol.StaticVariations.BinaryInput = value; NotifyPropertyChanged(); }
        }

        [Category("Static Variations"), DisplayName("Analog Input (static)"), Description("Analog input default static variation")]
        [JsonIgnore]
        public AIVariationsStatic AIStatic
        {
            get { return Protocol.StaticVariations.AnalogInput; }
            set { Protocol.StaticVariations.AnalogInput = value; NotifyPropertyChanged(); }
        }

        [Category("Static Variations"), DisplayName("Counter (static)"), Description("Counter default static variation")]
        [JsonIgnore]
        public CounterVariationsStatic CounterStatic
        {
            get { return Protocol.StaticVariations.Counter; }
            set { Protocol.StaticVariations.Counter = value; NotifyPropertyChanged(); }
        }

        [Category("Event Variations"), DisplayName("Binary Input (event)"), Description("Binary input default event variation")]
        [JsonIgnore]
        public BIVariationsEvent BIEvent
        {
            get { return Protocol.EventVariations.BinaryInput; }
            set { Protocol.EventVariations.BinaryInput = value; NotifyPropertyChanged(); }
        }

        [Category("Event Variations"), DisplayName("Analog Input (event)"), Description("Analog input default event variation")]
        [JsonIgnore]
        public AIVariationsEvent AIEvent
        {
            get { return Protocol.EventVariations.AnalogInput; }
            set { Protocol.EventVariations.AnalogInput = value; NotifyPropertyChanged(); }
        }


        [Category("Event Variations"), DisplayName("Counter (event)"), Description("Counter default event variation")]
        [JsonIgnore]
        public CounterVariationsEvent CounterEvent
        {
            get { return Protocol.EventVariations.Counter; }
            set { Protocol.EventVariations.Counter = value; NotifyPropertyChanged(); }
        }

        [Browsable(false)]
        [JsonProperty("channel")]
        public Dnp3OutstationChannel Channel { get; set; } = new Dnp3OutstationChannel();

        [Browsable(false)]
        [JsonProperty("protocol")]
        public Dnp3OutstationProtocol Protocol { get; set; } = new Dnp3OutstationProtocol();
    }
}
