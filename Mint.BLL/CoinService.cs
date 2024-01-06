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

        // Get coin by id.
        public async Task<Coin> GetCoinByIdAsync(string id)
        {
            Coin coin = await _apiService.GetCoinByIdAsync(id);

            if (coin == null)
            {
                throw new Exception();
            }

            return coin;
        }

        // Get top n number of coins.
        public async Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            List<Coin> coins = await _apiService.GetTopNCoinsAsync(limit);

            if (coins == null)
            {
                throw new Exception();
            }

            return coins;
        }

        // Search coins by name or symbol
        public async Task<List<Coin>> SearchCoinByNameOrSymbolAsync(string searchString)
        {
            List<Coin> coins = await _apiService.SearchCoinsByNameOrSymbolAsync(searchString);

            if(coins == null)
            {
                throw new Exception();
            }

            return coins;
        }
    }
}
