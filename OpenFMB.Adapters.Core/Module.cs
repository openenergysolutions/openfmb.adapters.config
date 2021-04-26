// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Google.Protobuf.Reflection;
using System.Collections.Generic;
using System.Linq;
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
