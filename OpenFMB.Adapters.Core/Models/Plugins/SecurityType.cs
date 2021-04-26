// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public enum SecurityType
    {
        none,
        tls_server_auth,
        tls_mutual_auth
    }

    public enum AuthenticationType
    {
        none,
        password,
        certificate
    }
}
