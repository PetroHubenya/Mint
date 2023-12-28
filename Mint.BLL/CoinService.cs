using Interfaces.BusinessLogicLayer;
using Interfaces.DataAccessLayer;
using Models;

namespace Mint.BLL
{
    public class CoinService : ICoinService
    {
        private readonly IApiService _apiServiceCoingecko;

        public CoinService(IApiService apiServiceCoingecko)
        {
            _apiServiceCoingecko = apiServiceCoingecko;
        }

        // Get all coins from the third party API
        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            return await _apiServiceCoingecko.GetAllCoinsAsync();
        }

        public async Task<List<Coin>> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page)
        {
            List<Coin> result = await _apiServiceCoingecko.GetCoinsVsCurrencyInOrderPerPageAsync(vsCurrency, order, perPage, page);

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            throw new NotImplementedException();
        }
    }
}
