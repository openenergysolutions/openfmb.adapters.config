// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core
{
    public interface Editable
    {
        string FullPath { get; }
        void Save();
        bool HasChanged();
    }
}
