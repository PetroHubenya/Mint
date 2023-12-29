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
