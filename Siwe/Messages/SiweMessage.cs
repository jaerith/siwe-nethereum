using System.Collections.Generic;

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
		public SignatureType? type { get; set; }

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
		public string toMessage()
        {
			string message = string.Empty;

			// Instantiate random number generator using system-supplied value as seed.
			var rand = new System.Random();

			string Header 
				= "{Domain} wants you to sign in with your Ethereum account:";

			string UriField = "URI: {Uri}";

			string prefix = Header + "\n" + UriField;

			string versionField = "Version: {Version}";

			/*
			 * NOTE: Not yet implemented
			 * 
			if (string.IsNullOrEmpty(Nonce))
			{
				this.nonce = (Math.random() + 1).toString(36).substring(4);
			}

			const nonceField = `Nonce: ${ this.nonce}`;

			const suffixArray = [uriField, versionField, nonceField];

			if (this.issuedAt)
			{
				Date.parse(this.issuedAt);
			}
			this.issuedAt = this.issuedAt
				? this.issuedAt
				: new Date().toISOString();
			suffixArray.push(`Issued At: ${ this.issuedAt}`);

			if (this.expirationTime)
			{
				const expiryField = `Expiration Time: ${ this.expirationTime}`;

				suffixArray.push(expiryField);
			}

			if (this.notBefore)
			{
				suffixArray.push(`Not Before: ${ this.notBefore}`);
			}

			if (this.requestId)
			{
				suffixArray.push(`Request ID: ${ this.requestId}`);
			}

			if (this.chainId)
			{
				suffixArray.push(`Chain ID: ${ this.chainId}`);
			}

			if (this.resources)
			{
				suffixArray.push(
	
					[`Resources:`, ...this.resources.map((x) => `- ${ x}`)].join(
'\n'
)
				);
			}

			let suffix = suffixArray.join('\n');

			if (this.statement)
			{
				prefix = [prefix, this.statement].join('\n\n');
			}

			return [prefix, suffix].join('\n\n');
			*/

			return message;
		}

		/**
		 * This method parses all the fields in the object and creates a sign
		 * message according with the type defined.
		 * @returns {string} Returns a message ready to be signed according with the
		 * type defined in the object.
		 */
		public string signMessage()
        {
			// NOTE: Not yet implemented
			return string.Empty;
		}


		#region Support Methods

		#endregion
	}
}