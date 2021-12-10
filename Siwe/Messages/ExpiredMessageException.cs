using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
