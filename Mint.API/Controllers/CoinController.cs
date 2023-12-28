using Interfaces.BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.Collections.Generic;

namespace Mint.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;

        public CoinController(ICoinService coinService)
        {
            _coinService = coinService;            
        }

        // Get all coins from the third party API
        [HttpGet]
        public async Task<IActionResult> GetAllCoinsAsync()
        {
            List<Coin> coins = await _coinService.GetAllCoinsAsync();

            if (coins.Count == 0)
            {
                return NotFound();
            }

            return Ok(coins);
        }

        // Get https://api.coingecko.com/api/v3/coins/markets?vs_currency=Usd&order=market_cap_desc&per_page=10&page=1
        [HttpGet("vs_currency={vsCurrency}/order={order}/per_page={perPage}/page={page}")]
        public async Task<IActionResult> GetCoinsVsCurrencyInOrderPerPageAsync(string vsCurrency, string order, int perPage, int page)
        {
            List<Coin> coins = await _coinService.GetCoinsVsCurrencyInOrderPerPageAsync(vsCurrency, order, perPage, page);

            if (coins.Count == 0)
            {
                return NotFound();
            }

            return Ok(coins);
        }

        // Get top n number of coins. Coingecko "per_page" = Coincap "limit"
        [HttpGet("limit={limit}")]
        public async Task<IActionResult> GetTopNCoinsAsync(int limit)
        {
            List<Coin> coins = await _coinService.GetTopNCoinsAsync(limit);

            if (coins.Count == 0)
            {
                return NotFound();
            }

            return Ok(coins);
        }
    }
}
