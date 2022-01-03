using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Signer;
using Nethereum.Siwe.Core;
using Nethereum.UI;

using siwe.Base36;
using siwe.Messages;

namespace siwe.Cacao
{
    public static class CacaoExtensions
    {
        /// <summary>
        /// Converts SIWE message to CACAO object, as mentioned in:
        /// https://github.com/ChainAgnostic/CAIPs/pull/74
        /// </summary>
        public static CacaoMessage ConvertToCacao(this SiweMessage msg)
        {
            CacaoMessage cacaoMsg = new CacaoMessage();

            cacaoMsg.Payload.Aud = msg.Uri;

            if (!String.IsNullOrEmpty(msg.Domain))
            {
                cacaoMsg.Payload.Domain = msg.Domain;
            }

            if (!String.IsNullOrEmpty(msg.Address))
            {
                var chainId = (!String.IsNullOrEmpty(msg.ChainId)) ? msg.ChainId : "1";

                cacaoMsg.Payload.Issuer =
                    String.Format("did:pkh:eip155:{0}:{1}", chainId, msg.Address);
            }

            if (!String.IsNullOrEmpty(msg.ExpirationTime))
            {
                cacaoMsg.Payload.ExpiresAt = GetEpochTime(msg.ExpirationTime);
            }

            if (!String.IsNullOrEmpty(msg.IssuedAt))
            {
                cacaoMsg.Payload.IssuedAt = GetEpochTime(msg.IssuedAt);
            }

            if (!String.IsNullOrEmpty(msg.NotBefore))
            {
                cacaoMsg.Payload.NotBefore = GetEpochTime(msg.NotBefore);
            }

            if (!String.IsNullOrEmpty(msg.Statement))
            {
                cacaoMsg.Payload.Statement = msg.Statement;
            }

            if (!String.IsNullOrEmpty(msg.Nonce))
            {
                cacaoMsg.Payload.Nonce = msg.Nonce;
            }

            if (!String.IsNullOrEmpty(msg.Version))
            {
                cacaoMsg.Payload.Version = msg.Version;
            }

            if (msg.Resources?.Count > 0)
            {
                cacaoMsg.Payload.Resources = new List<string>(msg.Resources);
            }

            if (!String.IsNullOrEmpty(msg.Signature))
            {
                cacaoMsg.Signature.SignatureInHex = msg.Signature;
            }

            return cacaoMsg;
        }

        /// <summary>
        /// Converts CACAO object (as mentioned in https://github.com/ChainAgnostic/CAIPs/pull/74to) to SIWE message
        /// </summary>
        public static SiweMessage ConvertToSiwe(this CacaoMessage msg)
        {
            SiweMessage siweMsg = new SiweMessage();

            if (!String.IsNullOrEmpty(msg.Payload?.Domain))
            {
                siweMsg.Domain = msg.Payload.Domain;
            }

            if (!String.IsNullOrEmpty(msg.Payload?.Issuer))
            {
                if (msg.Payload.Issuer.Contains(":"))
                {
                    var issuerParts = msg.Payload.Issuer.Split(':');
                    if (issuerParts.Length >= 4)
                    {
                        siweMsg.Address = issuerParts[3];
                        if (issuerParts.Length >= 5)
                        {
                            siweMsg.ChainId = issuerParts[4];
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(msg.Payload?.Statement))
            {
                siweMsg.Statement = msg.Payload.Statement;
            }

            if (!String.IsNullOrEmpty(msg.Payload?.Aud))
            {
                siweMsg.Uri = msg.Payload.Aud;
            }

            if (!String.IsNullOrEmpty(msg.Payload?.Version))
            {
                siweMsg.Version = msg.Payload.Version;
            }

            if (!String.IsNullOrEmpty(msg.Payload?.Nonce))
            {
                siweMsg.Nonce = msg.Payload.Nonce;
            }

            if (msg.Payload?.IssuedAt > 0)
            {
                siweMsg.IssuedAt = GetDateTime(msg.Payload.IssuedAt).ToString("O");
            }

            if (msg.Payload?.NotBefore > 0)
            {
                siweMsg.NotBefore = GetDateTime(msg.Payload.NotBefore).ToString("O");
            }

            if (msg.Payload?.ExpiresAt > 0)
            {
                siweMsg.ExpirationTime = GetDateTime(msg.Payload.ExpiresAt).ToString("O");
            }

            if (msg.Payload?.Resources?.Count > 0)
            {
                siweMsg.Resources = new List<string>(msg.Payload.Resources);
            }

            return siweMsg;
        }

        public static DateTime GetDateTime(long epochSeconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(epochSeconds).DateTime;
        }

        public static long GetEpochTime(string dateTimeFormat)
        {
            long epochTime = 0;

            DateTime targetDateTime = DateTime.MinValue;
            if (DateTime.TryParse(dateTimeFormat, out targetDateTime))
            {
                DateTimeOffset epochDateTime = new DateTimeOffset(targetDateTime);
                epochTime = epochDateTime.ToUnixTimeSeconds();
            }

            return epochTime;
        }
    } 
}
