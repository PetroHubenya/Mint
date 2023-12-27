using Interfaces.DataAccessLayer;
using Models;

namespace DataAccessLayer.Coingecko
{
    public class ApiServiceCoingecko : IApiServiceCoingecko
    {   
        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            List<Coin> result =
            [
                new Coin
                {
                    Id = "01coin",
                    Symbol = "zoc",
                    Name = "01coin"
                },
                new Coin
                {
                    Id = "0chain",
                    Symbol = "zcn",
                    Name = "Zus"
                }
            ];

            // Simulate an asynchronous operation.
            await Task.Delay(1000);

            return result;
        }
    }
}
