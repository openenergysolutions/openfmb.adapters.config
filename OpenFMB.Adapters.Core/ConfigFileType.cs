// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.Serialization;

namespace OpenFMB.Adapters.Core
{
    public static class ConfigFileTypeString
    {
        public const string Unknown = "unknown";
        public const string MainAdapter = "openfmb-adapter-main";
        public const string Template = "openfmb-adapter-template";

        public static ConfigFileType Convert(string fileType)
        {
            switch (fileType)
            {
                case MainAdapter:
                    return ConfigFileType.MainAdapter;
                case Template:
                    return ConfigFileType.Template;
                default:
                    return ConfigFileType.Unknown;
            }
        }

        public static string ToString(ConfigFileType fileType)
        {
            switch(fileType)
            {
                case ConfigFileType.MainAdapter:
                    return MainAdapter;
                case ConfigFileType.Template:
                    return Template;
                default:
                    return Unknown;
            }
        }
    }

    [Serializable]
    public enum ConfigFileType
    {
        [EnumMember(Value = "unknown")]
        Unknown,
        [EnumMember(Value = "openfmb-adapter-main")]
        MainAdapter,
        [EnumMember(Value = "openfmb-adapter-template")]
        Template
    }
}
