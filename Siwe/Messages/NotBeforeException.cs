namespace siwe.Messages
{
    public class NotBeforeException : SiweException
    {
        public NotBeforeException() : base()
        { }

        public NotBeforeException(string message) : base(message)
        { }
    }

}
