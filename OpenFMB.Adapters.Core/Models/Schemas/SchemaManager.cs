// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Schema;
using OpenFMB.Adapters.Core.Json;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenFMB.Adapters.Core.Models.Schemas
{
    public static class SchemaManager
    {        
        private static readonly string SchemaDirectory = "schemas";
        private static string DefaultSchemaDirectory;

        private static readonly ILogger _logger = MasterLogger.Instance;    

        private static readonly Dictionary<string, Schema> _schema = new Dictionary<string, Schema>();

        private static readonly Dictionary<string, string> _resources = new Dictionary<string, string>();

        public static string DefaultEdition { get; set; } = "2.0";
        public static string[] SupportEditions { get; } = new string[] { "2.0", "2.1" };

        public static void Init(string defaultEdition)
        {
            _logger.Log(Level.Info, "Initialize schema manager...");
            var appDataDir = FileHelper.GetAppDataFolder();

            Directory.CreateDirectory(Path.Combine(appDataDir, SchemaDirectory));

            DefaultSchemaDirectory = Path.Combine(appDataDir, SchemaDirectory);

            if (!string.IsNullOrWhiteSpace(defaultEdition))
            {
                DefaultEdition = defaultEdition;
            }            

            var assembly = Assembly.GetAssembly(typeof(SchemaManager));
            var adapterConfig = new AdapterConfiguration();
                        
            Parallel.ForEach(SupportEditions, ver =>
            {
                Directory.CreateDirectory(Path.Combine(DefaultSchemaDirectory, ver));
                var v = Path.Combine(DefaultSchemaDirectory, ver);
                Schema sm = new Schema(v);

                _schema[sm.Edition] = sm;

                foreach (var p in adapterConfig.Plugins.Plugins)
                {
                    // version 2.0 -> v2_0
                    var resourceName = $"{typeof(SchemaManager).Namespace}.v{Path.GetFileName(v).Replace('.', '_')}.{p.Name}.json";

                    var local = Path.Combine(v, $"{p.Name}.json");

                    try
                    {
                        if (!File.Exists(local))
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

                        if (File.Exists(local))
                        {
                            _logger.Log(Level.Info, $"Loading schema for {p.Name} (v{ver})");

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
            });

            ParseOpenFMBDocument("OpenFMB.Models.xml");
        }

        public static JSchema GetSchemaForPlugin(string pluginName, string edition)
        {
            Schema schema;
            if (string.IsNullOrWhiteSpace(edition))
            {
                edition = DefaultEdition;
            }

            if (_schema.TryGetValue(edition, out schema))
            {
                return schema.GetSchemaForPlugin(pluginName);
            }
            return null;
        }

        public static JSchema GetSchemaForProfile(string pluginName, string profileName, string edition)
        {
            Schema schema;
            if (string.IsNullOrWhiteSpace(edition))
            {
                edition = DefaultEdition;
            }

            if (_schema.TryGetValue(edition, out schema))
            {
                return schema.GetSchemaForProfile(pluginName, profileName);
            }
            return null;
        }

        public static Dictionary<string, List<Node>> GetSchemaDictionary(string plugInName, string profileName, string edition)
        {
            Schema schema;
            if (string.IsNullOrWhiteSpace(edition))
            {
                edition = DefaultEdition;
            }

            if (_schema.TryGetValue(edition, out schema))
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
        public string Edition { get; private set; }

        public string Directory { get; private set; }

        private readonly Dictionary<string, JSchema> _schemas = new Dictionary<string, JSchema>();

        private readonly Dictionary<string, Dictionary<string, List<Node>>> _schemaNodesDictionary = new Dictionary<string, Dictionary<string, List<Node>>>();

        public Schema(string directory)
        {
            Directory = directory;
            Edition = Path.GetFileName(directory);
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
