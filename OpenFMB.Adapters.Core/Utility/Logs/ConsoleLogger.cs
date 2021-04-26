// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public class ConsoleLogger : ILogger
    {
        public void Log(Level level, string message, object tag = null)
        {
            this.Log(level, message, (Exception)null);
        }

        public void Log(Level level, string message, Exception relatedException, object tag = null)
        {
            Console.Write(((object)level).ToString() + ", " + message + ", " + (relatedException != null ? ((object)relatedException).ToString() : "") + "\n");
        }
    }
}
