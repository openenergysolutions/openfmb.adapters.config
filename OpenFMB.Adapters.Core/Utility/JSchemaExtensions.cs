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

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Text;

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
