using CostumerAPI.DTO;
using CostumerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CostumerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CostumerController : ControllerBase
    {
        private readonly ICostumerService _costumerService;
        private readonly ILogger<CostumerController> _logger;

        public CostumerController(ICostumerService costumerService, ILogger<CostumerController> logger)
        {
            _costumerService = costumerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCostumerById(int id)
        {
            try
            {
                var costumer = await _costumerService.GetCostumerById(id);
                
                if (costumer == null)
                    return NotFound("Costumer not found");

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
                var createdCostumer = await _costumerService.Create(costumer);

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

                var costumerProcurado = await _costumerService.GetCostumerById(costumer.Id);

                if (costumerProcurado == null)
                    return NotFound();

                var costumerAtualizado = await _costumerService.Update(costumer);

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
                var costumer = await _costumerService.GetCostumerById(id);

                if (costumer == null)
                    return NotFound();

                await _costumerService.DeleteCostumerById(id);

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
