using Interfaces.BusinessLogicLayer;
using Interfaces.DataAccessLayer;
using Microsoft.Extensions.Caching.Memory;
using Models;

namespace Mint.BLL
{
    public class IdValidationService : IIdValidationService
    {
        private readonly IApiService _apiService;

        private readonly IMemoryCache _memoryCache;

        public IdValidationService(IApiService apiService, IMemoryCache memoryCache)
        {
            _apiService = apiService;
            _memoryCache = memoryCache;
        }

        // Get list of all coin Ids.
        public async Task<List<string>> GetAllIdsAsync()
        {
            try
            {
                List<Coin> coins = await _apiService.GetListOfAllCoinsAsync();

                if (coins == null || coins.Count == 0)
                {
                    throw new Exception("No coins found.");
                }

                List<string> ids = new();

                foreach (Coin coin in coins)
                {
                    if (!string.IsNullOrEmpty(coin.Id))
                    {
                        ids.Add(coin.Id);
                    }
                }

                return ids;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get Ids from cache. If it is empty, then get Ids from the Api and save them to cache.
        public async Task<List<string>> GetAllIdsCacheAsync()
        {
            List<string>? output = _memoryCache.Get<List<string>>(key: "ids");

            if (output == null)
            {
                output = await GetAllIdsAsync();

                _memoryCache.Set(key: "ids", output, TimeSpan.FromMinutes(value: 1));
            }

            return output;
        }

        // Verify if the received ID is in the dictionary.
        public async Task<bool> VerifyId(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("Id cannot be null or empty.", nameof(id));
                }

                List<string> ids = await GetAllIdsCacheAsync();

                return ids.Contains(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while verifying the ID.", ex);
            }
        }
    }
}
