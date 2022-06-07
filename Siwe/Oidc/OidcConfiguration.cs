using System.Collections.Generic;

namespace siwe.Oidc
{
    public class OidcConfiguration
    {
        public string? issuer { get; set; }

        public string? authorization_endpoint { get; set; }

        public string? token_endpoint { get; set; }

        public string? userinfo_endpoint { get; set; }

        public string? jwks_uri { get; set; }

        public string? registration_endpoint { get; set; }

        public List<string>? scopes_supported { get; set; }

        public List<string>? response_types_supported { get; set; }

        public List<string>? subject_types_supported { get; set; }

        public List<string>? id_token_signing_alg_values_supported { get; set; }

        public List<string>? userinfo_signing_alg_values_supported { get; set; }

        public List<string>? token_endpoint_auth_methods_supported { get; set; }

        public List<string>? claims_supported { get; set; }

        public string? op_policy_uri { get; set; }

        public string? op_tos_uri { get; set; }
    }
}
