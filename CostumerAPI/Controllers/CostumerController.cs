using CostumerAPI.DTO;
using CostumerAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CostumerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class CostumerController : ControllerBase
    {
        private readonly ICostumerRepository _costumerRepository;
        private readonly ILogger<CostumerController> _logger;

        public CostumerController(ICostumerRepository costumerRepository, ILogger<CostumerController> logger)
        {
            _costumerRepository = costumerRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCostumerById(int id)
        {
            try
            {
                var costumer = await _costumerRepository.GetCostumerById(id);

                if (costumer == null)
                    return NotFound();

                return Ok(costumer);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Failed to get costumer {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CostumerDTO costumer)
        {
            if (costumer == null) return BadRequest();

            try
            {
                var createdCostumer = await _costumerRepository.Create(costumer);

                _logger.LogInformation($"Costumer {costumer.Name} was created.");

                return Ok(createdCostumer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to create costumer {costumer.Id} {costumer.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CostumerDTO costumer)
        {
            try
            {
                if (costumer == null) return BadRequest();

                var costumerProcurado = await _costumerRepository.GetCostumerById(costumer.Id);

                if (costumerProcurado == null)
                    return NotFound();

                var costumerAtualizado = await _costumerRepository.Update(costumer);

                _logger.LogInformation($"Costumer {costumer.Name} with Id {costumer.Id} was updated.");

                return Ok(costumerAtualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update costumer {costumer.Id} {costumer.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var costumer = await _costumerRepository.GetCostumerById(id);

                if (costumer == null)
                    return NotFound();

                await _costumerRepository.DeleteCostumerById(id);

                _logger.LogInformation($"Costumer {costumer.Name} with Id {costumer.Id} was deleted.");

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete costumer with id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
