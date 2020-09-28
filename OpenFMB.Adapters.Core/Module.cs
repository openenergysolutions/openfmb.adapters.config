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

using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openfmb.breakermodule;
using openfmb.capbankmodule;
using openfmb.coordinationservicemodule;
using openfmb.essmodule;
using openfmb.generationmodule;
using openfmb.interconnectionmodule;
using openfmb.loadmodule;
using openfmb.metermodule;
using openfmb.reclosermodule;
using openfmb.regulatormodule;
using openfmb.reservemodule;
using openfmb.resourcemodule;
using openfmb.solarmodule;
using openfmb.switchmodule;

namespace OpenFMB.Adapters.Core
{
    public class Module
    {
        private static readonly List<FileDescriptor> _fileDescriptors = new List<FileDescriptor>()
        {
            BreakermoduleReflection.Descriptor,
            CapbankmoduleReflection.Descriptor,
            CoordinationservicemoduleReflection.Descriptor,
            EssmoduleReflection.Descriptor,
            GenerationmoduleReflection.Descriptor,
            InterconnectionmoduleReflection.Descriptor,
            LoadmoduleReflection.Descriptor,
            MetermoduleReflection.Descriptor,            
            ReclosermoduleReflection.Descriptor,
            RegulatormoduleReflection.Descriptor,
            ReservemoduleReflection.Descriptor,
            ResourcemoduleReflection.Descriptor,
            SolarmoduleReflection.Descriptor,
            SwitchmoduleReflection.Descriptor
        };

        public string Name { get; set; }
        public List<MessageDescriptor> Profiles { get; } = new List<MessageDescriptor>();

        public static List<Module> Build()
        {
            List<Module> modules = new List<Module>();
            foreach (var desc in _fileDescriptors)
            {
                Module module = new Module()
                {
                    Name = desc.Package
                };
                module.Profiles.AddRange(desc.MessageTypes.Where(x => x.Name.EndsWith("Profile")).ToList());
                modules.Add(module);
            }
            return modules;
        }
    }
}
