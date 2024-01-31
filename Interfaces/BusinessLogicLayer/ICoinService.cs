using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.BusinessLogicLayer
{
    public interface ICoinService
    {
        Task<Coin> GetCoinByIdAsync(string id);
        Task<List<Coin>> GetTopNCoinsAsync(int limit);
        Task<List<Coin>> SearchCoinByNameOrSymbolAsync(string searchString);
        Task<List<CoinHistory>> GetCoinHistoryByIdAndIntervalAsync(string id, string interval);
    }
}
