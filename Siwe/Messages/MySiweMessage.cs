using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Siwe.Core;

namespace siwe.Messages
{
    public class MySiweMessage : SiweMessage
    { 
        public string? Signature { get; set; }
    }
}
