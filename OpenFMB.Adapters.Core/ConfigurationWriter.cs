/*
 * Copyright 2017-2020 Duke Energy Corporation and Open Energy Solutions, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Goose;
using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
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
