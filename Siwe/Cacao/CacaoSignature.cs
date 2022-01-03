using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siwe.Cacao
{
    /// <summary>
    /// A sample implementation of the proposed chain-agnostic capability object, as mentioned in:
    /// https://github.com/ChainAgnostic/CAIPs/pull/74
    ///
    /// This class will someday be replaced by an official implementation in the Nethereum repo.
    ///
    /// </summary>
    public class CacaoSignature
    {
        /// <summary>
        /// List of information or references to information the user wishes to have resolved as part of authentication by the relying party. They are expressed as RFC 3986 URIs separated by `\n- `
        /// </summary>
        public CacaoSignatureMeta Meta { get; set; }

        /// <summary>
        /// The hex-represented bytes that form the signature of the issuer
        /// </summary>
        public string SignatureInHex { get; set; }
    }
}
