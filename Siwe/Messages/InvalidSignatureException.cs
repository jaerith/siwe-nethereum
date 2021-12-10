using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
