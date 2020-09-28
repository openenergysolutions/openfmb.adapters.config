using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public enum Level
    {
        Debug = 0,
        Info = 10,
        Warning = 20,
        Error = 30,
        Fatal = 40,
    }

    public interface ILogger
    {
        void Log(Level level, string message, object tag = null);

        void Log(Level level, string message, Exception relatedException, object tag = null);
    }
}
