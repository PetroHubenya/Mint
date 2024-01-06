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
            try
            {
                Coin coin = await _apiService.GetCoinByIdAsync(id);

                if (coin == null)
                {
                    throw new Exception($"Coin with ID '{id}' not found.");
                }

                return coin;
            }
            catch (ArgumentException)
            {                
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get top n number of coins.
        public async Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            try
            {
                List<Coin> coins = await _apiService.GetTopNCoinsAsync(limit);

                if (coins == null || coins.Count == 0)
                {
                    throw new Exception("No coins found.");
                }

                return coins;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Search coins by name or symbol
        public async Task<List<Coin>> SearchCoinByNameOrSymbolAsync(string searchString)
        {
            try
            {
                List<Coin> coins = await _apiService.SearchCoinsByNameOrSymbolAsync(searchString);

                if (coins == null || coins.Count == 0)
                {
                    throw new Exception($"No coins found for search string '{searchString}'.");
                }

                return coins;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
