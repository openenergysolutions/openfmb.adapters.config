// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel;
using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models
{
    public interface ISessionSpecificConfig : INotifyPropertyChanged
    {
        void ToYaml(YamlMappingNode root);

        void SetEdition(string edition);
    }
}
