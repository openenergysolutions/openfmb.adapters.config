// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using OpenFMB.Adapters.Core.Models.Schemas;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class Session : INotifyPropertyChanged, Editable
    {       
        public const string TemplateFilePathKey = "local-path";
        public const string SessionNameKey = "session-name";
        public const string PathKey = "path";
        public const string OverridesKey = "overrides";
        public const string KeyKey = "key";
        public const string ValueKey = "value";

        public event PropertyChangedEventHandler PropertyChanged;

        private SessionConfiguration _sessionConfiguration;        

        private string _path;
        private string _localFilePath;
        private string _fullPath;
        private string _sessionName = "Session";
        private string _edition;

        [Category("[General]"), DisplayName("Version"), Description("OpenFMB UML version"), ReadOnly(true)]
        public string Edition 
        { 
            get
            {
                if (!string.IsNullOrWhiteSpace(_edition))
                {
                    return _edition;
                }
                if (_sessionConfiguration != null)
                {
                    return _sessionConfiguration.Edition;
                }
                return SchemaManager.DefaultEdition;
            }
            set
            {
                _edition = value ?? value;
            }
        }

        private static readonly ILogger _logger = MasterLogger.Instance;       

        [Browsable(false)]
        public int Index { get; set; }

        [Category("[General]"), DisplayName("Session Name"), Description("Name of the session")]
        public string Name
        {
            get { return _sessionName; }
            set
            {
                _sessionName = value;               
                NotifyPropertyChanged("Name");
            }
        }

        [Category("[General]"), DisplayName("Plugin Name"), Description("Name of the plugin")]
        public string PluginName { get; private set; } = string.Empty;

        [Browsable(false)]
        public string FullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_fullPath))
                {
                    return Path.Combine(ConfigurationManager.Instance.WorkingDirectory, LocalFilePath);
                }
                else
                {
                    return _fullPath;
                }
            }
            set
            {
                _fullPath = value;
            }
        }
       
        [Category("Local File Path"), DisplayName("Template File Path (local)"), Description("Template file path relative to main configuration file when using this tool")]
        [ReadOnly(true)]
        public string LocalFilePath
        {
            get 
            {
                if (string.IsNullOrEmpty(_localFilePath))
                {                    
                    _localFilePath = FileHelper.ConvertToForwardSlash(PluginName.GetNextConfigFileName(PluginName + "-template"));
                }
                return _localFilePath; 
            }
            set
            {
                _localFilePath = value;

                NotifyPropertyChanged("LocalFilePath");
            }
        }                

        [Category("Runtime Environement"), DisplayName("Template File Path (runtime)"), Description("Template file path relative to main configuration file in runtime environment.  If the adapter is running as containerized app, the path should be prefixed with the mounted volumn name.")]        
        public string RuntimeFilePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_path))
                {
                    return FileHelper.ConvertToForwardSlash(LocalFilePath);
                }
                return _path;
            }
            set
            {
                _path = value;
                NotifyPropertyChanged("RuntimeFilePath");
            }
        }

        [Browsable(false)]
        public List<Override> Overrides { get; } = new List<Override>();

        [Browsable(false)]
        public SessionConfiguration SessionConfiguration
        {
            get
            {
                if (_sessionConfiguration == null)
                {
                    _sessionConfiguration = Create(plugin: PluginName);
                }
                return _sessionConfiguration;
            }
            set
            {
                _sessionConfiguration = value;
            }
        }

        [Browsable(false)]
        public bool IsStandAlone { get; set; }

        [JsonConstructor]
        public Session(string plugin)
        {
            PluginName = plugin;            
        }       

        public Session(string plugin, string localFilePath, string edition) : this(plugin)
        {            
            _localFilePath = localFilePath;            
            _edition = edition;
        }
        
        private SessionConfiguration Create(string plugin)
        {
            SessionConfiguration config = null;

            switch (plugin)
            {
                case PluginsSection.Dnp3Master:
                case PluginsSection.Dnp3Outstation:
                    config = new Dnp3SessionConfiguration(plugin, Edition);
                    break;                
                case PluginsSection.ModbusMaster:
                case PluginsSection.ModbusOutstation:
                    config = new ModbusSessionConfiguration(plugin, Edition);
                    break;                
                case PluginsSection.IEC61850Client:
                case PluginsSection.IEC61850Server:
                    config = new IEC61850SessionConfiguration(plugin, Edition);
                    break;
                case PluginsSection.IccpClient:
                case PluginsSection.IccpServer:
                    config = new IccpSessionConfiguration(plugin, Edition);
                    break;
                default:
                    throw new InvalidOperationException($"Plugin is not supported to have session configuration. [{plugin}]");                    
            }

            config.PropertyChanged += (sender, e) =>
            {
                NotifyPropertyChanged();
            };

            return config;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public YamlMappingNode ToYaml()
        {
            var ss = new YamlMappingNode();

            ss.Add(PathKey, FileHelper.ConvertToForwardSlash(RuntimeFilePath));            
            ss.Add(TemplateFilePathKey, LocalFilePath);
            ss.Add(SessionNameKey, Name);
            var overrides = new YamlSequenceNode();
            ss.Add(OverridesKey, overrides);
            foreach (var o in Overrides)
            {
                YamlMappingNode dic = new YamlMappingNode();
                dic.Add(KeyKey, o.Key);
                dic.Add(ValueKey, o.Value);
                overrides.Add(dic);
            }
            return ss;
        }

        public static Session FromYaml(string plugInName, YamlMappingNode master)
        {
            Session session = new Session(plugInName);            

            try
            {
                if (master.ContainsKey(PathKey))
                {
                    session.RuntimeFilePath = (master[PathKey] as YamlScalarNode).Value;
                }

                if (master.ContainsKey(TemplateFilePathKey))
                {
                    session.LocalFilePath = (master[TemplateFilePathKey] as YamlScalarNode).Value;
                }
                else
                {
                    session.LocalFilePath = session.RuntimeFilePath;
                }                

                if (master.ContainsKey(SessionNameKey))
                {
                    session.Name = (master[SessionNameKey] as YamlScalarNode).Value;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
            }

            var overrideSeq = master[OverridesKey] as YamlSequenceNode;
            foreach (var o in overrideSeq)
            {
                if (o is YamlScalarNode)
                {
                    // Older version of
                    session.Overrides.Add(new Override((o as YamlScalarNode).Value));
                }
                else if (o is YamlMappingNode)
                {
                    var map = o as YamlMappingNode;
                    var key = (map[KeyKey] as YamlScalarNode).Value;
                    var value = (map[ValueKey] as YamlScalarNode).Value;
                    session.Overrides.Add(new Override(key, value));
                }
            }
            return session;
        }

        public static Session FromFile(string baseDirectory, string localFilePath)
        {
            string plugin = null;
            string edition = null;

            var stream = new YamlStream();

            string filePath = Path.Combine(baseDirectory, localFilePath);

            using (var reader = new StreamReader(filePath))
            {
                stream.Load(reader);
                var doc = stream.Documents[0];

                var map = doc.RootNode as YamlMappingNode;               

                if (map.ContainsKey("file"))
                {
                    var fileInfo = map["file"] as YamlMappingNode;
                    if (fileInfo.ContainsKey("plugin"))
                    {
                        plugin = (fileInfo["plugin"] as YamlScalarNode)?.Value;
                    }
                    if (fileInfo.ContainsKey("edition"))
                    {
                        edition = (fileInfo["edition"] as YamlScalarNode)?.Value;
                    }
                }                
            }

            if (string.IsNullOrWhiteSpace(plugin))
            {
                var text = File.ReadAllText(filePath);
                if (text.IndexOf("outstation-ip:") >= 0)
                {
                    plugin = PluginsSection.Dnp3Master;
                }
                else if (text.IndexOf("auto_polling:") > 0)
                {
                    plugin = PluginsSection.ModbusMaster;
                }
                else
                {
                    throw new Exception("Unable to recognize template file.");
                }
            }

            Session session = new Session(plugin, localFilePath, edition);
            session.SessionConfiguration.Load(filePath);

            return session;
        }

        public void Reload(string baseDirectory, string localFilePath)
        {
            string plugin = null;

            var stream = new YamlStream();

            string filePath = Path.Combine(baseDirectory, localFilePath);

            using (var reader = new StreamReader(filePath))
            {
                stream.Load(reader);
                var doc = stream.Documents[0];

                var map = doc.RootNode as YamlMappingNode;

                if (map.ContainsKey("file"))
                {
                    var fileInfo = map["file"] as YamlMappingNode;
                    if (fileInfo.ContainsKey("plugin"))
                    {
                        plugin = (fileInfo["plugin"] as YamlScalarNode)?.Value;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(plugin))
            {
                var text = File.ReadAllText(filePath);
                if (text.IndexOf("outstation-ip:") >= 0)
                {
                    plugin = PluginsSection.Dnp3Master;
                }
                else if (text.IndexOf("auto_polling:") > 0)
                {
                    plugin = PluginsSection.ModbusMaster;
                }
                else
                {
                    throw new Exception("Unable to recognize template file.");
                }
            }

            if (PluginName != plugin)
            {
                throw new Exception($"Wrong plugin type for session to be reloaded.  Expected {PluginName} but got {plugin}.");
            }

            LocalFilePath = localFilePath;
            SessionConfiguration.Load(filePath);            
        }

        public void Save()
        {
            SessionConfiguration.Save(FullPath);
        }

        public bool HasChanged()
        {
            return SessionConfiguration.HasChanged();
        }
    }

    public class Override
    {
        public Override() { }
        public Override(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public Override(string s)
        {
            var tokens = s.Split('=');
            Key = tokens[0].Trim();
            Value = tokens[1].Trim();
        }

        [Category("Override"), DisplayName("Key"), Description("Override key.")]
        public string Key { get; set; }

        [Category("Override"), DisplayName("Value"), Description("Override value")]
        public string Value { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Key))
            {
                return Key;
            }
            return "Override";
        }
    }
}