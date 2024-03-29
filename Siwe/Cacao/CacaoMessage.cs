﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siwe.Cacao
{
    /// <summary>
    /// A sample implementation of the proposed chain-agnostic capability object, as mentioned in:
    /// https://github.com/ChainAgnostic/CAIPs/pull/74
    ///
    /// This class will someday be replaced by an official implementation in the Nethereum repo.
    ///
    /// </summary>
    public class CacaoMessage
    {
        public CacaoHeader Header { get; set; }

        public CacaoPayload Payload { get; set; }
        
        public CacaoSignature Signature { get; set; }

        public CacaoMessage()
        {
            Header    = new CacaoHeader();
            Payload   = new CacaoPayload();
            Signature = new CacaoSignature();
        }
    }
}
