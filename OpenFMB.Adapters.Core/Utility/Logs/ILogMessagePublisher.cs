// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public interface ILogMessagePublisher
    {
        void Subscribe(ILogger logger);

        void Unsubscribe(ILogger logger);
    }
}
