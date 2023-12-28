using Interfaces.DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer.Coingecko
{
    public class ApiServiceCoincap : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiServiceCoincap(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public Task<List<Coin>> GetAllCoinsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Coin>> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page)
        {
            throw new NotImplementedException();
        }

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
    }
}
