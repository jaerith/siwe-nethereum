using Nethereum.Siwe.Core;

using siwe.Messages;

namespace siwe_rest_service.Models
{
    public class MySiweMessageAndDocSign : MySiweMessage
    {
        public DocSignData? docSignData = null;

        public MySiweMessageAndDocSign(DocSignData data) : base()
        { 
            docSignData = data;
        }
    }
}
