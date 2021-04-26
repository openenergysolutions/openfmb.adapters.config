// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public class LogEntry
    {
        public string Message { get; set; }

        public Level Level { get; set; }

        public string RelatedException { get; set; }
    }
}
