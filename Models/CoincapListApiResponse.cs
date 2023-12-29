﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CoincapListApiResponse
    {
        public List<CoincapData>? Data { get; set; }
        public long Timestamp { get; set; }
    }
}
