using Interfaces.DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Coingecko
{
    public class ApiServiceCoincap : IApiService
    {
        public Task<List<Coin>> GetAllCoinsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Coin>> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page)
        {
            throw new NotImplementedException();
        }

        public Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {   
            throw new NotImplementedException();
        }
    }
}
