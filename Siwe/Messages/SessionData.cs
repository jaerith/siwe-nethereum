using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Siwe.Core; 

namespace siwe.Messages
{
    public record SessionData
    {
        public SiweMessage? Siwe { get; init; }

        public string? Nonce { get; init; }

        public string? Ens { get; init; }
    }
}
