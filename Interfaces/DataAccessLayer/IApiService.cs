using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataAccessLayer
{
    public interface IApiService
    {
        Task<Coin> GetCoinByIdAsync(string id);
        Task<List<Coin>> GetListOfAllCoinsAsync(string apiUrl);
        Task<List<Coin>> GetTopNCoinsAsync(int limit);
        Task<List<Coin>> SearchCoinsByNameOrSymbolAsync(string searchString);
    }
}
