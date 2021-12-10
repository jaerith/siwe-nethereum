using System;

namespace siwe.Messages
{
    public class SiweException: Exception
    {
        public SiweException() : base()
        {}

        public SiweException(string message) : base(message)
        { }
    }
}
