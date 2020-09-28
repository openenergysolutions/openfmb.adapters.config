using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public interface ILogMessagePublisher
    {
        void Subscribe(ILogger logger);

        void Unsubscribe(ILogger logger);
    }
}
