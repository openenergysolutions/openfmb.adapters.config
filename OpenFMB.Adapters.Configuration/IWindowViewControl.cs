// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Configuration
{
    public interface IWindowViewControl
    {
        string Caption { get; }
        string WorkspaceDir { get; }
    }
}