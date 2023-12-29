using Helpers;
using Interfaces.DataAccessLayer;
using Models;
using Newtonsoft.Json;

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
                        Coincap coincap = JsonConvert.DeserializeObject<Coincap>(jsonString);

                        if (coincap == null)
                        {
                            throw new Exception("Failed to deserialize JSON string to coincap.");
                        }
                        else
                        {
                            CoincapToCoinMapper coinMapper = new CoincapToCoinMapper();

                            Coin coin = coinMapper.MapCoincapToCoin(coincap);

                            if (coin == null)
                            {
                                throw new Exception("Failed to Map Coincap to Coin.");
                            }
                            else
                            {   
                                return coin;
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

        // Get top n coins.
        public async Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            string apiUrl = $"https://api.coincap.io/v2/assets?limit={limit}";

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
                        var apiResponse = JsonConvert.DeserializeObject<CoincapApiResponse>(jsonString);

                        if (apiResponse?.Data == null)
                        {
                            throw new Exception("Failed to deserialize JSON string or missing 'data' property.");
                        }
                        else
                        {
                            CoincapToCoinMapper coinMapper = new CoincapToCoinMapper();

                            List<Coin> coins = coinMapper.MapCoincapListToCoinList(apiResponse.Data);

                            if (coins == null)
                            {
                                throw new Exception("Failed to Map Coincap to Coin.");
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
