using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
    }
}
