using System.Runtime.Serialization;

namespace siwe.Anchor
{
    /**
     * Possible anchor types that this library supports.
     */
    [DataContract(Namespace = "http://siwe.metdatadojo.wordpress.com", Name = "AnchorProviderType")]
    public enum AnchorProviderType
    {
        [EnumMember]
        IPFS = 1,

        [EnumMember]
        Ceramic,

        [EnumMember]
        Polygon,

        [EnumMember]
        Other

    }
}
