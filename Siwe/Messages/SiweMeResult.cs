using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siwe.Messages
{
    public record SiweMeResult
    {
        public string? Text { get; set; }

        public string? Address { get; set; }

        public string? Ens { get; set; }

        public SiweMeResult()
        { }
    }
}
