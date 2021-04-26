// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Parsers;
using OpenFMB.Adapters.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenFMB.Adapters.Core.Json
{
    public static class JsonGenerator
    {
        private const string DefaultCommandId = "some-command-id";
        private const int DefaultToleranceMs = 5000;
        private const int DefaultPollPeriodMs = 1000;

        public static JToken Generate(JSchema schema)
        {
            if (schema.Type == null)
            {
                if (schema.Const == null)
                {
                    schema.Type = JSchemaType.Object;
                }
            }
            JToken output = null;
            switch (schema.Type)
            {
                case JSchemaType.Object:
                    JObject jObject = null;
                    if (schema.Properties != null)
                    {
                        jObject = new JObject();

                        foreach (var prop in schema.Properties)
                        {
                            //jObject.Add(prop.Key, Generate(prop.Value));
                            jObject.Add(prop.Key, Generate(prop));
                        }
                        output = jObject;
                    }
                    if (schema.OneOf.Count > 0)
                    {
                        if (jObject == null)
                        {
                            jObject = new JObject();
                        }

                        var item = schema.OneOf.FirstOrDefault();
                        if (item != null)
                        {
                            // Add first option
                            var first = Generate(item);

                            if (first is JObject)
                            {                                
                                var firstObj = first as JObject;
                                foreach(var prop in firstObj)
                                {
                                    jObject.Add(prop.Key, prop.Value);
                                }
                            }
                        }

                        output = jObject;
                    }
                    break;
                case JSchemaType.Array:
                    var jArray = new JArray();

                    foreach (var item in schema.Items)
                    {
                        jArray.Add(Generate(item));
                    }
                    output = jArray;
                    break;

                case JSchemaType.String:
                    {
                        string val = "ignored";
                        if (schema.Pattern == "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}")
                        {
                            val = Guid.Empty.ToString().ToLower();
                        }
                        else if (schema.Format == "ipv4")
                        {
                            val = "127.0.0.1";
                        }
                        else if (schema.Enum.Count > 0)
                        {
                            val = schema.Enum.First().ToString();
                        }
                        output = new JValue(val);
                    }
                    break;
                case JSchemaType.Number:
                    {
                        double num = 0.0;                        
                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Integer:
                    {
                        int num = 0;                                                
                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Boolean:
                    output = new JValue(false);
                    break;
                case JSchemaType.Null:
                    output = JValue.CreateNull();
                    break;
                default:
                    if (schema.Const != null)
                    {
                        try
                        {
                            if (schema.Const is JValue)
                            {
                                output = new JValue(schema.Const.ToString());
                            }
                            else
                            {
                                output = null;
                            }
                        }
                        catch
                        {
                            output = null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("SchemaType is null!!!!!");
                        output = null;
                    }
                    break;

            }

            return output;
        }

        public static JToken Generate(JSchema schema, ICsvRow row)
        {
            if (schema.Type == null)
            {
                if (schema.Const == null)
                {
                    schema.Type = JSchemaType.Object;
                }
            }
            JToken output = null;
            switch (schema.Type)
            {
                case JSchemaType.Object:
                    JObject jObject = null;
                    if (schema.Properties != null)
                    {
                        jObject = new JObject();

                        foreach (var prop in schema.Properties)
                        {                           
                            jObject.Add(prop.Key, Generate(prop, row));
                        }
                        output = jObject;
                    }
                    if (schema.OneOf.Count > 0)
                    {
                        if (jObject == null)
                        {
                            jObject = new JObject();
                        }

                        bool found = false;

                        foreach(var oneOf in schema.OneOf)
                        {
                            foreach(var prop in oneOf.Properties)
                            {
                                if (prop.Value.Const?.ToString() == row.DataType)
                                {
                                    found = true;
                                    var first = Generate(oneOf, row);

                                    if (first is JObject)
                                    {
                                        var firstObj = first as JObject;
                                        foreach (var property in firstObj)
                                        {
                                            jObject.Add(property.Key, property.Value);
                                        }
                                    }
                                }
                            }
                        } 
                        
                        if (!found)
                        {
                            var item = schema.OneOf.FirstOrDefault();
                            if (item != null)
                            {
                                // Add first option
                                var first = Generate(item, row);

                                if (first is JObject)
                                {
                                    var firstObj = first as JObject;
                                    foreach (var prop in firstObj)
                                    {
                                        jObject.Add(prop.Key, prop.Value);
                                    }
                                }
                            }
                        }

                        output = jObject;
                    }
                    break;
                case JSchemaType.Array:
                    var jArray = new JArray();

                    foreach (var item in schema.Items)
                    {
                        jArray.Add(Generate(item, row));
                    }
                    output = jArray;
                    break;

                case JSchemaType.String:
                    {
                        string val = "ignored";
                        if (schema.Pattern == "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}")
                        {
                            val = Guid.Empty.ToString().ToLower();
                        }
                        else if (schema.Format == "ipv4")
                        {
                            val = "127.0.0.1";
                        }
                        else if (schema.Enum.Count > 0)
                        {
                            val = schema.Enum.First().ToString();
                        }
                        output = new JValue(val);
                    }
                    break;
                case JSchemaType.Number:
                    {
                        double num = 0.0;                       
                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Integer:
                    {
                        int num = 0;                        
                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Boolean:
                    output = new JValue(false);
                    break;
                case JSchemaType.Null:
                    output = JValue.CreateNull();
                    break;
                default:
                    if (schema.Const != null)
                    {
                        try
                        {
                            if (schema.Const is JValue)
                            {
                                output = new JValue(schema.Const.ToString());
                            }
                            else
                            {
                                output = null;
                            }
                        }
                        catch
                        {
                            output = null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("SchemaType is null!!!!!");
                        output = null;
                    }
                    break;

            }

            return output;
        }

        private static JToken Generate(KeyValuePair<string, JSchema> property)
        {
            var schema = property.Value;

            if (schema.Type == null)
            {
                if (schema.Const == null)
                {
                    schema.Type = JSchemaType.Object;
                }
            }
            JToken output = null;
            switch (schema.Type)
            {                
                case JSchemaType.String:
                    {
                        string val = "";
                        if (property.Key.EndsWith("-field-type"))
                        {
                            val = "ignored";
                        }
                        if (schema.Pattern == "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}")
                        {
                            val = Guid.Empty.ToString().ToLower();
                        }
                        else if (schema.Format == "ipv4")
                        {
                            val = "127.0.0.1";
                        }
                        else if (schema.Enum.Count > 0)
                        {
                            val = schema.Enum.First().ToString();
                        }
                        else if (property.Key == "command-id" || property.Key == "command-order")
                        {
                            val = DefaultCommandId;
                        }
                        
                        output = new JValue(val);
                    }
                    break;
                case JSchemaType.Number:
                    {
                        double num = 0.0;
                        if (property.Key == "scale")
                        {
                            num = 1.0;
                        }                       
                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Integer:
                    {
                        int num = 0;  
                        if (property.Key == "tolerance-ms")
                        {
                            num = DefaultToleranceMs;
                        }
                        else if (property.Key == "poll_period_ms")
                        {
                            num = DefaultPollPeriodMs;
                        }

                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Boolean:
                    output = new JValue(false);
                    break;
                case JSchemaType.Null:
                    output = JValue.CreateNull();
                    break;
                default:
                    if (schema.Const != null)
                    {
                        output = new JValue(schema.Const.ToString());
                    }
                    else
                    {
                        output = Generate(schema);
                    }
                    break;

            }

            return output;
        }

        private static JToken Generate(KeyValuePair<string, JSchema> property, ICsvRow row)
        {
            var schema = property.Value;

            if (schema.Type == null)
            {
                if (schema.Const == null)
                {
                    schema.Type = JSchemaType.Object;
                }
            }
            JToken output = null;
            switch (schema.Type)
            {
                case JSchemaType.Object:
                    JObject jObject = null;
                    if (schema.Properties != null)
                    {
                        jObject = new JObject();

                        foreach (var prop in schema.Properties)
                        {
                            jObject.Add(prop.Key, Generate(prop, row));
                        }
                        output = jObject;
                    }
                    if (schema.OneOf.Count > 0)
                    {
                        if (jObject == null)
                        {
                            jObject = new JObject();
                        }

                        bool found = false;

                        foreach (var oneOf in schema.OneOf)
                        {
                            foreach (var prop in oneOf.Properties)
                            {
                                if (prop.Value.Const?.ToString() == row.DataType)
                                {
                                    found = true;
                                    var first = Generate(oneOf, row);

                                    if (first is JObject)
                                    {
                                        var firstObj = first as JObject;
                                        foreach (var p in firstObj)
                                        {
                                            jObject.Add(p.Key, p.Value);
                                        }
                                    }
                                }
                            }
                        }

                        if (!found)
                        {
                            var item = schema.OneOf.FirstOrDefault();
                            if (item != null)
                            {
                                // Add first option
                                var first = Generate(item, row);

                                if (first is JObject)
                                {
                                    var firstObj = first as JObject;
                                    foreach (var prop in firstObj)
                                    {
                                        jObject.Add(prop.Key, prop.Value);
                                    }
                                }
                            }
                        }

                        output = jObject;
                    }
                    break;
                case JSchemaType.String:
                    {
                        string val = "";
                        if (property.Key.EndsWith("-field-type"))
                        {
                            val = "ignored";
                        }
                        if (schema.Pattern == "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}")
                        {
                            val = Guid.Empty.ToString().ToLower();
                        }
                        else if (schema.Format == "ipv4")
                        {
                            val = "127.0.0.1";
                        }
                        else if (schema.Enum.Count > 0)
                        {
                            val = schema.Enum.First().ToString();
                        }
                        else if (property.Key == "command-id" || property.Key == "command-order")
                        {
                            val = DefaultCommandId;
                        }

                        output = new JValue(val);
                    }
                    break;
                case JSchemaType.Number:
                    {
                        double num = 0.0;
                        if (property.Key == "scale")
                        {
                            num = 1;
                        }                        
                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Integer:
                    {
                        int num = 0;
                        if (property.Key == "index" || property.Key == "lower_index")
                        {
                            num = Convert.ToInt32(row.Index);
                        }
                        else if (property.Key == "upper_index")
                        {
                            var modbus = row as ModbusCsvRow;
                            if (modbus != null)
                            {
                                num = Convert.ToInt32(modbus.UpperIndex);
                            }
                        }
                        output = new JValue(num);
                    }
                    break;
                case JSchemaType.Boolean:
                    output = new JValue(false);
                    break;
                case JSchemaType.Null:
                    output = JValue.CreateNull();
                    break;
                default:
                    if (schema.Const != null)
                    {
                        output = new JValue(schema.Const.ToString());
                    }
                    else
                    {
                        output = Generate(schema);
                    }
                    break;

            }

            return output;
        }

        public static void LoadSchema(JSchema schema, Node node)
        {
            if (schema.Type == null)
            {
                if (schema.Const == null)
                {
                    schema.Type = JSchemaType.Object;
                }                
            }
            
            switch (schema.Type)
            {
                case JSchemaType.Object:
                    
                    if (schema.Properties != null)
                    {
                        foreach (var prop in schema.Properties)
                        {
                            Node n = new Node(prop.Key);
                            n.Schema = prop.Value;
                            node.AddNode(n);
                            LoadSchema(prop.Value, n);                            
                        }                        
                    }
                    if (schema.OneOf.Count > 0)
                    {                        
                        for (int i = 0; i < schema.OneOf.Count; ++i)
                        {
                            var item = schema.OneOf[i];
                            
                            LoadSchema(item, node);                            
                        }                        
                    }
                    break;
                case JSchemaType.Array:                    
                    for(int i = 0; i < schema.Items.Count; ++i)
                    {                        
                        var temp = Utils.RemoveEndArray(node.Name);
                        if (node.Path.IndexOf("gooseStructure.[0]") > 0)
                        {
                            break;
                        }

                        node.Name = temp + $".[{i}]";
                        LoadSchema(schema.Items[i], node);
                    }
                    break;

                case JSchemaType.String:                    
                    break;
                case JSchemaType.Number:                    
                    break;
                case JSchemaType.Integer:                   
                    break;
                case JSchemaType.Boolean:                    
                    break;
                case JSchemaType.Null:                    
                    break;
                default:                    
                    break;
            }
        }
    }
}
