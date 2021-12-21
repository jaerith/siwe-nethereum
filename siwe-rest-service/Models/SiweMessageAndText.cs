using Nethereum.Siwe.Core;

namespace siwe_rest_service.Models
{
    public class SiweMessageAndText : SiweMessage
    {
        public string? Text { get; set; }

        public string? Ens { get; set; }

        public SiweMessageAndText() : base()
        { }

        public SiweMessageAndText(string unparsedMessage)
        { 
            // NOTE: Not yet implemented
        }

        public SiweMessageAndText(SiweMessage original)
        { 
            // NOTE: Not yet implemented
        }
    }
}
