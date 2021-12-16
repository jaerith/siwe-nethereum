namespace siwe.Messages
{
    public class InvalidSignatureException : SiweException
    {
        public InvalidSignatureException() : base()
        { }

        public InvalidSignatureException(string message) : base(message)
        { }
    }
}
