using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siwe.Cacao
{
    /**
     ** A sample implementation of the proposed chain-agnostic capability object, as mentioned in:
     ** https://github.com/ChainAgnostic/CAIPs/pull/74
     *
     ** This class will someday be replaced by an official implementation in the Nethereum repo.
     **
     **/
    public class CacaoPayload
    {
        /// <summary>
        /// Authority requesting the signing
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// The ID of the signer (i.e., did:pkh)
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Resource that is the subject of the signing
        /// </summary>
        public string Aud { get; set; }

        /// <summary>
        /// Version of message 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Randomized token used to prevent replay attacks
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// Epoch time for issued date
        /// </summary>
        public long IssuedAt { get; set; }

        /// <summary>
        /// Epoch time for not-before date
        /// </summary>
        public long NotBefore { get; set; }

        /// <summary>
        /// Epoch time for expiration date
        /// </summary>
        public long ExpiresAt { get; set; }

        /// <summary>
        /// Human-readable ASCII assertion that the user will sign
        /// </summary>
        public string Statement { get; set; }

        /// <summary>
        /// System-specific identifier that may be used to uniquely refer to the sign-in request
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// List of information or references to information the user wishes to have resolved as part of authentication by the relying party. They are expressed as RFC 3986 URIs separated by `\n- `
        /// </summary>
        public List<string> Resources { get; set; }
    }
}
