﻿using System.Collections.Generic;

namespace Siwe.Messages
{
	public class SiweMessage
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

		#region Support Methods
		public void ParseMessage(string unparsedMessage)
		{
			// NOTE: Not yet implemented
		}
		#endregion
	}
}