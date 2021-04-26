// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

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
