﻿/*
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

using Newtonsoft.Json.Schema;
using OpenFMB.Adapters.Core.Json;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace OpenFMB.Adapters.Core.Models.Schemas
{
    public static class SchemaManager
    {        
        private static readonly string DefaultSchemaDirectory = "schemas";

        private static readonly ILogger _logger = MasterLogger.Instance;    

        private static readonly Dictionary<string, Schema> _schema = new Dictionary<string, Schema>();

        private static readonly Dictionary<string, string> _resources = new Dictionary<string, string>();

        public static string DefaultVersion { get; private set; } = "2.0";

        public static void Init(string version = null)
        {
            _logger.Log(Level.Info, "Initialize schema manager...");            

            if (!string.IsNullOrWhiteSpace(version))
            {
                DefaultVersion = version;
            }

            Directory.CreateDirectory(Path.Combine(DefaultSchemaDirectory, DefaultVersion));

            var versions = Directory.GetDirectories(DefaultSchemaDirectory);

            var assembly = Assembly.GetAssembly(typeof(SchemaManager));
            var adapterConfig = new AdapterConfiguration();

            foreach(var v in versions)
            {
                Schema sm = new Schema(v);

                _schema[sm.Version] = sm;

                foreach (var p in adapterConfig.Plugins.Plugins)
                {
                    var resourceName = $"{typeof(SchemaManager).Namespace}.{p.Name}.json";

                    var local = Path.Combine(v, $"{p.Name}.json");

                    try
                    {
                        if (!File.Exists(local))
                        {
                            if (sm.Version.Equals(DefaultVersion, StringComparison.InvariantCultureIgnoreCase))
                            {
                                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                                {
                                    if (stream != null)
                                    {                                        
                                        using (StreamReader reader = new StreamReader(stream))
                                        {
                                            var template = reader.ReadToEnd();
                                            File.WriteAllText(local, template);
                                        }
                                    }
                                }
                            }
                            
                        }

                        if (File.Exists(local))
                        {
                            _logger.Log(Level.Info, $"Loading schema for {p.Name}");

                            using (StreamReader reader = new StreamReader(local))
                            {
                                var template = reader.ReadToEnd();

                                var schema = JSchema.Parse(template);

                                sm.AddSchema(p.Name, schema);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(Level.Error, ex.Message, ex);
                    }
                }
            }

            ParseOpenFMBDocument("OpenFMB.Models.xml");
        }

        public static JSchema GetSchemaForPlugin(string pluginName, string version = null)
        {
            Schema schema;
            if (string.IsNullOrWhiteSpace(version))
            {
                version = DefaultVersion;
            }

            if (_schema.TryGetValue(version, out schema))
            {
                return schema.GetSchemaForPlugin(pluginName);
            }
            return null;
        }

        public static JSchema GetSchemaForProfile(string pluginName, string profileName, string version = null)
        {
            Schema schema;
            if (string.IsNullOrWhiteSpace(version))
            {
                version = DefaultVersion;
            }

            if (_schema.TryGetValue(version, out schema))
            {
                return schema.GetSchemaForProfile(pluginName, profileName);
            }
            return null;
        }

        public static Dictionary<string, List<Node>> GetSchemaDictionary(string plugInName, string profileName, string version = null)
        {
            Schema schema;
            if (string.IsNullOrWhiteSpace(version))
            {
                version = DefaultVersion;
            }

            if (_schema.TryGetValue(version, out schema))
            {
                return schema.GetSchemaDictionary(plugInName, profileName);
            }

            return new Dictionary<string, List<Node>>();
        }

        public static List<string> GetSchemaVersions()
        {
            var list = _schema.Keys.ToList();
            list.Sort();
            return list;
        }

        private static void ParseOpenFMBDocument(string filePath)
        {
            _resources.Clear();

            try
            {
                XDocument document = XDocument.Load(filePath);

                var membersElement = document.Root.Element("members");

                var memberList = membersElement.Elements("member");

                foreach (var m in memberList)
                {
                    var name = m.Attribute("name").Value;
                    var tokens = name.Split('.');
                    var tag = tokens[tokens.Length - 1];
                    var summary = m.Element("summary").Value.Trim();

                    if (!summary.StartsWith("MISSING DOCUMENTATION")) // ignore missing documentation
                    {
                        _resources[tag.ToLower()] = summary;
                    }                    
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
            }
        }

        public static string GetDescription(string tag)
        {
            string desc;
            if (_resources.TryGetValue(tag.ToLower(), out desc)) {
                return desc;
            }
            return "No description";
        }
    }

    public class Schema
    {
        public string Version { get; private set; }

        public string Directory { get; private set; }

        private readonly Dictionary<string, JSchema> _schemas = new Dictionary<string, JSchema>();

        private readonly Dictionary<string, Dictionary<string, List<Node>>> _schemaNodesDictionary = new Dictionary<string, Dictionary<string, List<Node>>>();

        public Schema(string directory)
        {
            Directory = directory;
            Version = Path.GetFileName(directory);
        }

        internal void AddSchema(string pluginName, JSchema schema)
        {
            _schemas[pluginName] = schema;
        }

        public JSchema GetSchemaForPlugin(string pluginName)
        {
            JSchema schema;
            if (_schemas.TryGetValue(pluginName, out schema))
            {
                return schema;
            }
            return null;
        }

        public JSchema GetSchemaForProfile(string pluginName, string profileName)
        {
            var schema = GetSchemaForPlugin(pluginName);

            var profiles = schema.Properties.FirstOrDefault(x => x.Key == "profiles").Value;

            var options = profiles.Items.FirstOrDefault();

            foreach (var option in options.OneOf)
            {
                foreach (var prop in option.Properties)
                {
                    if (prop.Key == "name" && prop.Value.Const?.ToString() == profileName)
                    {
                        return option;
                    }
                }
            }
            return null;
        }

        public Dictionary<string, List<Node>> GetSchemaDictionary(string plugInName, string profileName)
        {
            Dictionary<string, List<Node>> dict;
            string key = $"{plugInName}:{profileName}";
            if (!_schemaNodesDictionary.TryGetValue(key, out dict))
            {
                dict = new Dictionary<string, List<Node>>();
                _schemaNodesDictionary[key] = dict;

                var node = new Node(profileName);
                node.Schema = GetSchemaForProfile(plugInName, profileName);
                JsonGenerator.LoadSchema(node.Schema, node);

                var allNodes = node.Traverse();
                foreach (var n in allNodes)
                {
                    //dict[n.Path] = n;
                    if (dict.ContainsKey(n.Path))
                    {
                        var existingList = dict[n.Path];
                        existingList.Add(n);
                    }
                    else
                    {
                        dict[n.Path] = new List<Node>() { n };
                    }
                }
            }
            return dict;
        }
    }
}
