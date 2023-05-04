﻿// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace OpenFMB.Adapters.Core.Parsers
{
    public class CSVParser
    {
        private static readonly Regex CsvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

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
                        var tokens = CsvParser.Split(line);
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
                        var tokens = CsvParser.Split(line);
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

                return Profile.CreateWithCsv(profileName, pluginName, rows);
            }

            throw new Exception("Invalid template file.");
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