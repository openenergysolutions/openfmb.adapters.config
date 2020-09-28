using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace OpenFMB.Adapters.Core.Models
{
    public abstract class BaseSessionSpecifiConfig : ISessionSpecificConfig
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Category("General"), DisplayName("Plugin Name"), Description("Name of the plugin")]
        [JsonIgnore]
        public string PlugIn
        {
            get { return FileInformation.Plugin; }
            protected set { FileInformation.Plugin = value; }
        }

        [Browsable(false)]
        [JsonIgnore]
        public bool HasModified { get; set; }

        [JsonProperty("file")]
        [Browsable(false)]
        public FileInformation FileInformation { get; set; }

        public BaseSessionSpecifiConfig()
        {
            // TODO:: Inject file information
            FileInformation = new FileInformation()
            {
                Id = ConfigFileType.Template
            };
        }

        public virtual void ToYaml(YamlMappingNode root)
        {
            var json = JsonConvert.SerializeObject(this);
            var dict = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

            var yaml = new Serializer().Serialize(dict);

            var yamlMapping = new Deserializer().Deserialize(yaml, typeof(YamlMappingNode)) as YamlMappingNode;

            foreach (var kvp in yamlMapping)
            {
                root.Add(kvp.Key, kvp.Value);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            HasModified = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
