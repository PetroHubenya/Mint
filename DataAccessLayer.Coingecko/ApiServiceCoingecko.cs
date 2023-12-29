using Interfaces.DataAccessLayer;
using Models;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataAccessLayer
{
    public class ApiServiceCoingecko : IApiService
    {
        public Task<Coin> GetCoinByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            throw new NotImplementedException();
        }
    }
}
