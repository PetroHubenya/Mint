using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class CoincapDataToCoinMapper
    {
        public Coin MapCoincapToCoin(CoincapData coincapData)
        {
            if (coincapData == null)
            {
                throw new ArgumentNullException(nameof(coincapData));
            }

            return new Coin
            {
                Id = coincapData.Id,
                Rank = coincapData.Rank,
                Symbol = coincapData.Symbol,
                Name = coincapData.Name,
                Supply = coincapData.Supply,
                MaxSupply = coincapData.MaxSupply,
                MarketCapUsd = coincapData.MarketCapUsd,
                VolumeUsd24Hr = coincapData.VolumeUsd24Hr,
                PriceUsd = coincapData.PriceUsd,
                ChangePercent24Hr = coincapData.ChangePercent24Hr,
                Vwap24Hr = coincapData.Vwap24Hr,
            };
        }

        public List<Coin> MapCoincapListToCoinList(List<CoincapData> coincaps)
        {
            if (coincaps == null)
            {
                throw new ArgumentNullException(nameof(coincaps));
            }

            return coincaps.Select(MapCoincapToCoin).ToList();
        }
    }
}
