// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using YamlDotNet.Serialization;

namespace OpenFMB.Adapters.Core.Utility
{
    public static class JTokenExtensions
    {
        public static string ToYamlString(this JToken token)
        {
            string json = JsonConvert.SerializeObject(token);

            if (!json.StartsWith("{"))
            {
                json = "{" + json + "}";
            }
            var dict = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

            Serializer serializer = new Serializer();
            var yaml = serializer.Serialize(dict);

            return yaml;
        }
    }
}
