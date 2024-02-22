// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Goose
{
    public class FCDA
    {
        public string LDInst { get; set; }
        public string Prefix { get; set; }
        public string LnClass { get; set; }
        public int LnInst { get; set; }
        public string DoName { get; set; }
        public string DaName { get; set; }
        public string LnTypeId { get; set; }
        public DataType DataType { get; set; }
        public string IEC61850DataType { get; set; }
        public string Name
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return $"{Prefix}{LnClass}{LnInst}.{DoName}.{DaName}";
        }
    }
}
