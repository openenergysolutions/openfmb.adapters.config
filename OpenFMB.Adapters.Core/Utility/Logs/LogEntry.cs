using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public class LogEntry
    {
        public string Message { get; set; }

        public Level Level { get; set; }

        public string RelatedException { get; set; }
    }
}
