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
            try
            {
                List<Coin> coins = await _coinService.GetTopNCoinsAsync(limit);

                if (coins.Count == 0)
                {
                    return NoContent();
                }

                return Ok(coins);
            }
            catch (ArgumentException)
            {   
                return BadRequest("Invalid limit value.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // Get coin by id.
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetCoinByIdAsync(string id)
        {
            try
            {
                Coin coin = await _coinService.GetCoinByIdAsync(id);

                if (coin == null)
                {
                    return NotFound($"Coin with Id '{id}' not found.");
                }

                return Ok(coin);
            }
            catch (ArgumentException)
            {   
                return BadRequest("Invalid coin Id.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }            
        }

        // Search coins by name or symbol
        [HttpGet("search={searchString}")]
        public async Task<IActionResult> SearchCoinByNameOrSymbolAsync(string searchString)
        {
            try
            {
                List<Coin> coins = await _coinService.SearchCoinByNameOrSymbolAsync(searchString);

                if (coins.Count == 0)
                {
                    return NoContent();
                }

                return Ok(coins);
            }
            catch (ArgumentException)
            {   
                return BadRequest("Invalid search string.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }            
        }
    }
}
