using Interfaces.DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mint.BLL
{
    public class IdValidationService
    {
        private readonly IApiService _apiService;

        public IdValidationService(IApiService apiService)
        {
            _apiService = apiService;
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
        
        // Verify if the received ID is in the dictionary.


    }
}
