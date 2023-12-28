using Interfaces.DataAccessLayer;
using Models;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataAccessLayer.Coingecko
{
    public class ApiServiceCoingecko : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiServiceCoingecko(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }


        public async Task<List<Coin>> GetAllCoinsAsync()
        {   
            string apiUrl = "https://api.coingecko.com/api/v3/coins/list";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    if (content == null)
                    {
                        throw new Exception("Responce content is null.");
                    }
                    else
                    {
                        // List<Coin> coins = JsonConvert.DeserializeObject<List<Coin>>(content);

                        var coins = JsonSerializer.Deserialize<List<Coin>>(content);


                        if (coins == null)
                        {
                            throw new Exception("The coins variable is null.");
                        }
                        else
                        {
                            return coins;
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

        // Get list of coins vs specific currency in required order using pagination from the API
        // https://api.coingecko.com/api/v3/coins/markets?vs_currency=Usd&order=market_cap_desc&per_page=10&page=1

        public async Task<List<Coin>> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page)
        {
            string apiUrl = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency={vsCurrency}&order={order}&per_page={perPage}&page={page}&sparkline=false&locale=en";

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
                        List<Coin>? coins = JsonSerializer.Deserialize<List<Coin>>(jsonString);

                        if (coins == null)
                        {
                            throw new Exception("Failed to deserialize JSON string.");
                        }
                        else
                        {
                            return coins;
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

        public Task<List<Coin>> GetTopNCoinsAsync(int limit)
        {
            throw new NotImplementedException();
        }

        //


    }
}
