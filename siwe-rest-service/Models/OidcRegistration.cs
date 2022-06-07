namespace siwe_rest_service.Models
{
    public class OidcRegistration
    {
        public string? client_id { get; set; }

        public string? client_secret { get; set; }

        public string? registration_access_token { get; set; }

        public string? registration_client_uri { get; set; }

        public List<string>? redirect_uris { get; set; }
    }
}
