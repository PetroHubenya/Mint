using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CoincapHistoryListApiResponce
    {
        public List<CoincapHistoryData>? Data { get; set; }
        public long Timestamp { get; set; }
    }
}
