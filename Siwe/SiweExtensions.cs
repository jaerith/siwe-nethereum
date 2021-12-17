using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Signer;

using Nethereum.UI;

using siwe.Messages;

namespace siwe
{
    public static class SiweExtensions
    {
        const string CONST_ABNF_GRAMMAR =
@"
sign-in-with-ethereum =
    domain %s"" wants you to sign in with your Ethereum account:"" LF
    address LF
    LF
    [ statement LF ]
    LF
    %s""URI: "" URI LF
    %s""Version: "" version LF
    %s""Nonce: "" nonce LF
    %s""Issued At: "" issued-at
    [ LF %s""Expiration Time: "" expiration-time ]
    [ LF %s""Not Before: "" not-before ]
    [ LF %s""Request ID: "" request-id ]
    [ LF %s""Chain ID: "" chain-id ]
    [ LF %s""Resources:""
    resources ]

domain = dnsauthority

address = ""0x"" 40*40HEXDIG
    ; Must also conform to captilization
    ; checksum encoding specified in EIP-55
    ; where applicable (EOAs).

statement = *( reserved / unreserved / "" "" )
    ; The purpose is to exclude LF (line breaks).

version = ""1""

nonce = 8*( ALPHA / DIGIT )

issued-at = date-time
expiration-time = date-time
not-before = date-time

request-id = *pchar

chain-id = 1*DIGIT
    ; See EIP-155 for valid CHAIN_IDs.

resources = *( LF resource )

resource = ""- "" URI

; ------------------------------------------------------------------------------
; RFC 3986

URI           = scheme "":"" hier-part [ ""?"" query ] [ ""#"" fragment ]

hier-part     = ""//"" authority path-abempty
              / path-absolute
              / path-rootless
              / path-empty

scheme        = ALPHA *( ALPHA / DIGIT / ""+"" / ""-"" / ""."" )

authority     = [ userinfo ""@"" ] host [ "":"" port ]
userinfo      = *( unreserved / pct-encoded / sub-delims / "":"" )
host          = IP-literal / IPv4address / reg-name
port          = *DIGIT

IP-literal    = ""["" ( IPv6address / IPvFuture  ) ""]""

IPvFuture     = ""v"" 1*HEXDIG ""."" 1*( unreserved / sub-delims / "":"" )

IPv6address   =                            6( h16 "":"" ) ls32
              /                       ""::"" 5( h16 "":"" ) ls32
              / [               h16 ] ""::"" 4( h16 "":"" ) ls32
              / [ *1( h16 "":"" ) h16 ] ""::"" 3( h16 "":"" ) ls32
              / [ *2( h16 "":"" ) h16 ] ""::"" 2( h16 "":"" ) ls32
              / [ *3( h16 "":"" ) h16 ] ""::""    h16 "":""   ls32
              / [ *4( h16 "":"" ) h16 ] ""::""              ls32
              / [ *5( h16 "":"" ) h16 ] ""::""              h16
              / [ *6( h16 "":"" ) h16 ] ""::""

h16           = 1*4HEXDIG
ls32          = ( h16 "":"" h16 ) / IPv4address
IPv4address   = dec-octet ""."" dec-octet ""."" dec-octet ""."" dec-octet
dec-octet     = DIGIT                 ; 0-9
                 / %x31-39 DIGIT         ; 10-99
                 / ""1"" 2DIGIT            ; 100-199
                 / ""2"" %x30-34 DIGIT     ; 200-249
                 / ""25"" %x30-35          ; 250-255

reg-name      = *( unreserved / pct-encoded / sub-delims )

path-abempty  = *( ""/"" segment )
path-absolute = ""/"" [ segment-nz *( ""/"" segment ) ]
path-rootless = segment-nz *( ""/"" segment )
path-empty    = 0pchar

segment       = *pchar
segment-nz    = 1*pchar

pchar         = unreserved / pct-encoded / sub-delims / "":"" / ""@""

query         = *( pchar / ""/"" / ""?"" )

fragment      = *( pchar / ""/"" / ""?"" )

pct-encoded   = ""%"" HEXDIG HEXDIG

unreserved    = ALPHA / DIGIT / ""-"" / ""."" / ""_"" / ""~""
reserved      = gen-delims / sub-delims
gen-delims    = "":"" / ""/"" / ""?"" / ""#"" / ""["" / ""]"" / ""@""
sub-delims    = ""!"" / ""$"" / ""&"" / ""'"" / ""("" / "")""
              / ""*"" / ""+"" / "","" / "";"" / ""=""

; ------------------------------------------------------------------------------
; RFC 4501

dnsauthority    = host [ "":"" port ]
                             ; See RFC 3986 for the
                             ; definition of ""host"" and ""port"".

; ------------------------------------------------------------------------------
; RFC 3339

date-fullyear   = 4DIGIT
date-month      = 2DIGIT  ; 01-12
date-mday       = 2DIGIT  ; 01-28, 01-29, 01-30, 01-31 based on
                          ; month/year
time-hour       = 2DIGIT  ; 00-23
time-minute     = 2DIGIT  ; 00-59
time-second     = 2DIGIT  ; 00-58, 00-59, 00-60 based on leap second
                          ; rules
time-secfrac    = ""."" 1*DIGIT
time-numoffset  = (""+"" / ""-"") time-hour "":"" time-minute
time-offset     = ""Z"" / time-numoffset

partial-time    = time-hour "":"" time-minute "":"" time-second
                  [time-secfrac]
full-date       = date-fullyear ""-"" date-month ""-"" date-mday
full-time       = partial-time time-offset

date-time       = full-date ""T"" full-time

; ------------------------------------------------------------------------------
; RFC 5234

ALPHA          =  %x41-5A / %x61-7A   ; A-Z / a-z
LF             =  %x0A
                  ; linefeed
DIGIT          =  %x30-39
                  ; 0-9
HEXDIG         =  DIGIT / ""A"" / ""B"" / ""C"" / ""D"" / ""E"" / ""F""
";

        public const string REGEX_DOMAIN  = @"(([a-zA-Z]([a-zA-Z\\d-]*[a-zA-Z\\d])*)\\.)+[a-zA-Z]{2,}";
        public const string REGEX_ADDRESS = @"0x[a-zA-Z0-9]{40}";
        public const string REGEX_URI     = @"(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\\?([^#]*))?(#(.*))?";

        public const string REGEX_DATETIME =
            @"([0-9]+)-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])[Tt]([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]|60)(\\.[0-9]+)?(([Zz])|([\\+|\\-]([01][0-9]|2[0-3]):[0-5][0-9]))";

        public const string REGEX_REQUESTID = @"[-._~!$&'()*+,;=:@%a-zA-Z0-9]*";

        public static readonly string[] SIWE_CACHE_KEYS = new string[] { "siwe", "nonce", "ens" };

        public static void Ingest(this SiweMessage message, string siwePayload, bool bUseBNFParser = false)
		{
			// NOTE: Future implementation here
		}

        public static bool IsSiweCacheKey(string cacheKey)
        {
            return SIWE_CACHE_KEYS.Contains(cacheKey);
        }

        /// <summary>
        /// 
        /// Provides the default functionality associated with the SIWE signin in a Controller.
        /// 
        /// <param name="poOnixProduct">Refers to the current deserialized ONIX product being examined</param>
        /// </summary>
        public static void SiweSignIn(this SiweMessage message, Dictionary<string,string> tmpCache)
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
            /**
             ** NOTE: To be addressed later, when dealing with ABNF
             **
            else if (!String.IsNullOrEmpty(HttpContext.Session.GetString("siwe")))
            {
                //
                // NOTE: Get nonce from stored SIWE message
                //
                // nonce = HttpContext.Session.GetString("siwe");
                //
            }
             **/

            if ((message == null) || String.IsNullOrEmpty(message.Address) || String.IsNullOrEmpty(message.Signature))
                throw new InvalidSiweDataException("Malformed session");

            if (nonce != message.Nonce)
                throw new InvalidSiweDataException("Invalid nonce");

            message.Validate();

            tmpCache["siwe"] = message.ToMessage();
        }

        /**
         * Validates the integrity of the fields of this object by matching its
         * signature.
         */
        public static void Validate(this SiweMessage message)
        {
            string messageBody = message.SignMessage();

            var signer = new EthereumMessageSigner();

            var recoveredAddress = signer.EncodeUTF8AndEcRecover(messageBody, message.Signature);

            // NOTE: Should we address any EIP-55 issues here?
            if (recoveredAddress.ToLower() != message?.Address?.ToLower())
            {
                throw new InvalidSignatureException();
            }

            if (!String.IsNullOrEmpty(message.ExpirationTime))
            {
                DateTime currDateTime = DateTime.Now;
                DateTime expiredDateTime = DateTime.Parse(message.ExpirationTime);

                if (currDateTime > expiredDateTime)
                    throw new ExpiredMessageException();
            }
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

    }

}
