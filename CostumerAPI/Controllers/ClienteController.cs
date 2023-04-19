using CostumerAPI.DTO;
using CostumerAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CostumerAPI.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger _logger;

        public ClienteController(IClienteRepository clienteRepository, ILogger logger)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetClienteById(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetClientById(id);

                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error trying get client by id");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDTO cliente)
        {
            try
            {
                if (cliente == null) return BadRequest();

                var clienteCriado = await _clienteRepository.Create(cliente);

                return Ok(clienteCriado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying create client");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClienteDTO cliente)
        {
            try
            {
                if (cliente == null) return BadRequest();

                var clienteProcurado = await _clienteRepository.GetClientById(cliente.ClienteId);

                if (clienteProcurado == null)
                    return NotFound();

                var clienteAtualizado = await _clienteRepository.Update(cliente);

                return Ok(clienteAtualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying update client");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetClientById(id);

                if (cliente == null)
                    return NotFound();

                await _clienteRepository.DeleteClienteById(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error trying delete client");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
