using Interfaces.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Mint.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;

        private readonly IIdValidationService _idValidationService;

        public CoinController(ICoinService coinService, IIdValidationService idValidationService)
        {
            _coinService = coinService;
            _idValidationService = idValidationService;
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
                bool idVerified = await _idValidationService.VerifyIdAsync(id);

                if (!idVerified)
                {
                    return BadRequest($"Invalid coin Id: {id}");
                }

                Coin coin = await _coinService.GetCoinByIdAsync(id);

                if (coin == null)
                {
                    return NotFound($"Coin not found for Id: {id}");
                }

                return Ok(coin);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid argument: {ex.ParamName}");
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

        // Get coin history by id and interval.
        [HttpGet("id={id}/history_interval={interval}")]
        public async Task<IActionResult> GetCoinHistoryByIdAndIntervalAsync(string id, string interval)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("The ID cannot be null or empty.");
                }

                if (!Enum.IsDefined(typeof(Interval), interval.ToLower()))
                {
                    throw new ArgumentException("Invalid interval received. List of valid intervals: m1, m5, m15, m30, h1, h2, h6, h12, d1");
                }

                List<CoinHistory> result = await _coinService.GetCoinHistoryByIdAndIntervalAsync(id, interval);

                if (result.Count == 0)
                {
                    return NoContent();
                }                

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid parameter: {ex.Message}");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
