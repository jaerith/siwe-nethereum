using System.Collections.Generic;
using System.Linq;
using System.Text;

using siwe.Base36;

namespace siwe.Messages
{
	public record SiweMessage
	{
		/**RFC 4501 dns authority that is requesting the signing. */
		public string? Domain { get; set; }

		/**Ethereum address performing the signing conformant to capitalization
		 * encoded checksum specified in EIP-55 where applicable. */
		public string? Address { get; set; }

		/**Human-readable ASCII assertion that the user will sign, and it must not
		 * contain `\n`. */
		public string? Statement { get; set; }

		/**RFC 3986 URI referring to the resource that is the subject of the signing
		 *  (as in the __subject__ of a claim). */
		public string? Uri { get; set; }

		/**Current version of the message. */
		public string? Version { get; set; }

		/**Randomized token used to prevent replay attacks, at least 8 alphanumeric
		 * characters. */
		public string? Nonce { get; set; }

		/**ISO 8601 datetime string of the current time. */
		public string? IssuedAt { get; set; }

		/**ISO 8601 datetime string that, if present, indicates when the signed
		 * authentication message is no longer valid. */
		public string? ExpirationTime { get; set; }

		/**ISO 8601 datetime string that, if present, indicates when the signed
		 * authentication message will become valid. */
		public string? NotBefore { get; set; }

		/**System-specific identifier that may be used to uniquely refer to the
		 * sign-in request. */
		public string? RequestId { get; set; }

		/**EIP-155 Chain ID to which the session is bound, and the network where
		 * Contract Accounts must be resolved. */
		public string? ChainId { get; set; }

		/**List of information or references to information the user wishes to have
		 * resolved as part of authentication by the relying party. They are
		 * expressed as RFC 3986 URIs separated by `\n- `. */
		public HashSet<string>? Resources { get; set; }

		/**Signature of the message signed by the wallet. */
		public string? Signature { get; set; }

		/**Type of sign message to be generated. */
		public SignatureType? Type { get; set; }

		public SiweMessage()
		{ }

		/**
		 * Creates a parsed Sign-In with Ethereum Message (EIP-4361) object from a
		 * string. If a string is used a ABNF parser is called to
		 * validate the parameter, otherwise the fields are attributed.
		 * @param param {string} Sign message as a string.
		 */
		public SiweMessage(string unparsedMessage)
		{
			ParseMessage(unparsedMessage);
		}

		/**
		 ** NOTE: Not yet implemented
		 **
		public SiweMessage(SiweMessage original)
		{
		
		}
		 */

		public void ParseMessage(string unparsedMessage)
		{
			// NOTE: Not yet implemented
		}

		/**
		 * This function can be used to retrieve an EIP-712 formated message for
		 * signature, although you can call it directly it's advised to use
		 * [signMessage()] instead which will resolve to the correct method based
		 * on the [type] attribute of this object, in case of other formats being
		 * implemented.
		 * @returns {string} EIP-712 formated message.
		 */
		public string ToMessage()
        {
			string message = string.Empty;

			string Header 
				= $"{Domain} wants you to sign in with your Ethereum account:\n{Address}";

			string uriField = $"URI: {Uri}";

			string prefix = Header;

			string versionField = $"Version: {Version}";

			if (string.IsNullOrEmpty(this.Nonce))
			{
				this.Nonce = GetNonce();
			}

			string nonceField = $"Nonce: {this.Nonce}";

			List<string> suffixArray = new List<string>() { uriField, versionField, nonceField };

			if (!string.IsNullOrEmpty(IssuedAt))
            {
				// NOTE: Should validation occur here?
            }

			IssuedAt = 
				!string.IsNullOrEmpty(IssuedAt) ? IssuedAt : System.DateTime.UtcNow.ToString("o");

			suffixArray.Add($"Issued At: ${IssuedAt}");

			if (!string.IsNullOrEmpty(ExpirationTime))
            {
				suffixArray.Add($"Expiration Time: ${ExpirationTime}");
            }

			if (!string.IsNullOrEmpty(NotBefore))
			{
				suffixArray.Add($"Not Before: ${NotBefore}");
			}

			if (!string.IsNullOrEmpty(RequestId))
			{
				suffixArray.Add($"Request ID: ${RequestId}");
			}

			if (!string.IsNullOrEmpty(ChainId))
			{
				suffixArray.Add($"Chain ID: ${ChainId}");
			}
			else
            {
				suffixArray.Add("Chain ID: 1");
			}

			if ((Resources != null) && (Resources.Count > 0))
            {
				suffixArray.Add("Resources:\n");
				Resources.ToList().ForEach(x => suffixArray.Add("- " + x + "\n"));
            }

			StringBuilder SuffixBuilder = new StringBuilder();
			suffixArray.ToList().ForEach(x => SuffixBuilder.Append(x).Append("\n"));

			StringBuilder MsgBuilder = new StringBuilder(prefix);
			if (!string.IsNullOrEmpty(Statement))
            {
				MsgBuilder.Append("\n\n").Append(Statement);
            }

			MsgBuilder.Append("\n\n").Append(SuffixBuilder.ToString());

			return MsgBuilder.ToString();
		}

		/**
		 * This method parses all the fields in the object and creates a sign
		 * message according with the type defined.
		 * @returns {string} Returns a message ready to be signed according with the
		 * type defined in the object.
		 */
		public string SignMessage()
        {
			string message = string.Empty;

			switch (this.Type)
			{
				case SignatureType.PERSONAL_SIGNATURE:
					{
						message = this.ToMessage();
						break;
					}

				default:
					{
						message = this.ToMessage();
						break;
					}
			}

			return message;
		}

		#region Support Methods

		public static string GetNonce()
        {
			// Instantiate random number generator using system-supplied value as seed.
			var rand = new System.Random();

			return
				Base36Converter.ConvertTo(System.Convert.ToInt64((rand.NextDouble() + 1).ToString().Replace(".", ""))).Substring(4);
		}

		#endregion
	}
}