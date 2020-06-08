using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCNBlockchain
{
    public class Transaction
    {
        public string sender { get; set; }
        public string recipient { get; set; }
        public string coin { get; set; }
        public string data { get; set; }
        public string date { get; set; }
    }
}
