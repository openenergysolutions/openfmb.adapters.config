// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class Dnp3OutstationPlugin : BaseSessionablePlugin, IYamlNode, ISessionable
    {
        public override string Name => PluginsSection.Dnp3Outstation;

        public override string SessionTagName => "outstations";
    }
}