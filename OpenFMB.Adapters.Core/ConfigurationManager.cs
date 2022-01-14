// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core
{
    public class ConfigurationManager
    {
        public event EventHandler OnConfigurationSaved;
        public event EventHandler OnConfigurationClosed;
        public event EventHandler OnWorkspaceOpened;

        public event EventHandler<FileSystemEventArgs> OnFileSystemChanged;
        public event EventHandler<FileSystemEventArgs> OnFileSystemDeleted;
        public event EventHandler<FileSystemEventArgs> OnFileSystemCreated;
        public event EventHandler<RenamedEventArgs> OnFileSystemRenamed;

        public static ConfigurationManager Instance { get; } = new ConfigurationManager();

        private string _checkSum;

        private static ILogger _logger = MasterLogger.Instance;

        private readonly FileSystemWatcher _fileSystemWatcher;

        public Editable ActiveConfiguration { get; private set; }

        public string WorkingDirectory { get; private set; }

        public bool HasActiveWorkspace
        {
            get { return !string.IsNullOrWhiteSpace(WorkingDirectory); }
        }

        public static string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private ConfigurationManager() 
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _fileSystemWatcher.IncludeSubdirectories = true;

            _fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            _fileSystemWatcher.Created += FileSystemWatcher_Created;
            _fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            _fileSystemWatcher.Changed += FileSystemWatcher_Changed;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _fileSystemWatcher.EnableRaisingEvents = false;

            try
            {
                OnFileSystemChanged?.Invoke(this, e);                
            }
            finally
            {
                // naive way
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            OnFileSystemDeleted?.Invoke(this, e);
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            OnFileSystemCreated?.Invoke(this, e);
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            OnFileSystemRenamed?.Invoke(this, e);
        }

        public void SuspendFileWatcher()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        public void ResumeFileWatcher()
        {
            try
            {
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
            catch
            {
                // ignore.  first created file has no workspace
            }
        }

        public void OpenWorkspace(string directory)
        {
            CloseWorkspace();
            WorkingDirectory = directory;            

            _fileSystemWatcher.Path = WorkingDirectory;

            ResumeFileWatcher();

            OnWorkspaceOpened?.Invoke(this, EventArgs.Empty);
        }

        public void LoadConfiguration(string filePath, ConfigFileType? type = null)
        {
            if (type == null)
            {
                type = GetFileType(filePath);
            }

            if (type == ConfigFileType.MainAdapter)
            {
                AdapterConfiguration config = new AdapterConfiguration();
                config.Load(filePath);

                foreach (var p in config.Plugins.Plugins)
                {
                    ISessionable s = p as ISessionable;
                    if (s != null)
                    {
                        foreach (var session in s.Sessions)
                        {                            
                            var path = session.LocalFilePath;
                            var temp = Path.Combine(WorkingDirectory, path.TrimStart(new char[] { '\\', '/' }));
                            session.SessionConfiguration.Load(temp);
                        }
                    }
                }
                ActiveConfiguration = config;
            }
            else if (type == ConfigFileType.Template)
            {
                var relative = FileHelper.MakeRelativePath(WorkingDirectory, filePath);
                var session = Session.FromFile(WorkingDirectory, relative);
                session.Name = relative;

                ActiveConfiguration = session;
            }            
        }    
        
        public bool HasChanged()
        {
            return ActiveConfiguration != null && ActiveConfiguration.HasChanged();
        }

        public void Save()
        {
            Save(ActiveConfiguration);            
        }

        public void Save(Editable editable, bool suspendFileWatcher = true)
        {
            if (editable != null)
            {
                try
                {
                    if (suspendFileWatcher)
                    {
                        SuspendFileWatcher();
                    }

                    editable.Save();
                }
                finally
                {
                    if (suspendFileWatcher)
                    {
                        ResumeFileWatcher();
                    }
                }

                OnConfigurationSaved?.Invoke(this, EventArgs.Empty);
            }
        }

        public void LoadConfiguration(Editable configuration)
        {
            // open workspace first
            if (string.IsNullOrWhiteSpace(WorkingDirectory))
            {
                OpenWorkspace(Path.GetDirectoryName(configuration.FullPath));
            }

            // set active configuration
            ActiveConfiguration = configuration;
        }

        public void UnloadConfiguration()
        {
            ActiveConfiguration = null;            
        }

        public void CopyFile(string sourceFileName, string destFileName, bool suspendFileWatcher = true)
        {
            try
            {
                if (suspendFileWatcher)
                {
                    SuspendFileWatcher();
                }

                var directory = Path.GetDirectoryName(destFileName);
                Directory.CreateDirectory(directory);

                File.Copy(sourceFileName, destFileName, true);
            }
            finally
            {
                if (suspendFileWatcher)
                {
                    ResumeFileWatcher();
                }                
            }
        }

        public void CloseWorkspace()
        {
            SuspendFileWatcher();

            ActiveConfiguration = null;
            WorkingDirectory = string.Empty;
           
            _checkSum = string.Empty;
            OnConfigurationClosed?.Invoke(this, EventArgs.Empty);
        }

        public void UpdatePubSubTopics(TransportPluginType? type = null, bool reset = false)
        {
            if (ActiveConfiguration is AdapterConfiguration)
            {
                var plugins = (ActiveConfiguration as AdapterConfiguration).Plugins;

                if (reset)
                {
                    if (!type.HasValue)
                    {
                        plugins.NatsPlugin.Subscribes.Clear();
                        plugins.NatsPlugin.Publishes.Clear();

                        plugins.MqttPlugin.Subscribes.Clear();
                        plugins.MqttPlugin.Publishes.Clear();
                    }
                    else if (type == TransportPluginType.NATS)
                    {
                        plugins.NatsPlugin.Subscribes.Clear();
                        plugins.NatsPlugin.Publishes.Clear();
                    }
                    else if (type == TransportPluginType.MQTT)
                    {
                        plugins.MqttPlugin.Subscribes.Clear();
                        plugins.MqttPlugin.Publishes.Clear();
                    }
                    else if (type == TransportPluginType.ZENOH)
                    {
                        plugins.ZenohPlugin.Subscribes.Clear();
                        plugins.ZenohPlugin.Publishes.Clear();
                    }
                }

                foreach (var plugin in plugins.Plugins)
                {
                    var sessionable = plugin as ISessionable;
                    if (sessionable != null)
                    {
                        foreach (var session in sessionable.Sessions)
                        {
                            var profiles = session.SessionConfiguration.GetProfiles();
                            var isClient = PluginsSection.IsClientPlugin(session.PluginName);
                            foreach (var p in profiles)
                            {                                
                                var isReading = ProfileRegistry.IsReadingProfile(p.ProfileName) || ProfileRegistry.IsStatusProfile(p.ProfileName) || ProfileRegistry.IsEventProfile(p.ProfileName) || ProfileRegistry.IsCapabilityProfile(p.ProfileName);
                                if (!isClient)
                                {
                                    isReading = !isReading;
                                }

                                if (isReading)
                                {
                                    if (!type.HasValue)
                                    {
                                        UpdateTopics(session, p, plugins.NatsPlugin.Publishes, true);
                                        UpdateTopics(session, p, plugins.MqttPlugin.Publishes, true);
                                    }
                                    else if (type == TransportPluginType.NATS)
                                    {
                                        UpdateTopics(session, p, plugins.NatsPlugin.Publishes, true);
                                    }
                                    else if (type == TransportPluginType.MQTT)
                                    {
                                        UpdateTopics(session, p, plugins.MqttPlugin.Publishes, true);
                                    }
                                    else if (type == TransportPluginType.ZENOH)
                                    {
                                        UpdateTopics(session, p, plugins.ZenohPlugin.Publishes, true);
                                    }
                                }
                                else // if (ProfileRegistry.IsControlProfile(p.ProfileName))
                                {
                                    if (!type.HasValue)
                                    {
                                        UpdateTopics(session, p, plugins.NatsPlugin.Subscribes, false);
                                        UpdateTopics(session, p, plugins.MqttPlugin.Subscribes, false);
                                    }
                                    else if (type == TransportPluginType.NATS)
                                    {
                                        UpdateTopics(session, p, plugins.NatsPlugin.Subscribes, false);
                                    }
                                    else if (type == TransportPluginType.MQTT)
                                    {
                                        UpdateTopics(session, p, plugins.MqttPlugin.Subscribes, false);
                                    }
                                    else if (type == TransportPluginType.ZENOH)
                                    {
                                        UpdateTopics(session, p, plugins.ZenohPlugin.Subscribes, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateTopics(Session session, Profile profile, List<ITopic> topics, bool isPublishing)
        {
            var index = session.SessionConfiguration.GetProfiles().IndexOf(profile);

            var kvp = session.Overrides.FirstOrDefault(x => x.Key.StartsWith($"profiles[{index}]") && x.Key.EndsWith(".mRID.value"));
            string mrid = "*";
            if (kvp != null)
            {
                mrid = kvp.Value;
            }
            
            if (topics.FirstOrDefault(x => x.Profile == profile.ProfileName && x.Subject == mrid) == null)
            {
                if (isPublishing)
                {
                    topics.Add(new Publish()
                    {
                        Profile = profile.ProfileName,
                        Subject = mrid
                    });
                }
                else
                {
                    topics.Add(new Subscribe()
                    {
                        Profile = profile.ProfileName,
                        Subject = mrid
                    });
                }
            }
        }

        private static ConfigFileType GetFileType(string filePath)
        {
            try
            {
                var extension = Path.GetExtension(filePath).ToLower();
                if (extension.EndsWith(".yaml") || extension.EndsWith(".yml"))
                {
                    var text = File.ReadAllText(filePath);
                    if (text.IndexOf("logging:") >= 0 && text.IndexOf("plugins:") >= 0)
                    {
                        return ConfigFileType.MainAdapter;
                    }
                    else if (text.IndexOf("profiles:") > 0) 
                    {
                        return ConfigFileType.Template;
                    }
                }                                
            }
            catch
            {
               //
            }
            return ConfigFileType.Unknown;
        }

        public static FileInformation GetFileInformation(string filePath)
        {
            var fileInformation = new FileInformation();

            var extension = Path.GetExtension(filePath).ToLower();
            if (!extension.EndsWith("yml") && !extension.EndsWith("yaml"))
            {
                return fileInformation;
            }

            try
            {
                var stream = new YamlStream();

                using (var reader = new StreamReader(filePath))
                {
                    stream.Load(reader);
                    var doc = stream.Documents[0];

                    var map = doc.RootNode as YamlMappingNode;

                    if (map.ContainsKey("file"))
                    {                       
                        fileInformation.FromYaml(doc.RootNode["file"]);
                    }
                    else
                    {
                        var text = File.ReadAllText(filePath);
                        if (text.IndexOf("logging:") >= 0 && text.IndexOf("plugins:") >= 0)
                        {
                            fileInformation.Id = ConfigFileType.MainAdapter;
                        }
                        else if (text.IndexOf("profiles:") >= 0)
                        {
                            fileInformation.Id = ConfigFileType.Template;

                            if (text.IndexOf("outstation-ip:") >= 0)
                            {
                                fileInformation.Plugin = PluginsSection.Dnp3Master;
                            }
                            else if (text.IndexOf("auto_polling:") >= 0)
                            {
                                fileInformation.Plugin = PluginsSection.ModbusMaster;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Debug, ex.Message, ex);
            }
            return fileInformation;
        }
    }
}
