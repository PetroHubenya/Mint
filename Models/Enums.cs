using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum VsCurrency
    {
        usd,
        eur,
        cad,
        uah
    }
    public enum Order
    {
        market_cap_asc,
        market_cap_desc,
        volume_asc,
        volume_desc,
        id_asc,
        id_desc
    }
}
