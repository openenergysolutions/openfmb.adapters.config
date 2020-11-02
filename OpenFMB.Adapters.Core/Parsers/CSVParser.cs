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
using Google.Protobuf.Reflection;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenFMB.Adapters.Core.Parsers
{
    public class CSVParser
    {
        public static bool IsConfiguredForType(SessionablePluginType type, string filePath)
        {            
            using (var reader = new StreamReader(filePath))
            {
                var headers = reader.ReadLine().Split(',');
                if (headers.Length == 4 && type == SessionablePluginType.DNP3)
                {
                    return true;
                }
                else if (headers.Length == 5 && type == SessionablePluginType.MODBUS)
                {
                    return true;
                }
            }
            return false;
        }

        public static Profile Parse(string pluginName, string filePath)
        {
            List<ICsvRow> rows = new List<ICsvRow>();
            using (var reader = new StreamReader(filePath))
            {
                bool hasDescription = false;

                if (pluginName == PluginsSection.ModbusMaster)
                {
                    string line;                    

                    while ((line = reader.ReadLine()) != null)
                    {
                        var tokens = line.Split(',');
                        if (tokens[0].ToLower() == "path")
                        {
                            if (tokens[1].ToLower() == "description")
                            {
                                hasDescription = true;
                            }
                            continue;
                        }

                        if (hasDescription)
                        {
                            rows.Add(new ModbusCsvRow()
                            {
                                Path = tokens[0],
                                Description = tokens[1],
                                Index = tokens[2],
                                UpperIndex = tokens[3],
                                DataType = tokens[4],
                                Value = string.Empty
                            });
                        }
                        else
                        {
                            rows.Add(new ModbusCsvRow()
                            {
                                Path = tokens[0],
                                Description = string.Empty,
                                Index = tokens[1],
                                UpperIndex = tokens[2],
                                DataType = tokens[3],
                                Value = string.Empty
                            });
                        }
                    }
                }
                else if (pluginName == PluginsSection.Dnp3Master)
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var tokens = line.Split(',');
                        if (tokens[0].ToLower() == "path")
                        {
                            if (tokens[1].ToLower() == "description")
                            {
                                hasDescription = true;
                            }
                            continue;
                        }

                        if (hasDescription)
                        {
                            rows.Add(new Dnp3CsvRow()
                            {
                                Path = tokens[0],
                                Description = tokens[1],
                                Index = tokens[2],
                                DataType = tokens[3],
                                Value = string.Empty
                            });
                        }
                        else
                        {
                            rows.Add(new Dnp3CsvRow()
                            {
                                Path = tokens[0],
                                Description = string.Empty,
                                Index = tokens[1],
                                DataType = tokens[2],
                                Value = string.Empty
                            });
                        }
                    }
                }
                else
                {
                    throw new Exception($"Import from csv file for {pluginName} is not supported.");
                }
            }

            if (rows.Count > 0)
            {
                var profileName = rows[0].Path.Split('.')[0];

                return Profile.Create(profileName, pluginName, rows);
            }

            throw new Exception("Invalid template file.");
        }

        private static Type SetFieldTypeNode(Node node, string value = "mapped")
        {
            var property = node.GetType().GetProperty("Value");
            var v = Enum.Parse(property.PropertyType, value);
            property.SetValue(node, v);
            return property.PropertyType;
        }
    }

    public static class CSVParserExtension
    {
        public static string MappingPath(this string path)
        {
            var tokens = path.Split('.');
            return string.Join(".", tokens.SubArray(1, tokens.Length - 1));
        }

        private static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}