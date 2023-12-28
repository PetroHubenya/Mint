using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class CoincapToCoinMapper
    {
        public Coin MapCoincapToCoin(Coincap coincap)
        {
            if (coincap == null)
            {
                throw new ArgumentNullException(nameof(coincap));
            }

            return new Coin
            {
                Id = coincap.Id,
                Symbol = coincap.Symbol,
                Name = coincap.Name,
                PriceUsd = coincap.PriceUsd
            };
        }

        public List<Coin> MapCoincapListToCoinList(List<Coincap> coincaps)
        {
            if (coincaps == null)
            {
                throw new ArgumentNullException(nameof(coincaps));
            }

            return coincaps.Select(MapCoincapToCoin).ToList();
        }
    }
}
