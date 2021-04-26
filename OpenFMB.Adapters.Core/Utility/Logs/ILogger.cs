// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;

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
