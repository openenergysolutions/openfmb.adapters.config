// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.ComponentModel;

namespace OpenFMB.Adapters.Core.Models
{
    public class IccpSessionConfiguration : SessionConfiguration
    {
        private ISessionSpecificConfig _sessionSpecific;

        public override ISessionSpecificConfig SessionSpecificConfig
        {
            get
            {
                if (_sessionSpecific == null)
                {
                    if (PluginName == PluginsSection.IccpClient)
                    {
                        _sessionSpecific = new IccpClientSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                    else
                    {
                        _sessionSpecific = new IccpServerSpecificConfig(Edition);
                        _sessionSpecific.PropertyChanged += OnPropertyChanged;
                    }
                }
                return _sessionSpecific;
            }
        }

        public IccpSessionConfiguration(string pluginName, string edition)
        {
            PluginName = pluginName;
            Edition = edition;
        }

        protected override void LoadSessionConfigurationFromJson(string json)
        {
            try
            {
                if (PluginName == PluginsSection.IccpClient)
                {
                    _sessionSpecific = JsonConvert.DeserializeObject<IccpClientSpecificConfig>(json);
                }
                else
                {
                    _sessionSpecific = JsonConvert.DeserializeObject<IccpServerSpecificConfig>(json);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
            }
        }
    }

    public class IcppAddressing
    {
        [JsonProperty("ae-invoke-id")]
        public int AEInvokeId { get; set; } = 10;
        [JsonProperty("ae-qualifier")]
        public int AEQualifier { get; set; } = 12;
        [JsonProperty("ap-invoke-id")]
        public int APInvokeId { get; set; } = 10;
        [JsonProperty("ap-title")]
        public string APTitle { get; set; } = "1.1.999.1";
        [JsonProperty("presentation-selector")]
        public string PSEL { get; set; } = "00 00 00 01";
        [JsonProperty("session-selector")]
        public string SSEL { get; set; } = "00 01";
        [JsonProperty("transport-selector")]
        public string TSEL { get; set; } = "00 01";
    }

    public class MMSSettings
    {
        [JsonProperty("max-nesting-level")]
        public int MaxNestingLevel { get; set; } = 5;
        [JsonProperty("max-pdu-size")]
        public int MaxPduSize { get; set; } = 65435;
        [JsonProperty("max-services-called")]
        public int MaxServicesCalled { get; set; } = 10;
        [JsonProperty("max-services-calling")]
        public int MaxServicesCalling { get; set; } = 10;
        [JsonProperty("request-timeout")]
        public int RequestTimeoutMx { get; set; } = 10000;
    }

    public class AuthenticateMechnism
    {
        [JsonProperty("authentication-type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.none;

        [JsonProperty("password")]
        [PasswordPropertyText(true)]
        public string Password { get; set; } = "";

        [JsonProperty("ca-trusted-cert-file")]
        public string CertFile { get; set; } = "cert.pem";

        [JsonProperty("client-private-key-file")]
        public string ClientKey { get; set; } = "client_key.pem";

        [JsonProperty("client-cert-chain-file")]
        public string ClientCert { get; set; } = "client_cert.pem";

    }

    public class IccpClientSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string name = "client1";
        private string serverIp = "127.0.0.1";
        private int serverPort = 102;
        private bool autoReconnect = true;
        private int autoReconnectTries = 0;
        private int autoReconnectWaitTimeMs = 1000;

        public IccpClientSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.IccpClient;
        }

        [Category("General"), DisplayName("Service Name"), Description("Name of the service for logging purpose.")]
        [JsonProperty("name")]
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }

        [Category("Logging"), DisplayName("Log Level")]
        [JsonProperty("log-level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevel LogLevel { get; set; } = LogLevel.Warn;

        [Category("Basic"), DisplayName("Server IP Address")]
        [JsonProperty("server-ip")]
        public string ServerIp { get => serverIp; set { serverIp = value; NotifyPropertyChanged(); } }

        [Category("Basic"), DisplayName("Server TCP Port"), Description("The TCP port number.  Default port is 102")]
        [JsonProperty("server-port")]
        public int ServerPort { get => serverPort; set { serverPort = value; NotifyPropertyChanged(); } }

        [Category("Basic"), DisplayName("Auto Reconnect"), Description("This will try to reconnect when connection is lost")]
        [JsonProperty("auto-reconnect")]
        public bool AutoReconnect { get => autoReconnect; set { autoReconnect = value; NotifyPropertyChanged(); } }

        [Category("Basic"), DisplayName("Auto Reconnect Tries"), Description("Number of times the client will try to reconnect (0 means forever)")]
        [JsonProperty("auto-reconnect-tries")]
        public int AutoReconnectTries { get => autoReconnectTries; set { autoReconnectTries = value; NotifyPropertyChanged(); } }

        [Category("Basic"), DisplayName("Auto Reconnect Wait Time"), Description("Number of milliseconds to wait to try to reconnect")]
        [JsonProperty("auto-reconnect-wait-time-ms")]
        public int AutoReconnectWaitTimeMs { get => autoReconnectWaitTimeMs; set { autoReconnectWaitTimeMs = value; NotifyPropertyChanged(); } }

        // client specific

        [Browsable(false)]
        [JsonProperty("client-specific")]
        public IcppAddressing ClientAddress { get; set; } = new IcppAddressing();                

        [Category("Client Specific"), DisplayName("AE Invoke ID"), Description("ACSE AE Invoke ID")]
        [JsonIgnore]
        public int ClientAEInvokeId
        {
            get { return ClientAddress.AEInvokeId; }
            set { ClientAddress.AEInvokeId = value; NotifyPropertyChanged(); }
        }

        [Category("Client Specific"), DisplayName("AE Qualifier"), Description("ACSE AE Qualifier")]
        [JsonIgnore]
        public int ClientAEQualifier
        {
            get { return ClientAddress.AEQualifier; }
            set { ClientAddress.AEQualifier = value; NotifyPropertyChanged(); }
        }

        [Category("Client Specific"), DisplayName("AP Invoke ID"), Description("ACSE AP Invoke ID")]
        [JsonIgnore]
        public int ClientAPInvokeId
        {
            get { return ClientAddress.APInvokeId; }
            set { ClientAddress.APInvokeId = value; NotifyPropertyChanged(); }
        }

        [Category("Client Specific"), DisplayName("AP Title"), Description("ACSE AP Title")]
        [JsonIgnore]
        public string ClientAPTitle
        {
            get { return ClientAddress.APTitle; }
            set { ClientAddress.APTitle = value; NotifyPropertyChanged(); }
        }

        [Category("Client Specific"), DisplayName("Presentation Selector"), Description("Presentation address of client")]
        [JsonIgnore]
        public string ClientPSEL
        {
            get { return ClientAddress.PSEL; }
            set { ClientAddress.PSEL = value; NotifyPropertyChanged(); }
        }

        [Category("Client Specific"), DisplayName("Session Selector"), Description("Session address of client")]
        [JsonIgnore]
        public string ClientSSEL
        {
            get { return ClientAddress.SSEL; }
            set { ClientAddress.SSEL = value; NotifyPropertyChanged(); }
        }

        [Category("Client Specific"), DisplayName("Transport Selector"), Description("Transport address of client")]
        [JsonIgnore]
        public string ClientTSEL
        {
            get { return ClientAddress.TSEL; }
            set { ClientAddress.TSEL = value; NotifyPropertyChanged(); }
        }

        // server specific

        [Browsable(false)]
        [JsonProperty("server-specific")]
        public IcppAddressing ServerAddress { get; set; } = new IcppAddressing();

        [Category("Server Specific"), DisplayName("AE Invoke ID"), Description("ACSE AE Invoke ID")]
        [JsonIgnore]
        public int ServerAEInvokeId
        {
            get { return ServerAddress.AEInvokeId; }
            set { ServerAddress.AEInvokeId = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("AE Qualifier"), Description("ACSE AE Qualifier")]
        [JsonIgnore]
        public int ServerAEQualifier
        {
            get { return ServerAddress.AEQualifier; }
            set { ServerAddress.AEQualifier = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("AP Invoke ID"), Description("ACSE AP Invoke ID")]
        [JsonIgnore]
        public int ServerAPInvokeId
        {
            get { return ServerAddress.APInvokeId; }
            set { ServerAddress.APInvokeId = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("AP Title"), Description("ACSE AP Title")]
        [JsonIgnore]
        public string ServerAPTitle
        {
            get { return ServerAddress.APTitle; }
            set { ServerAddress.APTitle = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("Presentation Selector"), Description("Presentation address of server")]
        [JsonIgnore]
        public string ServerPSEL
        {
            get { return ServerAddress.PSEL; }
            set { ServerAddress.PSEL = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("Session Selector"), Description("Session address of server")]
        [JsonIgnore]
        public string ServerSSEL
        {
            get { return ServerAddress.SSEL; }
            set { ServerAddress.SSEL = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("Transport Selector"), Description("Transport address of server")]
        [JsonIgnore]
        public string ServerTSEL
        {
            get { return ServerAddress.TSEL; }
            set { ServerAddress.TSEL = value; NotifyPropertyChanged(); }
        }

        // mms settings
        [Browsable(false)]
        [JsonProperty("mms-settings")]
        public MMSSettings MMSSettings { get; set; } = new MMSSettings();

        [Category("MMS Settings"), DisplayName("Max Nesting Level"), Description("Maximum nesting level")]
        [JsonIgnore]
        public int MaxNestingLevel
        {
            get { return MMSSettings.MaxNestingLevel; }
            set { MMSSettings.MaxNestingLevel = value; NotifyPropertyChanged(); }
        }

        [Category("MMS Settings"), DisplayName("Max PDU Size"), Description("Maximum PDU size")]
        [JsonIgnore]
        public int MaxPduSize
        {
            get { return MMSSettings.MaxPduSize; }
            set { MMSSettings.MaxPduSize = value; NotifyPropertyChanged(); }
        }

        [Category("MMS Settings"), DisplayName("Max Services Called"), Description("Maximum outstanding Server to Client transactions")]
        [JsonIgnore]
        public int MaxServicesCalled
        {
            get { return MMSSettings.MaxServicesCalled; }
            set { MMSSettings.MaxServicesCalled = value; NotifyPropertyChanged(); }
        }

        [Category("MMS Settings"), DisplayName("Max Services Calling"), Description("Maximum outstanding Client to Server transactions")]
        [JsonIgnore]
        public int MaxServicesCalling
        {
            get { return MMSSettings.MaxServicesCalling; }
            set { MMSSettings.MaxServicesCalling = value; NotifyPropertyChanged(); }
        }

        // authentication    
        [Browsable(false)]
        [JsonProperty("authentication-mechanism")]
        public AuthenticateMechnism AuthenticateMechnism { get; set; } = new AuthenticateMechnism();

        [Category("Authentication Mechanism"), DisplayName("Authentication Type"), Description("Type of authentication")]
        [JsonIgnore]
        public AuthenticationType AuthenticationType
        {
            get { return AuthenticateMechnism.AuthenticationType; }
            set { AuthenticateMechnism.AuthenticationType = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Password"), Description("Specify password if Password Authentication Type is being selected")]
        [JsonIgnore]
        public string Password
        {
            get { return AuthenticateMechnism.Password; }
            set { AuthenticateMechnism.Password = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Cert File"), Description("Specify path to cert file if Certificate Authentication Type is being selected")]
        [JsonIgnore]
        public string CertFile
        {
            get { return AuthenticateMechnism.CertFile; }
            set { AuthenticateMechnism.CertFile = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Client Cert File"), Description("Specify path to client cert file if Certificate Authentication Type is being selected")]
        [JsonIgnore]
        public string ClientCert
        {
            get { return AuthenticateMechnism.ClientCert; }
            set { AuthenticateMechnism.ClientCert = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Client Key File"), Description("Specify path to client key file if Certificate Authentication Type is being selected")]
        [JsonIgnore]
        public string ClientKey
        {
            get { return AuthenticateMechnism.ClientKey; }
            set { AuthenticateMechnism.ClientKey = value; NotifyPropertyChanged(); }
        }
    }

    public class IccpServerSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string name = "service1";
        private string serverIp = "127.0.0.1";
        private int serverPort = 102;
        private string serverFilePath = "server-model.csv";

        public IccpServerSpecificConfig(string edition) : base(edition)
        {
            PlugIn = PluginsSection.IccpServer;
        }

        [Category("General"), DisplayName("Service Name"), Description("Name of the service for logging purpose.")]
        [JsonProperty("name")]
        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }

        [Category("Logging"), DisplayName("Log Level")]
        [JsonProperty("log-level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevel LogLevel { get; set; } = LogLevel.Warn;

        [Category("Basic"), DisplayName("Server IP Address")]
        [JsonProperty("server-ip")]
        public string ServerIp { get => serverIp; set { serverIp = value; NotifyPropertyChanged(); } }

        [Category("Basic"), DisplayName("Server TCP Port"), Description("The TCP port number.  Default port is 102")]
        [JsonProperty("server-port")]
        public int ServerPort { get => serverPort; set { serverPort = value; NotifyPropertyChanged(); } }

        [Category("Basic"), DisplayName("Server Model File"), Description("Path to file described server's model")]
        [JsonProperty("server-file-path")]
        public string ServerFilePath { get => serverFilePath; set { serverFilePath = value; NotifyPropertyChanged(); } }

        // server specific

        [Browsable(false)]
        [JsonProperty("server-specific")]
        public IcppAddressing ServerAddress { get; set; } = new IcppAddressing();

        [Category("Server Specific"), DisplayName("AE Invoke ID"), Description("ACSE AE Invoke ID")]
        [JsonIgnore]
        public int ServerAEInvokeId
        {
            get { return ServerAddress.AEInvokeId; }
            set { ServerAddress.AEInvokeId = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("AE Qualifier"), Description("ACSE AE Qualifier")]
        [JsonIgnore]
        public int ServerAEQualifier
        {
            get { return ServerAddress.AEQualifier; }
            set { ServerAddress.AEQualifier = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("AP Invoke ID"), Description("ACSE AP Invoke ID")]
        [JsonIgnore]
        public int ServerAPInvokeId
        {
            get { return ServerAddress.APInvokeId; }
            set { ServerAddress.APInvokeId = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("AP Title"), Description("ACSE AP Title")]
        [JsonIgnore]
        public string ServerAPTitle
        {
            get { return ServerAddress.APTitle; }
            set { ServerAddress.APTitle = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("Presentation Selector"), Description("Presentation address of server")]
        [JsonIgnore]
        public string ServerPSEL
        {
            get { return ServerAddress.PSEL; }
            set { ServerAddress.PSEL = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("Session Selector"), Description("Session address of server")]
        [JsonIgnore]
        public string ServerSSEL
        {
            get { return ServerAddress.SSEL; }
            set { ServerAddress.SSEL = value; NotifyPropertyChanged(); }
        }

        [Category("Server Specific"), DisplayName("Transport Selector"), Description("Transport address of server")]
        [JsonIgnore]
        public string ServerTSEL
        {
            get { return ServerAddress.TSEL; }
            set { ServerAddress.TSEL = value; NotifyPropertyChanged(); }
        }

        // authentication    
        [Browsable(false)]
        [JsonProperty("authentication-mechanism")]
        public AuthenticateMechnism AuthenticateMechnism { get; set; } = new AuthenticateMechnism();

        [Category("Authentication Mechanism"), DisplayName("Authentication Type"), Description("Type of authentication")]
        [JsonIgnore]
        public AuthenticationType AuthenticationType
        {
            get { return AuthenticateMechnism.AuthenticationType; }
            set { AuthenticateMechnism.AuthenticationType = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Password"), Description("Specify password if Password Authentication Type is being selected")]
        [JsonIgnore]
        public string Password
        {
            get { return AuthenticateMechnism.Password; }
            set { AuthenticateMechnism.Password = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Cert File"), Description("Specify path to cert file if Certificate Authentication Type is being selected")]
        [JsonIgnore]
        public string CertFile
        {
            get { return AuthenticateMechnism.CertFile; }
            set { AuthenticateMechnism.CertFile = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Client Cert File"), Description("Specify path to client cert file if Certificate Authentication Type is being selected")]
        [JsonIgnore]
        public string ClientCert
        {
            get { return AuthenticateMechnism.ClientCert; }
            set { AuthenticateMechnism.ClientCert = value; NotifyPropertyChanged(); }
        }

        [Category("Authentication Mechanism"), DisplayName("Client Key File"), Description("Specify path to client key file if Certificate Authentication Type is being selected")]
        [JsonIgnore]
        public string ClientKey
        {
            get { return AuthenticateMechnism.ClientKey; }
            set { AuthenticateMechnism.ClientKey = value; NotifyPropertyChanged(); }
        }
    }
}
