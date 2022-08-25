using Nethereum.Siwe.Core;

using siwe.Messages;

namespace siwe_rest_service.Models
{
    public class MySiweMessageAndText : MySiweMessage
    {
        public string? Text { get; set; }

        public string? Ens { get; set; }

        public string? Token { get; set; }

        public string? ErrorMsg { get; set; }

        public MySiweMessageAndText() : base()
        { }
    }
}
