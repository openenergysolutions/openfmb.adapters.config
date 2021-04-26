// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace OpenFMB.Adapters.Core.Utility
{
    public static class JSchemaExtensions
    {
        public static JSchema GetPropertyByName(this JSchema schema, string name)
        {
            foreach (var prop in schema.Properties)
            {
                if (prop.Key == "name" && prop.Value.Const?.ToString() == name)
                {
                    return prop.Value;
                }
            }
            return null;
        }

        public static JSchemaType SchemaType(this JToken token)
        {
            JTokenType jTokenType = token.Type;

            switch (jTokenType)
            {
                case JTokenType.Boolean:
                    return JSchemaType.Boolean;
                case JTokenType.Integer:
                    return JSchemaType.Integer;
                case JTokenType.Float:
                    return JSchemaType.Number;
                case JTokenType.Object:
                    return JSchemaType.Object;
                case JTokenType.Array:
                    return JSchemaType.Array;
                default:
                    return JSchemaType.String;
            }

        }
    }
}
