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

using Newtonsoft.Json;
using OpenFMB.Adapters.Core.Models.Goose;
using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using YamlDotNet.RepresentationModel;

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
            _sessionSpecific = JsonConvert.DeserializeObject<GooseSubSpecificConfig>(json);
        }
    }

    public class GooseSubSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string networkAdapter = "wlp2s0";
        private int appId = 12290;
        private string goCbRef = "AA1D/LLN0$GO$gcb_XCBRMeas";

        public GooseSubSpecificConfig()
        {
            PlugIn = PluginsSection.GooseSub;
        }

        [Description("Name of network adapter"), DisplayName("Network Adapter")]
        [JsonProperty("networkAdapter")]
        public string NetworkAdapter { get => networkAdapter; set { networkAdapter = value; NotifyPropertyChanged(); } }
        [Description("Application ID"), DisplayName("App ID")]

        [JsonProperty("appId")]
        public int AppId { get => appId; set { appId = value; NotifyPropertyChanged(); } }
        [Description("GOOSE control block reference"), DisplayName("GOOSE Cb Ref")]

        [JsonProperty("goCbRef")]
        public string GoCbRef { get => goCbRef; set { goCbRef = value; NotifyPropertyChanged(); } }
        [JsonProperty("gooseStructure")]
        [Description("GOOSE structures used to map to OpenFMB profile"), DisplayName("GOOSE Structures")]
        public List<GooseStructure> GooseStructures { get; set; } = new List<GooseStructure>();
    }

    public class GoosePubSpecificConfig : BaseSessionSpecifiConfig, ISessionSpecificConfig
    {
        private string networkAdapter = "wlp2s0";
        private string sourceMacAddress = "a8:6b:00:2a:09:a3";
        private string destinationMacAddress = "01:0C:CD:01:00:00";
        private int appId = 1;
        private string goCbRef = "AA1D/LLN0$GO$gcb_XCBRMeas";
        private string dataSet = "AA1D/LLN0$dsXCBRMeas";
        private string goId = "AA1D/LLN0.gcb_XCBRMeas";
        private string confRev = "100";
        private int tTL = 1000;

        public GoosePubSpecificConfig()
        {
            PlugIn = PluginsSection.GoosePub;
        }

        [Description("Name of network adapter"), DisplayName("Network Adapter")]
        [JsonProperty("networkAdapter")]
        public string NetworkAdapter { get => networkAdapter; set { networkAdapter = value; NotifyPropertyChanged(); } }
        [JsonProperty("src-mac")]
        [Description("Source MAC address"), DisplayName("Source MAC Address")]
        public string SourceMacAddress { get => sourceMacAddress; set { sourceMacAddress = value; NotifyPropertyChanged(); } }
        [JsonProperty("dest-mac")]
        [Description("Destination MAC address"), DisplayName("Destination MAC Address")]
        public string DestinationMacAddress { get => destinationMacAddress; set { destinationMacAddress = value; NotifyPropertyChanged(); } }
        [JsonProperty("appId")]
        [Description("Application ID"), DisplayName("App ID")]
        public int AppId { get => appId; set { appId = value; NotifyPropertyChanged(); } }
        [Description("GOOSE control block reference"), DisplayName("GOOSE Cb Ref")]

        [JsonProperty("goCbRef")]
        public string GoCbRef { get => goCbRef; set { goCbRef = value; NotifyPropertyChanged(); } }
        [JsonProperty("datSet")]
        [Description("Name of the data set"), DisplayName("Data Set")]
        public string DataSet { get => dataSet; set { dataSet = value; NotifyPropertyChanged(); } }
        [JsonProperty("goID")]
        [Description("GOOSE ID"), DisplayName("GOOSE ID")]
        public string GoId { get => goId; set { goId = value; NotifyPropertyChanged(); } }
        [JsonProperty("confRev")]
        [Description("Configuration revision number"), DisplayName("Configuration Rev.")]
        public string ConfRev { get => confRev; set { confRev = value; NotifyPropertyChanged(); } }
        [JsonProperty("ttl")]
        [Description("Retransmission interval in milliseconds"), DisplayName("TTL")]
        public int TTL { get => tTL; set { tTL = value; NotifyPropertyChanged(); } }

        [Description("GOOSE structures used to map to OpenFMB profile"), DisplayName("GOOSE Structures")]
        public List<GooseStructure> GooseStructures { get; set; } = new List<GooseStructure>();
    }
}