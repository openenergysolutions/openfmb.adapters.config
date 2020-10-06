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

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OpenFMB.Adapters.Core.Json;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenFMB.Adapters.Configuration
{
    public static class MigrationManager
    {
        // newname:oldname
        private static readonly Dictionary<string, string> _nameChanges = new Dictionary<string, string>();        
        private static readonly ILogger _logger = MasterLogger.Instance;

        static MigrationManager()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "OpenFMB.Adapters.Configuration.Migrations.NameChanges.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        var tokens = line.Split(':');
                        if (!line.StartsWith("//") && tokens.Length == 2)
                        {
                            _nameChanges[tokens[0]] = tokens[1];
                        }
                    }                    
                }
            }           
        }

        public static string GetOldName(string newName)
        {
            string oldName;
            if (_nameChanges.TryGetValue(newName, out oldName))
            {
                return oldName;
            }
            return null;
        }

        public static string GetNewName(string oldName)
        {
            foreach(var key in _nameChanges.Keys)
            {
                if (_nameChanges[key] == oldName)
                {
                    return key;
                }
            }
            return null;
        }

        public static JToken SuggestCorrection(Node selectedNode)
        {            
            var selectedParentNode = selectedNode.Parent;
            JToken newToken = null;

            try
            {
                JSchema schema = selectedParentNode.Parent != null ? selectedParentNode.Parent.Schema : selectedParentNode.Schema;

                if (schema != null)
                {
                    if (schema.Type == JSchemaType.Object)
                    {
                        var oldProp = selectedParentNode.Tag as JProperty;

                        // Deep clone before merge
                        oldProp = oldProp.DeepClone() as JProperty;

                        JObject jObject = null;
                        if (oldProp.Value is JObject)
                        {
                            jObject = GenerateToken(schema, oldProp) as JObject;
                        }
                        else
                        {
                            jObject = JsonGenerator.Generate(schema) as JObject;
                        }
                        JToken token;

                        if (jObject.TryGetValue(selectedParentNode.Name, out token))
                        {
                            // !!!! This token.Parent is same level as node.Tag object                                                        
                            var newProp = token.Parent as JProperty;

                            Merge(oldProp, newProp);

                            newToken = newProp;                            
                        }                        

                    }
                    else if (schema.Type == JSchemaType.Array)
                    {
                        var firstElement = schema.Items.FirstOrDefault();

                        var oldObj = selectedParentNode.Tag as JObject;
                        oldObj = oldObj.DeepClone() as JObject;

                        foreach (var oneOf in firstElement.OneOf)
                        {
                            foreach (var key in oneOf.Required)
                            {
                                if (oldObj.ContainsKey(key))
                                {
                                    var val = oldObj[key].ToString();
                                    var s = oneOf.Properties[key].Const?.ToString();

                                    if (val == s)
                                    {
                                        // found                                        
                                        var newObj = GenerateToken(oneOf, oldObj) as JObject;
                                        Merge(oldObj, newObj);

                                        newToken = newObj;                                        

                                        break;
                                    }
                                }
                            }
                        }                       
                    }
                    else
                    {
                        _logger.Log(Level.Debug, "Suggested correction: Schema Type = " + schema.Type);                        
                    }
                }                
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
                throw;
            }
            return newToken;
        }

        public static bool AcceptCorrection(CorrectionType correctionType, JToken newToken, Node selectedParentNode)
        {
            bool result = false;
            try
            {
                if (correctionType == CorrectionType.Replace)
                {
                    if (newToken != null)
                    {
                        if (newToken is JProperty)
                        {
                            var prop = newToken as JProperty;
                            var parent = selectedParentNode.Parent;
                            var tag = parent.Tag as JProperty;
                            if (tag != null)
                            {
                                var properties = tag.Value as JObject;

                                if (properties.ContainsKey(prop.Name))
                                {
                                    properties[prop.Name] = prop.Value;

                                    result = true;
                                }
                            }
                            else
                            {
                                var properties = parent.Tag as JObject;
                                if (properties.ContainsKey(prop.Name))
                                {
                                    properties[prop.Name] = prop.Value;

                                    result = true;
                                }
                            }
                        }
                        else if (newToken is JObject)
                        {
                            var tag = selectedParentNode.Tag as JObject;
                            tag.RemoveAll();

                            tag.Merge(newToken);

                            result = true;
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
                result = false;
                throw;
            }

            return result;
        }

        private static JToken GenerateToken(JSchema schema, JObject oldObj, JObject existing = null)
        {
            if (existing == null)
            {
                existing = JsonGenerator.Generate(schema) as JObject;
            }

            if (schema.OneOf.Count > 0)
            {
                foreach (var oneOf in schema.OneOf)
                {
                    foreach (var key in oneOf.Required)
                    {
                        if (oldObj.ContainsKey(key))
                        {
                            var val = oldObj[key].ToString();
                            var s = oneOf.Properties[key].Const?.ToString();

                            if (val == s)
                            {
                                var newToken = JsonGenerator.Generate(oneOf) as JObject;
                                existing.Merge(newToken, new JsonMergeSettings()
                                {
                                    MergeArrayHandling = MergeArrayHandling.Union
                                });

                                return GenerateToken(oneOf, oldObj, existing);
                            }
                        }
                    }
                }
            }

            return existing;
        }

        private static JToken GenerateToken(JSchema schema, JProperty oldProperty, JObject existing = null)
        {
            if (existing == null)
            {
                existing = JsonGenerator.Generate(schema) as JObject;
            }

            if (schema.Properties.Count > 0)
            {
                foreach (var prop in schema.Properties)
                {
                    if (prop.Key == oldProperty.Name)
                    {
                        return GenerateToken(prop.Value, oldProperty, existing);
                    }
                }
            }

            if (schema.OneOf.Count > 0)
            {
                var myObj = oldProperty.Value as JObject;
                foreach (var oneOf in schema.OneOf)
                {
                    foreach (var key in oneOf.Required)
                    {
                        if (myObj.ContainsKey(key))
                        {
                            var val = myObj[key].ToString();
                            var s = oneOf.Properties[key].Const?.ToString();

                            if (val == s)
                            {
                                var newToken = JsonGenerator.Generate(oneOf) as JObject;
                                var temp = existing[oldProperty.Name] as JObject;
                                temp.Merge(newToken, new JsonMergeSettings()
                                {
                                    MergeArrayHandling = MergeArrayHandling.Union
                                });

                                return GenerateToken(oneOf, oldProperty, existing);
                            }
                        }
                    }
                }
            }
            return existing;
        }

        private static void Merge(JObject oldObj, JObject newObj)
        {
            if (newObj == null || oldObj == null)
            {
                return;
            }

            foreach (var prop in newObj.Properties())
            {
                string oldName;

                if (oldObj.ContainsKey(prop.Name))
                {
                    var value = oldObj[prop.Name];
                    prop.Value = value;

                    oldObj.Remove(prop.Name);
                }
                else if ((oldName = GetOldName(oldObj, prop.Name)) != null)
                {
                    var value = oldObj[oldName];
                    prop.Value = value;

                    oldObj.Remove(oldName);
                }
            }
        }

        private static void Merge(JProperty oldProp, JProperty newProp)
        {
            var newObj = newProp.Value as JObject;
            var oldObj = oldProp.Value as JObject;

            if (newObj == null || oldObj == null)
            {
                return;
            }

            if (oldObj.ContainsKey("i"))
            {
                // Delete "i"
                oldObj.Remove("i");
            }

            if (oldObj.ContainsKey("f"))
            {
                // move up (skip "f") 
                oldObj = oldObj.GetValue("f") as JObject;

                // and skip "value" for "mag", "ang" has "value because it is optional
                if (newProp.Name == "mag")
                {
                    oldObj = oldObj.GetValue("value") as JObject;

                    foreach (var prop in oldObj.Properties())
                    {
                        string newName;
                        if (newObj.ContainsKey(prop.Name))
                        {
                            newObj[prop.Name] = prop.Value;
                        }
                        else if ((newName = GetNewName(newObj, prop.Name)) != null)
                        {
                            var value = oldObj[prop.Name];
                            newObj[newName] = value;
                        }
                        else
                        {
                            newObj.Add(prop.Name, prop.Value);
                        }
                    }
                }
                else if (newProp.Name == "ang")
                {
                    foreach (var prop in newObj.Properties())
                    {
                        if (oldObj.ContainsKey(prop.Name))
                        {
                            var value = oldObj[prop.Name] as JObject;

                            if (value.ContainsKey("float-field-type"))
                            {
                                var v = value.GetValue("float-field-type");

                                value.Add("double-field-type", v);
                                value.Remove("float-field-type");
                            }

                            prop.Value = value;
                            break;
                        }
                    }
                }

            }
            else
            {
                foreach (var prop in newObj.Properties())
                {
                    string oldName;

                    if (oldObj.ContainsKey(prop.Name))
                    {
                        var value = oldObj[prop.Name];
                        prop.Value = value;

                        oldObj.Remove(prop.Name);
                    }
                    else if ((oldName = GetOldName(oldObj, prop.Name)) != null)
                    {
                        var value = oldObj[oldName];
                        prop.Value = value;

                        oldObj.Remove(oldName);
                    }
                    else
                    {
                        Merge(oldProp, prop);
                    }
                }
            }
        }

        private static string GetOldName(JObject obj, string currentName)
        {
            var name = MigrationManager.GetOldName(currentName);
            if (name != null && obj.ContainsKey(name))
            {
                return name;
            }
            return null;
        }

        private static string GetNewName(JObject obj, string oldName)
        {
            var name = MigrationManager.GetNewName(oldName);
            if (name != null && obj.ContainsKey(name))
            {
                return name;
            }
            return null;
        }
    }
}
