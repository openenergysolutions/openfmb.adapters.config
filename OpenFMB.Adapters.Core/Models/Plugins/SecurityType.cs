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
