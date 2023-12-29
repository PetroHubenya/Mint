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
        Task<List<Coin>> GetTopNCoinsAsync(int limit);
    }
}
