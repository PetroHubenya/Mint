using Helpers;
using Interfaces.DataAccessLayer;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class ApiServiceCoincap : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiServiceCoincap(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // Get coin by id.
        public async Task<Coin> GetCoinByIdAsync(string id)
        {
            // Main URL part should be moved to the settings file.

            string apiUrl = $"https://api.coincap.io/v2/assets/{id}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(jsonString))
                    {
                        throw new Exception("Empty or null JSON string.");
                    }
                    else
                    {
                        CoincapApiResponse? coincapApiResponse = JsonConvert.DeserializeObject<CoincapApiResponse>(jsonString);

                        if (coincapApiResponse == null)
                        {
                            throw new Exception("Failed to deserialize JSON string to CoincapApiResponse.");
                        }
                        else
                        {
                            CoincapData? coincapData = coincapApiResponse.Data;

                            if (coincapData == null)
                            {
                                throw new Exception("CoincapApiResponse.Data is null.");
                            }
                            else
                            {
                                // Mapper should be moved to the Business Logic Layer.

                                CoincapDataToCoinMapper coinMapper = new CoincapDataToCoinMapper();

                                Coin coin = coinMapper.MapCoincapToCoin(coincapData);

                                if (coin == null)
                                {
                                    throw new Exception("Failed to Map CoincapData to Coin.");
                                }
                                else
                                {
                                    return coin;
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new HttpRequestException($"Failed to retrieve coins. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------

        // Get top n coins.

        public async Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            string apiUrl = $"https://api.coincap.io/v2/assets?limit={limit}";

            List<Coin> coins = await GetListOfAllCoinsAsync(apiUrl);

            return coins;
        }        

        //----------------------------------------------------------------

        // Search coin by name or symbol.
        public async Task<List<Coin>> SearchCoinByNameOrSymbol(string searchString)
        {   
            string apiUrl = $"https://api.coincap.io/v2/assets";

            List<Coin> coins = await GetListOfAllCoinsAsync(apiUrl);
            
            var result = from coin in coins
                         where coin.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                               coin.Symbol.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                         select coin;
            
            return result.ToList();
        }

        //----------------------------------------------------------------

        // Get list of all coins.
        public async Task<List<Coin>> GetListOfAllCoinsAsync(string apiUrl)
        {
            // Instantiate string, that will contain API url, that returns the list of all coins. Later, this API url will be moved to the settings file.
            // string apiUrl = $"https://api.coincap.io/v2/assets";            
            try
            {   
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(jsonString))
                    {
                        throw new Exception("Empty or null JSON string.");
                    }
                    else
                    {
                        CoincapListApiResponse? coincapListApiResponse = JsonConvert.DeserializeObject<CoincapListApiResponse>(jsonString);

                        if (coincapListApiResponse?.Data == null)
                        {
                            throw new Exception("Failed to deserialize JSON string or missing 'data' property.");
                        }
                        else
                        {
                            // Mapper should be moved to the Business Logic Layer.

                            CoincapDataToCoinMapper coinMapper = new CoincapDataToCoinMapper();

                            List<Coin> coins = coinMapper.MapCoincapListToCoinList(coincapListApiResponse.Data);

                            if (coins == null)
                            {
                                throw new Exception("Failed to Map CoincapListApiResponse.Data to List<Coin>.");
                            }
                            else
                            {
                                return coins;
                            }
                        }
                    }
                }
                else
                {
                    throw new HttpRequestException($"Failed to retrieve coins. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
