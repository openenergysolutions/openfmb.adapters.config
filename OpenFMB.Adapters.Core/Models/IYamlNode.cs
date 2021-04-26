// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using YamlDotNet.RepresentationModel;

namespace OpenFMB.Adapters.Core.Models
{
    public interface IYamlNode
    {
        string Name { get; }

        YamlNode ToYaml();
        void FromYaml(YamlNode yamlNode);
    }
}