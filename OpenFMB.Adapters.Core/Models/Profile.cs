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
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OpenFMB.Adapters.Core.Json;
using OpenFMB.Adapters.Core.Models.Schemas;
using OpenFMB.Adapters.Core.Parsers;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models
{
    public class Profile
    {
        public event EventHandler<EventArgs> OnValidated;

        private static readonly ILogger _logger = MasterLogger.Instance;

        public string ProfileName { get; set; }
        public string PluginName { get; set; }

        [Browsable(false)]
        public Node NavigatorRoot { get; set; }

        [Browsable(false)]
        public List<ICsvRow> CsvData { get; } = new List<ICsvRow>();

        [Browsable(false)]
        public List<ValidationErrorMessage> ErrorMessages { get; set; } = new List<ValidationErrorMessage>();

        [Browsable(false)]
        public SessionConfiguration SessionConfiguration { get; set; }

        public ProfileType ProfileType
        {
            get
            {
                return ProfileRegistry.GetProfileType(ProfileName);
            }
        }

        public bool IsValid
        {
            get
            {
                return ErrorMessages.Count == 0;// && GetAllNavigatorNodes().FirstOrDefault(x => x.IsValid == false) == null;
            }
        }

        private readonly Dictionary<string, List<Node>> _schemaLookup;
       
        private List<Node> _allNavigatorNodes;

        public JToken Token { get; private set; }

        public JSchema Schema
        {            
            get; private set;
        }

        public Profile(string profileName, string plugInName)
        {
            ProfileName = profileName;
            PluginName = plugInName;

            Schema = SchemaManager.GetSchemaForProfile(plugInName, profileName);

            if (Schema == null) 
            {
                _logger.Log(Level.Error, $"Unable to find schema for {profileName} [{plugInName}]");
                throw new NoSchemaFoundException($"Unable to find schema for {profileName} [{plugInName}]", profileName, plugInName);
            }
           
            Token = JsonGenerator.Generate(Schema);
           
            _schemaLookup = SchemaManager.GetSchemaDictionary(plugInName, profileName);

            Validate();
        }

        public Profile(string profileName, string plugInName, JToken token)
        {
            ProfileName = profileName;
            PluginName = plugInName;

            Schema = SchemaManager.GetSchemaForProfile(plugInName, profileName);

            if (Schema == null)
            {
                _logger.Log(Level.Error, $"Unable to find schema for {profileName} [{plugInName}]");
                throw new NoSchemaFoundException($"Unable to find schema for {profileName} [{plugInName}]", profileName, plugInName);
            }

            if (token == null)
            {               
                throw new ArgumentNullException("token", $"Unable to create profile {profileName} [{plugInName}] with null token.");
            }

            Token = token;            

            _schemaLookup = SchemaManager.GetSchemaDictionary(plugInName, profileName);

            Validate();

            MitigateErrors();
        }

        public string GetDeviceMRID()
        {
            string mrid = "???";

            var mridNode = GetDeviceMRIDNode();

            if (mridNode != null)
            {
                mrid = mridNode.Value;
            }

            return mrid;
        }

        public Node GetDeviceMRIDNode()
        {
            return GetAllNavigatorNodes().FirstOrDefault(x => x.Path.EndsWith(".conductingEquipment.mRID.value"));
        }

        public bool Validate()
        {
            bool valid = true;
            ErrorMessages.Clear();

            IList<string> messages;
            var obj = Token as JObject;
            if (!obj.IsValid(Schema, out messages))
            {                
                ErrorMessages.AddRange(messages.Select(x => ValidationErrorMessage.Parse(x)));
                valid = false;
            };

            OnValidated?.Invoke(this, EventArgs.Empty);

            return valid;
        }

        private void MitigateErrors()
        {
            foreach(var error in ErrorMessages)
            {
                if (error.Message.StartsWith("Required properties are missing from object"))
                {
                    var tokens = error.Message.Trim().TrimEnd('.').Split(':');
                    if (tokens.Length > 1)
                    {
                        var tag = tokens[1].Trim();
                        if (Schema.Properties.ContainsKey(tag))
                        {
                            var schema = Schema.Properties[tag];
                            var token = JsonGenerator.Generate(schema);
                            (Token as JObject).Add(new JProperty(tag, token));
                        }
                    }
                }
            }
        }

        public Node GetSchemaByPath(string path, JSchemaType? schemaType)
        {
            List<Node> nodes;

            path = Utils.ReplaceWithIndexZeroArray(path);

            if (_schemaLookup.TryGetValue(path, out nodes))
            {
                var node = nodes.LastOrDefault(x => x.Schema.Type == schemaType || !x.Schema.Type.HasValue);
                if (node == null)
                {
                    return nodes.FirstOrDefault();
                }
                else
                {
                    return node;
                }
            }
            return null;
        }

        public Node GetSchemaByPath(Node targetNode, JSchemaType? schemaType)
        {
            List<Node> nodes;

            var path = Utils.ReplaceWithIndexZeroArray(targetNode.Path);

            if (schemaType == JSchemaType.Array)
            {
                if (!path.EndsWith(".[0]"))
                {
                    path = path + ".[0]";
                }
            }                        

            if (_schemaLookup.TryGetValue(path, out nodes))
            {
                if (nodes.Count > 1)
                {
                    if (targetNode.Parent != null)
                    {
                        List<Node> parentNodes;
                        var parentPath = Utils.ReplaceWithIndexZeroArray(targetNode.Parent.Path);
                        if (_schemaLookup.TryGetValue(parentPath, out parentNodes))
                        {
                            var parentNode = parentNodes.FirstOrDefault();
                            if (parentNode != null)
                            {
                                var child = parentNode.Nodes.FirstOrDefault(x => x.Schema.Const?.ToString() == targetNode.Value);
                                if (child != null)
                                {
                                    return child;
                                }
                                else
                                {                                    
                                    foreach(var sibling in targetNode.Parent.Nodes)
                                    {                                        
                                        if (sibling != targetNode)
                                        {
                                            var temp = parentNode.Nodes.FirstOrDefault(x => x.Schema.Const?.ToString() == sibling.Value);
                                            if (temp != null)
                                            {
                                                var index = parentNode.Nodes.IndexOf(temp);
                                                var subList = parentNode.Nodes.GetRange(index, parentNode.Nodes.Count - index);
                                                return subList.FirstOrDefault(x => x.Name == targetNode.Name);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }                    
                }
                else
                {
                    return nodes.FirstOrDefault();
                }                
            }
            return null;
        }

        public List<Node> GetAllNavigatorNodes(bool refresh = false)
        {
            if (refresh || _allNavigatorNodes == null)
            {
                _allNavigatorNodes = NavigatorRoot.Traverse().ToList();
            }
            return _allNavigatorNodes;
        }
        
        public YamlNode ToYaml()
        {            
            var serializer = new YamlDotNet.Serialization.Serializer();
            
            string json = JsonConvert.SerializeObject(Token);
            var dict = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

            var yaml = serializer.Serialize(dict);
            var deserializer = new YamlDotNet.Serialization.Deserializer();            

            using (TextReader reader = new StringReader(yaml))
            {
                return deserializer.Deserialize(reader, typeof(YamlMappingNode)) as YamlMappingNode;                                                
            }
        }

        public static Profile Create(string profileName, string pluginName)
        {            
            return Activator.CreateInstance(typeof(Profile), new object[] { profileName, pluginName }) as Profile;
        }

        public static Profile Create(string profileName, string pluginName, List<ICsvRow> mappedData)
        {
            var profile = Activator.CreateInstance(typeof(Profile), new object[] { profileName, pluginName }) as Profile;
            profile.AddNode();

            foreach(var row in mappedData)
            {
                var node = profile.GetAllNavigatorNodes().FirstOrDefault(x => x.Path.ToLower() == row.FormattedPath.ToLower());
                if (node != null)
                {
                    var schema = node.Schema;
                    var required = Node.GetRequiredProperties(node);

                    foreach(var name in required)
                    {                        
                        foreach(var oneOf in schema.OneOf)
                        {
                            var prop = oneOf.Properties.FirstOrDefault(x => x.Key == name && x.Value.Const?.ToString() == "mapped");
                            if (prop.Key == name)
                            {
                                // Found
                                var token = JsonGenerator.Generate(oneOf, row) as JObject;
                                (node.Tag as JProperty).Value = token;

                                node.Nodes.Clear();
                                profile.AddNode(token, node);                                

                                break;
                            }
                        }
                    }
                }
            }

            return profile;
        }
                
        private void AddNode()
        {
            Node root = new Node(ProfileName);
            root.Tag = Token;
            root.Schema = Schema;

            NavigatorRoot = root;

            AddNode(Token, root);
        }

        private void AddNode(JToken token, Node nodeParent)
        {
            if (token == null)
            {
                return;
            }

            if (token is JValue)
            {
                if (nodeParent.Schema == null)
                {
                    nodeParent.Schema = GetSchemaByPath(nodeParent, token.SchemaType())?.Schema;
                }
            }
            else if (token is JObject)
            {
                var obj = token as JObject;
                foreach (var property in obj.Properties())
                {
                    var childNode = new Node(property.Name);
                    childNode.Tag = property;
                    childNode.Parent = nodeParent;
                    nodeParent.Nodes.Add(childNode);
                    childNode.Schema = GetSchemaByPath(childNode, property.Value.SchemaType())?.Schema;
                    //if (childNode.Schema == null)
                    //{
                    //    childNode.Schema = GetSchemaByPath(childNode.Path + ".[0]", property.Value.SchemaType())?.Schema;
                    //}

                    AddNode(property.Value, childNode);
                }
            }
            else if (token is JArray)
            {
                var array = token as JArray;
                for (int i = 0; i < array.Count; i++)
                {
                    var childNode = new Node($"[{i}]");
                    childNode.IsRepeatable = true;
                    childNode.Tag = array[i];
                    childNode.Parent = nodeParent;
                    nodeParent.Nodes.Add(childNode);
                    childNode.Schema = GetSchemaByPath(childNode, token.SchemaType())?.Schema;                   

                    AddNode(array[i], childNode);
                }
            }
            else if (token is JProperty)
            {
                var property = token as JProperty;
                foreach (JArray array in property)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        var childNode = new Node($"[{i}]");
                        childNode.IsRepeatable = true;
                        childNode.Tag = array[i];
                        childNode.Parent = nodeParent;
                        nodeParent.Nodes.Add(childNode);
                        childNode.Schema = GetSchemaByPath(childNode, token.SchemaType())?.Schema;
                    }
                }
            }
            else
            {
                Debug.WriteLine(string.Format("{0} not implemented", token.Type));
            }
        }

        public JArray UpdateCommandIds(List<string> allCommandId)
        {
            try
            {  
                var obj = Token as JObject;
                if (obj.ContainsKey("command-order"))
                {
                    var array = obj["command-order"] as JArray;                    
                    array.Clear();

                    for (int i = 0; i < allCommandId.Count; ++i)
                    {
                        var value = new JValue(allCommandId[i]);
                        array.Add(value);                        
                    }

                    return array;
                }                
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
            }
            return null;
        }
    }    
}