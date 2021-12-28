namespace siwe.Messages
{
    public class InvalidSiweDataException : SiweException
    {
        public InvalidSiweDataException() : base()
        { }

        public InvalidSiweDataException(string message) : base(message)
        { }
    }
}