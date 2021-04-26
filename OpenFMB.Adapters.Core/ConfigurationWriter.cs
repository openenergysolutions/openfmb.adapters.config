// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models.Goose;
using System.Collections.Generic;
using System.IO;

namespace OpenFMB.Adapters.Core
{
    public class ConfigurationWriter
    {
        public static readonly string BaseAdapterDir = "adapters";
        public static readonly string MRID_OVERRIDE = "profiles[{0}].mapping.{1}.conductingEquipment.mRID.value";
        public static readonly string NAMED_OBJECT_OVERRIDE = "profiles[{0}].mapping.{1}.conductingEquipment.namedObject.name.value.value";

        public static void SetupDirectories(string pluginName)
        {
            if (!Directory.Exists(BaseAdapterDir))
            {
                Directory.CreateDirectory(BaseAdapterDir);
            }

            if (!Directory.Exists(Path.Combine(BaseAdapterDir, pluginName)))
            {
                Directory.CreateDirectory(Path.Combine(BaseAdapterDir, pluginName));
            }
        }

        public List<string> WriteGooseConfigurationFiles(List<GseControlSelection> selections, string directory)
        {            
            return null;
        }
    }
}
