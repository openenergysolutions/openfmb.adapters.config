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

using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OpenFMB.Adapters.Configuration
{
    public static class MigrationManager
    {
        private static readonly Dictionary<string, string> _nameChanges = new Dictionary<string, string>();
        
        static MigrationManager()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "OpenFMB.Adapters.Configuration.Migrations.NameChanges.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        var tokens = line.Split(':');
                        if (!line.StartsWith("//") && tokens.Length == 2)
                        {
                            _nameChanges[tokens[0]] = tokens[1];
                        }
                    }                    
                }
            }            
        }

        public static string GetOldName(string newName)
        {
            string oldName;
            if (_nameChanges.TryGetValue(newName, out oldName))
            {
                return oldName;
            }
            return null;
        }
    }
}
