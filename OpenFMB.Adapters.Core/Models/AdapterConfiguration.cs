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

using OpenFMB.Adapters.Core.Models.Logging;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility.Logs;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models
{
    public class AdapterConfiguration : Editable
    {
        private static ILogger _logger = MasterLogger.Instance;

        public FileInformation FileInformation { get; set; }

        public LoggingSection Logging { get; } = new LoggingSection();

        public PluginsSection Plugins { get; } = new PluginsSection();

        public string FullPath { get; set; } = "";

        public string CheckSum { get; set; }

        public AdapterConfiguration()
        {
            FileInformation = new FileInformation()
            {
                Id = ConfigFileType.MainAdapter
            };
        }

        public void Save()
        {
            Save(FullPath);
        }

        public bool HasChanged()
        {
            bool flag = true;
            var stream = GetYamlStream();
                        
            using (StringWriter writer = new StringWriter())
            {
                stream.Save(writer, assignAnchors: false);
                var str = writer.ToString().Replace("\r\n", "\n");
                var checksum = Utility.Utils.ChecksumForString(str);
                flag = checksum != CheckSum;

                if (!flag)
                {
                    // now check for all session configs
                    foreach (var p in Plugins.Plugins)
                    {
                        ISessionable s = p as ISessionable;
                        if (s != null)
                        {
                            foreach (var session in s.Sessions)
                            {
                                if (session.SessionConfiguration != null && session.SessionConfiguration.HasChanged())
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return flag;
        }

        public YamlStream GetYamlStream()
        {
            var stream = new YamlStream();
            var root = new YamlMappingNode();
            var doc = new YamlDocument(root);

            stream.Add(doc);

            // file info            
            root.Add(FileInformation.Name, FileInformation.ToYaml());

            // Logging            
            root.Add(Logging.Name, Logging.ToYaml());

            // Plugins           
            root.Add(Plugins.Name, Plugins.ToYaml());

            return stream;
        }

        private void Save(string filePath)
        {
            var stream = GetYamlStream();
            using (var writer = new StringWriter())
            {
                stream.Save(writer, assignAnchors: false);

                // Replace
                var s = writer.ToString().Replace("\r\n", "\n");

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllText(filePath, s);

                CheckSum = Utility.Utils.ChecksumForString(s);
            }

            // Save sessions
            foreach (var p in Plugins.Plugins)
            {
                ISessionable s = p as ISessionable;
                if (s != null)
                {
                    foreach (var session in s.Sessions)
                    {                        
                        session.SessionConfiguration.Save(session.FullPath);
                    }
                }
            }

        }

        public string SaveForWeb(string filePath)
        {
            // replace Path.GetTempPath() with "outputs" dir
            var temp = Path.Combine("outputs", Path.GetRandomFileName());
            Directory.CreateDirectory(temp);

            var stream = GetYamlStream();
            using (var writer = new StringWriter())
            {
                stream.Save(writer, assignAnchors: false);

                // Replace
                var s = writer.ToString().Replace("\r\n", "\n");

                File.WriteAllText(Path.Combine(temp, filePath), s);

                CheckSum = Utility.Utils.ChecksumForString(s);
            }

            // Save sessions
            foreach (var p in Plugins.Plugins)
            {
                ISessionable s = p as ISessionable;
                if (s != null)
                {
                    foreach (var session in s.Sessions)
                    {
                        //if (session.Configuration != null && session.Configuration.HasChanged())
                        {                            
                            session.SessionConfiguration.Save(session.FullPath);
                        }
                    }
                }
            }

            // Zip
            var dest = Path.Combine("outputs", Path.GetRandomFileName());
            Directory.CreateDirectory(dest);
            string zipFile = Path.Combine(dest, Path.GetFileNameWithoutExtension(filePath) + ".zip");
            ZipFile.CreateFromDirectory(temp, zipFile);
            return zipFile;
        }

        public void Load(string filePath)
        {
            FullPath = filePath;

            var stream = new YamlStream();

            using (var reader = new StreamReader(filePath))
            {
                stream.Load(reader);
                var doc = stream.Documents[0];

                var map = doc.RootNode as YamlMappingNode;

                if (map.ContainsKey("logging"))
                {
                    Logging.FromYaml(doc.RootNode["logging"]);
                }
                else
                {
                    _logger.Log(Level.Warning, "Missing logging section. Default values shall be used.");
                }

                if (map.ContainsKey("plugins"))
                {
                    Plugins.FromYaml(doc.RootNode["plugins"]);
                }
                else
                {
                    _logger.Log(Level.Error, "Missing plugins section. Exit.");
                    throw new InvalidFilterCriteriaException("Missing plugin section in main configuration file.");
                }

                if (map.ContainsKey("file"))
                {
                    FileInformation.FromYaml(doc.RootNode["file"]);
                }  
                else
                {
                    _logger.Log(Level.Error, "Missing file information section in main adapter file.  Probably older configuration file is being used.");
                }

                reader.DiscardBufferedData();
                reader.BaseStream.Seek(0, SeekOrigin.Begin);                
                CheckSum = Utility.Utils.ChecksumForString(reader.ReadToEnd());
            }
        }
    }
}