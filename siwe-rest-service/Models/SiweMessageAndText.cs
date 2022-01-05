using Nethereum.Siwe.Core;

namespace siwe_rest_service.Models
{
    public class SiweMessageAndText : SiweMessage
    {
        public string? Text { get; set; }

        public string? Ens { get; set; }

        public string? Token { get; set; }

        public string? ErrorMsg { get; set; }

        public SiweMessageAndText() : base()
        { }
    }
}
