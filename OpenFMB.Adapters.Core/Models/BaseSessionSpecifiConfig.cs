// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using System.Collections.Generic;
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

        public BaseSessionSpecifiConfig(string edition)
        {
            if (string.IsNullOrWhiteSpace(edition))
            {
                edition = SchemaManager.DefaultEdition;
            }
            // TODO:: Inject file information
            FileInformation = new FileInformation()
            {
                Id = ConfigFileType.Template,
                Edition = edition,
            };
        }

        public virtual void ToYaml(YamlMappingNode root)
        {
            if (string.IsNullOrEmpty(FileInformation.Edition))
            {
                FileInformation.Edition = SchemaManager.DefaultEdition;
            }

            if (string.IsNullOrEmpty(FileInformation.Version))
            {
                FileInformation.Version = ConfigurationManager.Version;
            }

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

        public void SetEdition(string edition)
        {
            FileInformation.Edition = edition;
        }
    }

    public class EditionStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.AddRange(SchemaManager.SupportEditions);
            return new StandardValuesCollection(list);
        }
    }
}
