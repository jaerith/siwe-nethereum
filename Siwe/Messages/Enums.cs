using System.Runtime.Serialization;

namespace Siwe.Messages
{
    [DataContract(Namespace = "http://siwe.metdatadojo.wordpress.com", Name = "ErrorTypes")]
    public enum ErrorTypes
    {
        /**Thrown when the `validate()` function can verify the message. */
        [EnumMember]
        INVALID_SIGNATURE = 1, // "Invalid signature."

        /**Thrown when the `expirationTime` is present and in the past. */
        [EnumMember]
        EXPIRED_MESSAGE = 2, // 'Expired message.',

        [EnumMember]
        MALFORMED_SESSION = 3 // 'Malformed session.',
    }

    /**
     * Possible signature types that this library supports.
     */
    [DataContract(Namespace = "http://siwe.metdatadojo.wordpress.com", Name = "SignatureType")]
    public enum SignatureType
    {
        /**EIP-191 signature scheme */
        [EnumMember]
        PERSONAL_SIGNATURE = 1 // 'Personal signature',
    }
}
