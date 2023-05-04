// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Globalization;

namespace OpenFMB.Adapters.Core.Utility
{
    public static class NumberConverter
    {
        public static int ToInteger(object val)
        {
            try
            {
                var s = val.ToString();
                if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    s = s.Substring(2);
                    return int.Parse(s, NumberStyles.AllowHexSpecifier);
                }
                else
                {
                    return int.Parse(s);
                }

            }
            catch
            {
                // Not a number
                return 0;
            }
        }

        public static decimal ToDecimal(object val)
        {
            decimal.TryParse(val.ToString(), out decimal d);
            return d;
        }
    }
}
