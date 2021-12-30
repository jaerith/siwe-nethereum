using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siwe.Cacao
{
    /**
     ** A sample implementation of the proposed chain-agnostic capability header, as mentioned in:
     ** https://github.com/ChainAgnostic/CAIPs/pull/74
     *
     ** This class will someday be replaced by an official implementation in the Nethereum repo.
     **
     **/
    public class CacaoHeader
    {
        /// <summary>
        /// Type of message contained - for now, this will default to SIWE (i.e., "eip4361-eip191")
        /// </summary>
        public string Type { get; set; }

        public CacaoHeader()
        {
            Type = "eip4361-eip191";
        }
    }
}
