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
using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models.Schemas;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace OpenFMB.Adapters.Core.Models
{
    public abstract class SessionConfiguration : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual ISessionSpecificConfig SessionSpecificConfig { get; }

        public string CheckSum { get; set; }

        protected string FilePath { get; private set; }

        public string PluginName { get; protected set; }

        private List<Profile> Profiles { get; } = new List<Profile>();

        protected static ILogger _logger = MasterLogger.Instance;        

        private YamlDocument ToYamlDocument(bool validate)
        {
            var root = new YamlMappingNode();
            var doc = new YamlDocument(root);            

            SessionSpecificConfig.ToYaml(root);

            var seq = new YamlSequenceNode();
            root.Add("profiles", seq);            

            foreach(var p in Profiles)
            {
                p.ErrorMessages.Clear();
                if (validate)
                {
                    var result = p.Validate();
                    
                    if (!result)
                    {
                        _logger.Log(Level.Error, $"Validation failed for profile {p.ProfileName} at {FilePath}.");
                        foreach(var err in p.ErrorMessages)
                        {
                            _logger.Log(Level.Error, $"{err.Message}: {err.NodePath}");
                        }
                    }
                    else
                    {
                        _logger.Log(Level.Info, $"Validation OK for profile {p.ProfileName} at {FilePath}.");
                    }
                }

                var node = p.ToYaml();
                seq.Add(node);
            }

            return doc;
        }

        private void LoadSessionConfiguration(Dictionary<object, object> dictionary)
        {
            Dictionary<object, object> data = new Dictionary<object, object>();
            foreach(var kvp in dictionary)
            {
                if (kvp.Key.ToString() != "profiles")
                {
                    data.Add(kvp.Key, kvp.Value);
                }
            }
            var json = JsonConvert.SerializeObject(data);
            LoadSessionConfigurationFromJson(json);
        }

        protected abstract void LoadSessionConfigurationFromJson(string json);

        protected virtual void InitDefaultProfileSettings(Profile profile) 
        {
            // do nothing
        }

        public bool HasChanged()
        {
            bool flag = true;
            var stream = GetYamlStream(false);

            using (StringWriter writer = new StringWriter())
            {
                stream.Save(writer, assignAnchors: false);
                var s = writer.ToString().Replace("\r\n", "\n");
                var checksum = Utils.ChecksumForString(s);
                flag = checksum != CheckSum;
            }
            return flag;
        }       

        private YamlStream GetYamlStream(bool validate = true)
        {
            var stream = new YamlStream();
            stream.Add(ToYamlDocument(validate));
            return stream;
        }

        public void Save(string filePath)
        {
            var stream = GetYamlStream();

            var dir = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }            

            using (var writer = new StringWriter())
            {
                stream.Save(writer, assignAnchors: false);

                var ab = Path.GetFullPath(filePath);

                // Replace
                var s = writer.ToString().Replace("\r\n", "\n");
                File.WriteAllText(filePath, s);

                CheckSum = Utils.ChecksumForString(s);
            }
        }

        public void Load(string filePath)
        {
            Profiles.Clear();

            if (File.Exists(filePath))
            {                
                using (StreamReader reader = new StreamReader(filePath))
                {                   
                    var builder = new DeserializerBuilder().WithNodeTypeResolver(new ScalarNodeTypeResolver());
                    var des = builder.Build();
                    var dic = des.Deserialize(reader) as Dictionary<object, object>;
                    reader.DiscardBufferedData();
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    CheckSum = Utils.ChecksumForString(reader.ReadToEnd());

                    var profiles = dic["profiles"] as List<object>;

                    foreach(Dictionary<object, object> p in profiles)
                    {
                        try
                        {
                            var jsonStr = JsonConvert.SerializeObject(p, new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
                            var token = JsonConvert.DeserializeObject(jsonStr) as JToken;

                            string profileName = p["name"].ToString();

                            try
                            {
                                Profile profile = new Profile(p["name"].ToString(), PluginName, token);
                                AddProfile(profile);
                            }
                            catch (NoSchemaFoundException)
                            {
                                if (profileName == "SwitchControlProfile") // change to Discrete
                                {
                                    _logger.Log(Level.Debug, "Unable to find schema for SwitchControlProfile, rename to SwitchDiscreteControlProfile and retry.");
                                    profileName = "SwitchDiscreteControlProfile";
                                }
                                else if (profileName == "RecloserControlProfile") // change to Discrete
                                {
                                    _logger.Log(Level.Debug, "Unable to find schema for RecloserControlProfile, rename to RecloserDiscreteControlProfile and retry.");
                                    profileName = "RecloserDiscreteControlProfile";
                                }
                                else if (profileName.StartsWith("Shunt"))
                                {
                                    var temp = profileName.Replace("Shunt", "CapBank");
                                    _logger.Log(Level.Debug, $"Unable to find schema for {profileName}, rename to {temp} and retry.");
                                    profileName = temp;
                                }

                                (token as JObject)["name"] = new JValue(profileName);

                                Profile profile = new Profile(profileName, PluginName, token);
                                AddProfile(profile);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(Level.Error, ex.Message, ex);
                        }
                    }

                    LoadSessionConfiguration(dic);                    
                }

                FilePath = filePath;
            }
            else
            {                
                _logger.Log(Level.Warning, $"The file path '{filePath}' referenced by the adapter does not exist.");                
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        public void AddProfile(Profile profile)
        {
            profile.SessionConfiguration = this;
            InitDefaultProfileSettings(profile);
            Profiles.Add(profile);
        }

        public void DeleteProfile(Profile profile)
        {
            Profiles.Remove(profile);
        }

        public List<Profile> GetProfiles()
        {
            return Profiles;
        }
    }     
}
