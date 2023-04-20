using KeyAPI.Repositories.Interfaces;
using KeyAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KeyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyController : ControllerBase
    {
        private readonly ILogger<KeyController> _logger;
        private readonly IKeyRepository _keyRepository;

        public KeyController(IKeyRepository keyRepository, ILogger<KeyController> logger)
        {
            _keyRepository = keyRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKeysByCostumer(int costumerId)
        {
            try
            {
                var keys = await _keyRepository.GetAllKeysByCostumer(costumerId);

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

        [HttpGet]
        public async Task<IActionResult> GetKeyByCostumer(int keyId, int costumerId)
        {
            try
            {
                var key = await _keyRepository.GetKeyByCostumer(keyId, costumerId);

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
                var createdKey = await _keyRepository.Create(key);

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
                var createdKey = await _keyRepository.Update(key);

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
                var key = await _keyRepository.GetKeyByCostumer(keyId, costumerId);

                if (key == null) return NotFound();

                await _keyRepository.Delete(keyId, costumerId);

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