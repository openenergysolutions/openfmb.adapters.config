// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenFMB.Adapters.Core
{
    public static class ProfileRegistry
    {
        public static readonly Dictionary<string, MessageDescriptor> Profiles = new Dictionary<string, MessageDescriptor>();
        public static readonly Dictionary<string, string> ProfileDeviceTagMap = new Dictionary<string, string>();

        static ProfileRegistry()
        {
            var assembly = Assembly.Load("OpenFMB.Models");
            var namespaces = assembly.GetTypes()
                         .Select(t => t.Namespace)
                         .Distinct();

            foreach (var ns in namespaces)
            {
                var types = assembly.GetTypes().Where(t => string.Equals(t.Namespace, ns, StringComparison.Ordinal));

                foreach (var t in types)
                {
                    if (t.Name.EndsWith("ReadingProfile")
                        || t.Name.EndsWith("EventProfile")
                        || t.Name.EndsWith("StatusProfile")
                        || t.Name.EndsWith("ControlProfile")
                        || t.Name.EndsWith("ScheduleProfile")
                        || t.Name.EndsWith("AvailabilityProfile")
                        || t.Name.EndsWith("RequestProfile")
                        || t.Name.EndsWith("CapabilityProfile")
                        || t.Name.EndsWith("CapabilityOverrideProfile"))
                    {
                        var prop = t.GetProperty("Descriptor");
                        if (prop != null)
                        {
                            var descriptor = prop.GetValue(null) as MessageDescriptor;
                            Profiles.Add(t.Name, descriptor);
                            var module = descriptor.FullName.Split('.')[0];
                            string tag;
                            if (!ProfileDeviceTagMap.TryGetValue(module, out tag))
                            {
                                foreach (var f in descriptor.Fields.InFieldNumberOrder())
                                {
                                    if (f.FieldType == FieldType.Message)
                                    {
                                        var mrid = f.MessageType.FindFieldByName("mRID");
                                        if (mrid == null)
                                        {
                                            SetDeviceTagForProfile(module, f);
                                        }
                                        else
                                        {
                                            if (f.Name != "identifiedObject")
                                            {
                                                ProfileDeviceTagMap[module] = f.Name;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SetDeviceTagForProfile(string module, FieldDescriptor fieldDescriptor)
        {
            foreach (var c in fieldDescriptor.MessageType.Fields.InFieldNumberOrder())
            {
                if (c.FieldType == FieldType.Message)
                {
                    var mrid = c.MessageType.FindFieldByName("mRID");

                    if (mrid == null)
                    {
                        SetDeviceTagForProfile(module, c);
                    }
                    else
                    {
                        if (c.Name != "identifiedObject")
                        {
                            ProfileDeviceTagMap[module] = fieldDescriptor.Name;
                            break;
                        }
                    }
                }
            }
        }

        public static string GetProfileFullName(string profileName)
        {
            MessageDescriptor descriptor;
            if (Profiles.TryGetValue(profileName, out descriptor))
            {
                return descriptor.ClrType.FullName;
            }
            else
            {
                throw new ArgumentException($"'{profileName}' is not a valid profile name.");
            }
        }

        public static string GetDeviceTagForProfile(string profileName)
        {
            string deviceName = null;
            MessageDescriptor descriptor;
            if (Profiles.TryGetValue(profileName, out descriptor))
            {
                var module = descriptor.FullName.Split('.')[0];
                ProfileDeviceTagMap.TryGetValue(module, out deviceName);
            }

            return deviceName;
        }

        public static string GetDeviceTagForModule(string module)
        {
            string tag = string.Empty;
            ProfileDeviceTagMap.TryGetValue(module, out tag);
            return tag;
        }

        public static bool IsReadingProfile(string profileName)
        {
            return profileName.EndsWith("ReadingProfile");
        }

        public static bool IsStatusProfile(string profileName)
        {
            return profileName.EndsWith("StatusProfile");
        }

        public static bool IsEventProfile(string profileName)
        {
            return profileName.EndsWith("EventProfile");
        }

        public static bool IsCapabilityProfile(string profileName)
        {
            return profileName.EndsWith("CapabilityProfile") || profileName.EndsWith("CapabilityOverrideProfile");
        }

        public static bool IsControlProfile(string profileName)
        {
            return profileName.EndsWith("ControlProfile");
        }

        public static ProfileType GetProfileType(string profileName)
        {
            if (IsControlProfile(profileName))
            {
                return ProfileType.Control;
            }
            if (IsEventProfile(profileName))
            {
                return ProfileType.Event;
            }
            if (IsStatusProfile(profileName))
            {
                return ProfileType.Status;
            }
            if (IsCapabilityProfile(profileName))
            {
                return ProfileType.Capability;
            }
            if (IsReadingProfile(profileName))
            {
                return ProfileType.Reading;
            }
            return ProfileType.Unknown;
        }
    }

    public enum ProfileType
    {
        Unknown,
        Control,
        Event,
        Reading,
        Status,
        Capability
    }
}
