// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Utility;

namespace OpenFMB.Adapters.Core.Parsers
{
    public interface ICsvRow
    {
        string Path { get; }
        string Description { get; }
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