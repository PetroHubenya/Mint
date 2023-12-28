using Interfaces.BusinessLogicLayer;
using Interfaces.DataAccessLayer;
using Models;

namespace Mint.BLL
{
    public class CoinService : ICoinService
    {
        private readonly IApiService _apiService;

        public CoinService(IApiService apiService)
        {
            _apiService = apiService;
        }

        // Get all coins from the third party API
        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            return await _apiService.GetAllCoinsAsync();
        }

        public async Task<List<Coin>> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page)
        {
            List<Coin> result = await _apiService.GetCoinsVsCurrencyInOrderPerPageAsync(vsCurrency, order, perPage, page);

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        // Get top n number of coins.
        public async Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            List<Coin> result = await _apiService.GetTopNCoinsAsync(limit);

            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }
    }
}
