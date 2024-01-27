using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class CoincapHistoryToCoinHistoryMapper
    {
        public CoinHistory MapCoincapHistoryToCoinHistroy(CoincapHistoryData coincapHistoryData)
        {
            if (coincapHistoryData == null)
            {
                throw new ArgumentNullException(nameof(coincapHistoryData));
            }

            return new CoinHistory
            {
                PriceUsd = coincapHistoryData.PriceUsd,
                Time = coincapHistoryData.Time
            };
        }

        public List<CoinHistory> MapCoincapHistoryListToCoinHistoryList(List<CoincapHistoryData> coincapHistoryList)
        {
            if (coincapHistoryList == null)
            {
                throw new ArgumentNullException(nameof(coincapHistoryList));
            }

            return coincapHistoryList.Select(MapCoincapHistoryToCoinHistroy).ToList();
        }
    }
}
