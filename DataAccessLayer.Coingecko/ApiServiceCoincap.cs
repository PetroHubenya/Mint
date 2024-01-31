using Helpers;
using Interfaces.DataAccessLayer;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace DataAccessLayer
{
    public class ApiServiceCoincap : IApiService
    {
        private string? _apiUrl;

        public ApiServiceCoincap(string? apiUrl)
        {
            _apiUrl = apiUrl;
        }

        // Get coin by id: https://api.coincap.io/v2/assets/{id}
        public async Task<Coin> GetCoinByIdAsync(string id)
        {
            try
            {
                string apiUrl = _apiUrl + "/" + id;

                HttpResponseMessage response;

                using (HttpClient client = new HttpClient())
                {
                    response = await client.GetAsync(apiUrl);
                }

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception($"Coin with ID '{id}' not found.");
                    }

                    throw new HttpRequestException($"Failed to retrieve coins. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonString))
                {
                    throw new Exception("Empty or null JSON string.");
                }

                CoincapApiResponse? coincapApiResponse = JsonConvert.DeserializeObject<CoincapApiResponse>(jsonString);

                if (coincapApiResponse == null)
                {
                    throw new Exception("Error deserializing JSON while fetching a coin by ID.");
                }

                CoincapData? coincapData = coincapApiResponse.Data;

                if (coincapData == null)
                {
                    throw new Exception("CoincapApiResponse.Data is null.");
                }
                
                CoincapDataToCoinMapper coinMapper = new CoincapDataToCoinMapper();
                Coin coin = coinMapper.MapCoincapToCoin(coincapData);

                if (coin == null)
                {
                    throw new Exception("Failed to Map CoincapData to Coin.");
                }

                return coin;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (JsonException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------

        // Get top n coins: https://api.coincap.io/v2/assets?limit={limit}
        public async Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            try
            {
                if (limit <= 0)
                {
                    throw new ArgumentException("Limit must be a positive integer.");
                }

                string apiUrl = _apiUrl + "?limit=" + limit;

                List<Coin> coins = await GetListOfAllCoinsAsync(apiUrl);

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
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //----------------------------------------------------------------

        // Search coins by name or symbol: https://api.coincap.io/v2/assets
        public async Task<List<Coin>> SearchCoinsByNameOrSymbolAsync(string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                {
                    throw new ArgumentException("Search string cannot be null or empty.");
                }

                if (_apiUrl == null)
                {
                    throw new ("_apiUrl is null");
                }

                List<Coin> coins = await GetListOfAllCoinsAsync(_apiUrl);

                if (coins == null || coins.Count == 0)
                {
                    throw new Exception("No coins found.");
                }

                IEnumerable<Coin> result = coins.Where(coin =>
                    (coin.Name?.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
                    (coin.Symbol?.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ?? false));

                if (result == null || result.Count() == 0)
                {
                    throw new Exception($"No coins found matching the search string '{searchString}'.");
                }

                return result.ToList();
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

        //----------------------------------------------------------------

        // Get list of all coins.
        public async Task<List<Coin>> GetListOfAllCoinsAsync(string apiUrl)
        {
            try
            {   
                if (string.IsNullOrWhiteSpace(apiUrl))
                {
                    throw new ArgumentException("API URL cannot be null or empty.");
                }

                HttpResponseMessage response;

                using (HttpClient client = new HttpClient())
                {
                    response = await client.GetAsync(apiUrl);
                }

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception($"Resource not found. URL: {apiUrl}");
                    }

                    throw new HttpRequestException($"Failed to retrieve coins. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}. URL: {apiUrl}");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    throw new Exception("Empty or null JSON string.");
                }
                
                CoincapListApiResponse coincapListApiResponse = JsonConvert.DeserializeObject<CoincapListApiResponse>(jsonString)
                    ?? throw new Exception("Failed to deserialize JSON string to CoincapListApiResponse.");

                if (coincapListApiResponse.Data == null)
                {
                    throw new Exception("Missing 'data' property in CoincapListApiResponse.");
                }
                
                CoincapDataToCoinMapper coinMapper = new CoincapDataToCoinMapper();

                List<Coin> coins = coinMapper.MapCoincapListToCoinList(coincapListApiResponse.Data)
                    ?? throw new Exception("Failed to map CoincapListApiResponse.Data to List<Coin>.");

                return coins;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (JsonException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //----------------------------------------------------------------
        // Get coin history by id and interval.

        public async Task<List<CoinHistory>> GetCoinHistoryByIdAndIntervalAsync(string id, string interval)
        {   
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("The ID cannot be null or empty.");
                }

                if (!Enum.IsDefined(typeof(Interval), interval.ToLower()))
                {
                    throw new ArgumentException("Invalid interval received.");
                }

                string apiUrl = _apiUrl + "/" + id + "/history?interval=" + interval;

                HttpResponseMessage response;

                using (HttpClient client = new HttpClient())
                {
                    response = await client.GetAsync(apiUrl);
                }

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception($"History data with ID '{id}' and interval '{interval}' not found.");
                    }

                    throw new HttpRequestException($"Failed to retrieve history data. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonString))
                {
                    throw new Exception("Empty or null JSON string.");
                }

                CoincapHistoryListApiResponce? coincapHistoryListApiResponce = JsonConvert.DeserializeObject<CoincapHistoryListApiResponce>(jsonString);

                if (coincapHistoryListApiResponce == null)
                {
                    throw new Exception("Error deserializing JSON while fetching a history by ID and interval.");
                }

                List<CoincapHistoryData>? coincapHistoryData = coincapHistoryListApiResponce.Data;

                if (coincapHistoryData == null)
                {
                    throw new Exception("coincapHistoryListApiResponce.Data is null.");
                }

                CoincapHistoryToCoinHistoryMapper coincapHistoryToCoinHistoryMapper = new();

                List<CoinHistory>? coinHistoryData = coincapHistoryToCoinHistoryMapper.MapCoincapHistoryListToCoinHistoryList(coincapHistoryData);

                if (coinHistoryData == null)
                {
                    throw new Exception("Failed to Map CoincapHistoryData to CoinHistory.");
                }

                return coinHistoryData;

            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (JsonException)
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
