using KeyAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using KeyAPI.Services.Interfaces;

namespace KeyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyController : ControllerBase
    {
        private readonly ILogger<KeyController> _logger;
        private readonly IKeyService _keyService;

        public KeyController(IKeyService keyService, ILogger<KeyController> logger)
        {
            _keyService = keyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKeysByCostumer(int costumerId)
        {
            try
            {
                var keys = await _keyService.GetAllKeysByCostumer(costumerId);

                if (keys == null || keys.Any())
                    return NotFound();

                return Ok(keys);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying to get all keys.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetKeyByCostumer(int keyId, int costumerId)
        {
            try
            {
                var key = await _keyService.GetKeyByCostumer(keyId, costumerId);

                if (key == null)
                    return NotFound();

                return Ok(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error trying to get key {keyId} from costumer {costumerId}.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KeyDTO key)
        {
            if (key == null) return BadRequest();

            try
            {
                var createdKey = await _keyService.Create(key);

                _logger.LogInformation($"Key {key.KeyNumber} was created.");

                return Ok(createdKey);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to create key: {key}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] KeyDTO key)
        {
            if (key == null) return BadRequest();

            try
            {
                var createdKey = await _keyService.Update(key);

                _logger.LogInformation($"Key {key.KeyNumber} was updated.");

                return Ok(createdKey);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to update key: {key}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int keyId, int costumerId)
        {
            try
            {
                var key = await _keyService.GetKeyByCostumer(keyId, costumerId);

                if (key == null) return NotFound();

                await _keyService.Delete(keyId, costumerId);

                _logger.LogInformation($"Key {keyId} was deleted.");

                return Ok(keyId);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to delete key: {keyId} from costumer {costumerId}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}