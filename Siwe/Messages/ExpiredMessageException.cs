namespace siwe.Messages
{
    public class ExpiredMessageException : SiweException
    {
        public ExpiredMessageException() : base()
        { }

        public ExpiredMessageException(string message) : base(message)
        { }
    }

}
