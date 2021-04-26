// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public class GooseSubPlugin : BaseSessionablePlugin, IYamlNode, ISessionable
    {
        public override string Name => PluginsSection.GooseSub;

        public override string SessionTagName => "goCb";
    }
}