// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace OpenFMB.Adapters.Core
{
    public class Model
    {
        [JsonProperty("breakermodule")]
        public List<ProfileModel> BreakerModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("capbankmodule")]
        public List<ProfileModel> CapbankModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("circuitsegmentservicemodule")]
        public List<ProfileModel> CircuitSegmentServiceModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("essmodule")]
        public List<ProfileModel> EssModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("generationmodule")]
        public List<ProfileModel> GenerationModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("interconnectionmodule")]
        public List<ProfileModel> InterconnectionModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("loadmodule")]
        public List<ProfileModel> LoadModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("metermodule")]
        public List<ProfileModel> MeterModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("reclosermodule")]
        public List<ProfileModel> RecloserModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("regulatormodule")]
        public List<ProfileModel> RegulatorModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("reservemodule")]
        public List<ProfileModel> ReserveModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("resourcemodule")]
        public List<ProfileModel> ResourceModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("solarmodule")]
        public List<ProfileModel> SolarModule { get; set; } = new List<ProfileModel>();

        [JsonProperty("switchmodule")]
        public List<ProfileModel> SwitchModule { get; set; } = new List<ProfileModel>();

        private readonly List<ProfileModel> _allProfileModels = new List<ProfileModel>();

        [JsonIgnore]
        public List<ProfileModel> ProfileModels
        {
            get
            {
                if (_allProfileModels.Count == 0)
                {
                    var properties = GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        if (property.GetValue(this) is List<ProfileModel> list)
                        {
                            _allProfileModels.AddRange(list);
                        }
                    }
                }
                return _allProfileModels;
            }
        }

        public List<ProfileModel> GetProfilesListByModule(string name)
        {
            var property = GetType().GetProperty(name);
            if (property != null)
            {
                return property.GetValue(this) as List<ProfileModel>;
            }
            return null;
        }
    }

    public class ProfileModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topics")]
        public List<Topic> Topics { get; set; } = new List<Topic>();
    }

    public class Attributes
    {
        private string[] _keywords;

        [JsonProperty("label")]
        public string Label { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = "Not defined";

        [JsonIgnore]
        public string[] Keywords
        {
            get
            {
                try
                {
                    if (_keywords == null)
                    {
                        _keywords = Name.Split('|').Select(x => x.Trim().ToLower()).ToArray();
                    }
                    return _keywords;
                }
                catch { }
                return new string[0];
            }
        }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("measurement")]
        public string UOM { get; set; } = string.Empty;
    }

    public class Topic
    {
        [JsonProperty("type")]
        public string Type { get; } = "element";

        [JsonProperty("name")]
        public string Name { get; } = "Object";

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; } = new Attributes();

        [JsonIgnore]
        public string Label
        {
            get { return Attributes.Label; }
        }

        [JsonIgnore]
        public string Tags
        {
            get { return Attributes.Name; }
        }

        [JsonIgnore]
        public string Path
        {
            get { return Attributes.Path; }
        }

        [JsonIgnore]
        public int Matches { get; set; }

        public Topic Copy()
        {
            return this.MemberwiseClone() as Topic;
        }
    }
}
