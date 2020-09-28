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
using OpenFMB.Adapters.Core.Utility;

namespace OpenFMB.Adapters.Core.Parsers
{
    public interface ICsvRow
    {
        string Path { get; }
        string Description { get;  }
        string Index { get; }
        string DataType { get; }
        string Value { get; }

        string FormattedPath { get; }
    }

    public class ModbusCsvRow : ICsvRow
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public string Index { get; set; }
        public string UpperIndex { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }

        public string FormattedPath
        {
            get
            {
                return Utils.AddDotBeforeArray(Path);
            }
        }
    }

    public class Dnp3CsvRow : ICsvRow
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public string Index { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }

        public string FormattedPath
        {
            get
            {
                return Utils.AddDotBeforeArray(Path);
            }
        }
    }
}