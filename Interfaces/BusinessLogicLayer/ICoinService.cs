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
        Task<List<Coin>> GetAllCoinsAsync();
        Task<List<Coin>> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page);
        Task<List<Coin>> GetTopNCoinsAsync(int limit);
    }
}
