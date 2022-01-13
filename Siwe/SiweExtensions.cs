using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Signer;
using Nethereum.Siwe.Core;
using Nethereum.UI;

using siwe.Base36;
using siwe.Cacao;
using siwe.Messages;

namespace siwe
{
    public static class SiweExtensions
    {
        public static readonly string[] SIWE_CACHE_KEYS = new string[] { "siwe", "nonce", "ens" };

        public static string GetNonce(this SiweMessage msg)
        {
            // Instantiate random number generator using system-supplied value as seed (for now).
            var rand = new System.Random();

            string nonce = 
                Base36Converter.ConvertTo(System.Convert.ToInt64((rand.NextDouble() + 1).ToString().Replace(".", "")));

            if (nonce.Length < 8)
                nonce += nonce;

            return nonce;
        }

        public static void CheckDateRange(this SiweMessage message)
        {
            message.CheckExpirationDate();
            message.CheckNotBeforeDate();
        }

        public static void CheckExpirationDate(this SiweMessage message)
        {
            if (!String.IsNullOrEmpty(message.ExpirationTime))
            {
                DateTime ExpireTime = DateTime.Parse(message.ExpirationTime);
                if (DateTime.UtcNow.CompareTo(ExpireTime) > 0)
                {
                    throw new ExpiredMessageException();
                }
            }
        }

        public static void CheckNotBeforeDate(this SiweMessage message)
        {
            if (!String.IsNullOrEmpty(message.NotBefore))
            {
                DateTime NotBeforeTime = DateTime.Parse(message.NotBefore);
                if (DateTime.UtcNow.CompareTo(NotBeforeTime) < 0)
                {
                    throw new NotBeforeException();
                }
            }
        }

        public static void Ingest(this SiweMessage message, string siwePayload, bool bUseBNFParser = false)
		{
			// NOTE: Future implementation here
		}

        public static bool IsSiweCacheKey(string cacheKey)
        {
            return (!String.IsNullOrEmpty(cacheKey) && SIWE_CACHE_KEYS.Contains(cacheKey));
        }

        public static bool IsSiweCacheKeyValue(this KeyValuePair<string, object?> cacheKeyVal)
        {
            return (IsSiweCacheKey(cacheKeyVal.Key) && 
                    (cacheKeyVal.Value != null)     && 
                    cacheKeyVal.Value.GetType().Equals(typeof(string)));
        }

        /**
		 * This method parses all the fields in the object and creates a sign
		 * message according with the type defined.
		 * @returns {string} Returns a message ready to be signed according with the
		 * type defined in the object.
		 */
        public static string SignMessage(this SiweMessage msg)
        {
            return SiweMessageStringBuilder.BuildMessage(msg);
        }

        /// <summary>
        /// 
        /// Provides the default functionality associated with the SIWE signin in a Controller.
        /// 
        /// <param name="poOnixProduct">Refers to the current deserialized ONIX product being examined</param>
        /// </summary>
        public static void SiweSignIn(this SiweMessage message, Dictionary<string,string> tmpCache, bool anchorFlag = true)
        {
            string? nonce = String.Empty;

            if (message.Nonce == null)
            {
                // return BadRequest("Provided nonce is null");
                throw new NoNonceException("Provided nonce is null!");
            }

            if (tmpCache.ContainsKey("nonce"))
            {
                nonce = tmpCache["nonce"];
            }

            if ((message == null) || String.IsNullOrEmpty(message.Address) || String.IsNullOrEmpty(message.Signature))
                throw new InvalidSiweDataException("Malformed session");

            if (nonce != message.Nonce)
                throw new InvalidSiweDataException("Invalid nonce");

            message.Validate();

            tmpCache["siwe"] = SiweMessageStringBuilder.BuildMessage(message);

            if (anchorFlag)
            {
                var CacaoMsg = message.ConvertToCacao();

                // NOTE: To be implemented
                // Anchor CACAO message to a decentralized storage location (like Ceramic)
            }
        }

        /**
         * Validates the integrity of the fields of this object by matching its
         * signature.
         */
        public static void Validate(this SiweMessage message)
        {
            string messageBody = SiweMessageStringBuilder.BuildMessage(message);

            var signer = new EthereumMessageSigner();

            var recoveredAddress = signer.EncodeUTF8AndEcRecover(messageBody, message.Signature);

            // NOTE: Should we address any EIP-55 issues here?
            if (recoveredAddress.ToLower() != message?.Address?.ToLower())
            {
                throw new InvalidSignatureException();
            }

            message.CheckDateRange();

            message.ValidateStatement();

            message.ValidateUriAndResources();
        }

        /**
         * Validates the integrity of the fields of this object by matching its
         * signature.
         */
        public static async Task ValidateAsync(this SiweMessage message, IEthereumHostProvider provider = null)
        {
            if (provider != null)
            {
                var web3 = await provider.GetWeb3Async();

                // NOTE: Use the provider if possible? 
            }
            else
            {
                Validate(message);
            }
        }

        /**
         * Validates the statement provided in the SiweMessage instance.
         */
        public static void ValidateStatement(this SiweMessage message)
        {
            if (String.IsNullOrEmpty(message.Statement))
            {
                throw new InvalidSiweDataException("Statement of SiweMessage is empty");
            }

            if (message.Statement.Contains('\n'))
            {
                throw new InvalidSiweDataException("Statement of SiweMessage contains newlines");
            }
        }

        /**
         * Validates the resources provided in the SiweMessage instance.
         */
        public static void ValidateUriAndResources(this SiweMessage message)
        {
            if (!Uri.IsWellFormedUriString(message.Uri, UriKind.RelativeOrAbsolute))
            {
                throw new InvalidSiweDataException("Uri (" + message.Uri + ") of SiweMessage is an invalid URI");
            }

            if (message.Resources?.Count > 0)
            {
                foreach (var resource in message.Resources)
                {
                    if (!String.IsNullOrEmpty(resource) && !Uri.IsWellFormedUriString(resource, UriKind.RelativeOrAbsolute))
                    {
                        throw new InvalidSiweDataException("Resource (" + resource + ") of SiweMessage is an invalid URI");
                    }
                }
            }
        }

    }

}
