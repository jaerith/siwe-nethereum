using siwe.Messages;

namespace siwe_rest_service.Models
{
    public record SiweMessageAndText : SiweMessage
    {
        public string? Text { get; set; }

        public string? Ens { get; set; }

        public SiweMessageAndText() : base()
        { }

        public SiweMessageAndText(string unparsedMessage) : base(unparsedMessage)
        { }

        public SiweMessageAndText(SiweMessage original) : base(original)
        { }
    }
}
