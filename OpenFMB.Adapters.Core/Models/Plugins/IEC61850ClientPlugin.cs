// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class IEC61850ClientPlugin : BaseSessionablePlugin, IYamlNode, ISessionable
    {
        public override string Name => PluginsSection.IEC61850Client;

        public override string SessionTagName => "cb";
    }
}