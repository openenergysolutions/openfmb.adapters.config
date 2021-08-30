// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using OpenFMB.Adapters.Core.Models.Goose;
using OpenFMB.Adapters.Core.Models.Plugins;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenFMB.Adapters.Core.Models
{
    public class GooseSessionConfiguration : SessionConfiguration
    {
        private ISessionSpecificConfig _sessionSpecific;
        public override ISessionSpecificConfig SessionSpecificConfig
        {
            get
            {
                if (_sessionSpecific == null)
                {
                    if (PluginName == PluginsSection.GoosePub)
                    {
                        _sessionSpecific = new GoosePubSpecificConfig();
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                    else
                    {
                        _sessionSpecific = new GooseSubSpecificConfig();
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                }
                return _sessionSpecific;
            }
        }

        public GooseSessionConfiguration(string pluginName)
        {
            PluginName = pluginName;
        }        

        protected override void LoadSessionConfigurationFromJson(string json)
        {
            if (PluginName == PluginsSection.GoosePub)
            {
                _sessionSpecific = JsonConvert.DeserializeObject<GooseSubSpecificConfig>(json);
            }
            else
            {
                _sessionSpecific = JsonConvert.DeserializeObject<GoosePubSpecificConfig>(json);
            }
        }
    }

    public class GooseSubSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {               
        private string networkAdapter = "wlp2s0";
        private string serverIp = "0.0.0.0";
        private int serverPort = 102;

        public GooseSubSpecificConfig()
        {
            PlugIn = PluginsSection.GooseSub;
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

    public class GoosePubSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string networkAdapter = "wlp2s0";
        private string serverFilePath = "server-model.cid";
        private string serverIp = "127.0.0.1";
        private int serverPort = 102;

        public GoosePubSpecificConfig()
        {
            PlugIn = PluginsSection.GoosePub;
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