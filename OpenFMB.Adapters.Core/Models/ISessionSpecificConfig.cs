using System.ComponentModel;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models
{
    public interface ISessionSpecificConfig : INotifyPropertyChanged
    {
        void ToYaml(YamlMappingNode root);
    }
}
