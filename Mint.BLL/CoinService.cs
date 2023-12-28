﻿using Interfaces.BusinessLogicLayer;
using Interfaces.DataAccessLayer;
using Models;

namespace Mint.BLL
{
    public class CoinService : ICoinService
    {
        private readonly IApiServiceCoingecko _apiServiceCoingecko;

        public CoinService(IApiServiceCoingecko apiServiceCoingecko)
        {
            _apiServiceCoingecko = apiServiceCoingecko;
        }

        // Get all coins from the third party API
        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            return await _apiServiceCoingecko.GetAllCoinsAsync();
        }

        public Task<List<Coin>> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page)
        {
            throw new NotImplementedException();
        }
    }
}
