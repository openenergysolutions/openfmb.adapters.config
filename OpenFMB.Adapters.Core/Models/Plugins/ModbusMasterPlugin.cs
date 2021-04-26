// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class ModbusMasterPlugin : BaseSessionablePlugin, IYamlNode, ISessionable
    {       
        public override string Name => PluginsSection.ModbusMaster;

        public override string SessionTagName => "sessions";

    }
}