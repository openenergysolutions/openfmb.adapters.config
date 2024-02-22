// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using OpenFMB.Adapters.Core.Models.Goose;
using OpenFMB.Adapters.Core.Models.Plugins;
using System.ComponentModel;

namespace OpenFMB.Adapters.Core.Models
{
    public class IEC61850SessionConfiguration : SessionConfiguration
    {
        private ISessionSpecificConfig _sessionSpecific;
        public override ISessionSpecificConfig SessionSpecificConfig
        {
            get
            {
                if (_sessionSpecific == null)
                {
                    if (PluginName == PluginsSection.IEC61850Server)
                    {
                        _sessionSpecific = new IEC61850ServerSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                    else
                    {
                        _sessionSpecific = new IEC61850ClientSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                }
                return _sessionSpecific;
            }
        }

        public IEC61850SessionConfiguration(string pluginName, string edition)
        {
            PluginName = pluginName;
            Edition = edition;
        }

        protected override void LoadSessionConfigurationFromJson(string json)
        {
            if (PluginName == PluginsSection.IEC61850Client)
            {
                _sessionSpecific = JsonConvert.DeserializeObject<IEC61850ClientSpecificConfig>(json);
            }
            else
            {
                _sessionSpecific = JsonConvert.DeserializeObject<IEC61850ServerSpecificConfig>(json);
            }
        }
    }

    public enum IEC61850ClientType
    {
        GOOSE,
        MMS
    }

    public class IEC61850ClientSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string networkAdapter = "wlp2s0";
        private IEC61850ClientType clientType = IEC61850ClientType.GOOSE;
        private string serverIp = "0.0.0.0";
        private int serverPort = 102;

        public IEC61850ClientSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.IEC61850Client;
        }

        [Category("General"), Description("Name of network adapter"), DisplayName("Network Adapter")]
        [JsonProperty("networkAdapter")]
        public string NetworkAdapter { get => networkAdapter; set { networkAdapter = value; NotifyPropertyChanged(); } }

        [Category("General"), Description("Type of client, either GOOSE or MMS"), DisplayName("Client Type")]
        [JsonProperty("client-type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public IEC61850ClientType ClientType { get => clientType; set { clientType = value; NotifyPropertyChanged(); } }

        [Category("General"), DisplayName("Server IP Address")]
        [JsonProperty("server-ip")]
        public string ServerIp { get => serverIp; set { serverIp = value; NotifyPropertyChanged(); } }

        [Category("General"), DisplayName("Server TCP Port"), Description("The TCP port number.  Default port is 102")]
        [JsonProperty("server-port")]
        public int ServerPort { get => serverPort; set { serverPort = value; NotifyPropertyChanged(); } }

        [Browsable(false)]
        [JsonProperty("control-settings")]
        public ControlSettings ControlSettings { get; set; } = new ControlSettings();

        [JsonIgnore]
        [Category("Control Settings"), Description("Origin Category for control"), DisplayName("Origin Category")]
        public OriginCategory ControlSettingsOrCat
        {
            get { return ControlSettings.OrCat; }
            set { ControlSettings.OrCat = value; }
        }

        [JsonIgnore]
        [Category("Control Settings"), Description("Bit String of the control Origin Identification"), DisplayName("Origin Identification")]
        public string ControlSettingsOrIdent
        {
            get { return ControlSettings.OrIdent; }
            set { ControlSettings.OrIdent = value; }
        }
    }

    public class IEC61850ServerSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string networkAdapter = "wlp2s0";
        private string serverFilePath = "server-model.cid";
        private string serverIp = "127.0.0.1";
        private int serverPort = 102;

        public IEC61850ServerSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.IEC61850Server;
        }

        [Category("General"), Description("Name of network adapter"), DisplayName("Network Adapter")]
        [JsonProperty("networkAdapter")]
        public string NetworkAdapter { get => networkAdapter; set { networkAdapter = value; NotifyPropertyChanged(); } }

        [Category("General"), DisplayName("Server IP Address")]
        [JsonProperty("server-ip")]
        public string ServerIp { get => serverIp; set { serverIp = value; NotifyPropertyChanged(); } }

        [Category("General"), DisplayName("Server TCP Port"), Description("The TCP port number.  Default port is 102")]
        [JsonProperty("server-port")]
        public int ServerPort { get => serverPort; set { serverPort = value; NotifyPropertyChanged(); } }

        [JsonProperty("server-file-path")]
        [Category("General"), Description("Path to file described server's model"), DisplayName("Server Model File")]
        public string ServerFilePath { get => serverFilePath; set { serverFilePath = value; NotifyPropertyChanged(); } }
    }
}