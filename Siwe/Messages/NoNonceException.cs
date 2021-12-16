namespace siwe.Messages
{
    public class NoNonceException : SiweException
    {
        public NoNonceException() : base()
        { }

        public NoNonceException(string message) : base(message)
        { }
    }
}
