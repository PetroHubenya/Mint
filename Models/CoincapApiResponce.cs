using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CoinApiResponse
    {
        public CoincapData? Data { get; set; }
        public long Timestamp { get; set; }
    }
}
