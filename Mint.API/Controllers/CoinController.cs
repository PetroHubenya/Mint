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

        // Get top n coins.
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

        // Get coin by id.
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetCoinByIdAsync(string id)
        {
            Coin coin = await _coinService.GetCoinByIdAsync(id);

            if (coin == null)
            {
                return BadRequest();
            }

            return Ok(coin);
        }
    }
}
