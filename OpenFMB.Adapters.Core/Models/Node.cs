// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenFMB.Adapters.Core.Models
{
    public class Node
    {
        public event EventHandler<EventArgs> OnValueChanged;

        private readonly Dictionary<string, object> _previousValues = new Dictionary<string, object>();

        public Node(string name)
        {
            Name = name;
        }

        [ReadOnly(true)]
        [Description("Name of the node")]
        public string Name { get; set; }

        [Browsable(false)]
        public JSchema Schema { get; set; }

        [Browsable(false)]
        public Node Parent { get; set; }

        [Browsable(false)]
        public List<Node> Nodes { get; } = new List<Node>();

        [Browsable(false)]
        public bool IsRepeatable { get; set; }

        [Browsable(false)]
        public bool HasEnums
        {
            get
            {
                return Schema?.Enum.Count > 0;
            }
        }

        [Browsable(false)]
        public bool HasOptions
        {
            get
            {
                return Schema?.OneOf.Count > 0;
            }
        }

        [Browsable(false)]
        public object Tag { get; set; }

        [Browsable(false)]
        [JsonIgnore]
        public IDataNode DataNode { get; set; }

        public string Path
        {
            get
            {
                var p = Name;
                if (Parent != null)
                {
                    p = $"{Parent.Path}.{p}";
                }
                return p;
            }
        }

        [Browsable(false)]
        public string Value
        {
            get
            {
                if (Tag is JProperty)
                {
                    var prop = Tag as JProperty;
                    if (prop.Value is JValue val)
                    {
                        return val.Value?.ToString();
                    }
                }
                else if (Tag is JValue)
                {
                    var val = Tag as JValue;
                    return val.Value?.ToString();
                }
                return null;
            }
        }

        public string Error { get; set; } = string.Empty;

        public bool IsValid
        {
            get
            {
                return Schema != null && string.IsNullOrEmpty(Error);
            }
        }

        public void Validate()
        {
            try
            {
                if (Schema != null)
                {
                    var result = (Tag as JProperty).Value.IsValid(Schema, out IList<string> messages);
                    if (!result)
                    {
                        Error = string.Join(Environment.NewLine, messages);
                    }
                    else
                    {
                        Error = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        public void RaiseOnValueChangedEvent()
        {
            OnValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
            node.Parent = this;
        }

        public static JSchema GetOptionSchema(string key, string value, JSchema schema)
        {
            if (schema.Type == JSchemaType.Array)
            {
                foreach (var item in schema.Items)
                {
                    var result = GetOptionSchema(key, value, item);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            else
            {
                var result = schema.OneOf.FirstOrDefault(x => x.Properties.Where(p => p.Key == key && p.Value.Const?.ToString() == value).Count() > 0);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    foreach (var op in schema.OneOf)
                    {
                        result = GetOptionSchema(key, value, op);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
            }
            return null;
        }

        public static void GetOptionsForKey(string key, JSchema schema, List<string> options)
        {
            if (schema.Type == JSchemaType.Array)
            {
                foreach (var item in schema.Items)
                {
                    GetOptionsForKey(key, item, options);
                    if (options.Count > 0)
                    {
                        return;
                    }
                }
            }
            else
            {
                var list = schema.OneOf.
                    Select(x => x.Properties.Where(p => p.Key == key && p.Value.Const != null).
                    Select(c => c.Value.Const.ToString()));

                foreach (var l in list)
                {
                    if (l.Count() > 0)
                    {
                        var f = l.First();
                        if (!options.Contains(f))
                        {
                            options.Add(f);
                        }
                    }
                }

                if (options.Count > 0)
                {
                    return;
                }
                else
                {
                    foreach (var op in schema.OneOf)
                    {
                        GetOptionsForKey(key, op, options);
                    }
                }
            }
        }

        private static List<string> GetRequiredProperties(JSchema schema)
        {
            List<string> tempList = new List<string>();
            List<IList<string>> allRequires = null;

            if (schema.Type == JSchemaType.Array)
            {
                if (schema.Items.Count > 0)
                {
                    allRequires = schema.Items[0].OneOf.Select(x => x.Required).ToList();
                }
            }
            else
            {
                allRequires = schema.OneOf.Select(x => x.Required).ToList();
            }

            if (allRequires != null && allRequires.Count > 0)
            {
                tempList = allRequires.First().ToList();
                for (int i = 1; i < allRequires.Count; ++i)
                {
                    tempList = tempList.Intersect(allRequires[i]).ToList();
                }
            }

            return tempList;
        }

        private static List<string> GetRequiredProperties(Node parentNode, JSchema schema)
        {
            // get the minimum requirement
            List<string> tempList = new List<string>();

            if (schema.Type == JSchemaType.Array)
            {
                schema = schema.Items.FirstOrDefault();
            }
            if (schema != null)
            {
                var baseRequired = GetRequiredProperties(schema);
                foreach (var oneOf in schema.OneOf)
                {
                    foreach (var prop in oneOf.Properties)
                    {
                        if (baseRequired.Contains(prop.Key)) // ??? is this neccessary?
                        {
                            Node node = parentNode.Nodes.FirstOrDefault(x => x.Name == prop.Key && x.Value == prop.Value.Const?.ToString());
                            if (node != null)
                            {
                                if (oneOf.Required.Contains(node.Name))
                                {
                                    tempList.Add(node.Name);
                                    tempList = tempList.Union(GetRequiredProperties(parentNode, oneOf)).ToList();
                                }
                            }
                        }
                    }
                }
            }


            return tempList;
        }

        public static List<string> GetRequiredProperties(Node parentNode)
        {
            List<string> tempList = new List<string>();

            var schema = parentNode.Schema;

            if (schema.Type == JSchemaType.Array)
            {
                schema = schema.Items.FirstOrDefault();
            }
            if (schema != null)
            {
                var baseRequired = GetRequiredProperties(schema);

                foreach (var oneOf in schema.OneOf)
                {
                    foreach (var prop in oneOf.Properties)
                    {
                        if (baseRequired.Contains(prop.Key))
                        {
                            Node node = parentNode.Nodes.FirstOrDefault(x => x.Name == prop.Key && x.Value == prop.Value.Const?.ToString());
                            if (node != null)
                            {
                                tempList.AddRange(oneOf.Required);
                                tempList = tempList.Union(GetRequiredProperties(parentNode, oneOf)).ToList();
                            }
                        }
                    }
                }
            }


            return tempList;
        }

        public static bool HasOptionsForKey(string key, JSchema schema)
        {
            if (schema == null)
            {
                return false;
            }

            if (schema.Type == JSchemaType.Array)
            {
                foreach (var item in schema.Items)
                {
                    if (HasOptionsForKey(key, item))
                    {
                        return true;
                    }
                }
            }
            else
            {
                var allRequires = schema.OneOf.Select(x => x.Required).ToList();
                if (allRequires.Count > 0)
                {
                    var tempList = allRequires.First().ToList();
                    for (int i = 1; i < allRequires.Count; ++i)
                    {
                        tempList = tempList.Intersect(allRequires[i]).ToList();
                    }

                    if (tempList.Contains(key))
                    {
                        return true;
                    }
                    else
                    {
                        foreach (var op in schema.OneOf)
                        {
                            if (HasOptionsForKey(key, op))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool HasOptionsForKey(string key)
        {
            return HasOptionsForKey(key, Schema);
        }

        public override string ToString()
        {
            return Path;
        }

        public void ReserveValue(string nodeName, object value)
        {
            _previousValues[nodeName] = value;
        }

        public object GetReservedValue(string nodeName)
        {
            _previousValues.TryGetValue(nodeName, out object val);
            return val;
        }
    }
}