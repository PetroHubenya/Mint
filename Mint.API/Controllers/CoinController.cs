using Interfaces.BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;

namespace Mint.API.Controllers
{
    [Route("api/[controller]")]
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
        
        //// Get coin by id.
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCoinByIdAsync(int id)
        //{
        //    Coin coin = await _coinService.GetCoinByIdAsync(id);

        //    if (coin == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(coin);
        //}
    }
}
