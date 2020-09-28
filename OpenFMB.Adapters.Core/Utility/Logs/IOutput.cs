using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public enum OutputLevel
    {
        Info,
        ValidateOk,
        ValidateFailed
    }

    public interface IOutput
    {
        void Output(OutputLevel level, string message);
    }
}
