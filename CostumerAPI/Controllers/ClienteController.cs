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

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClienteById(int id)
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDTO cliente)
        {
            if (cliente == null) return BadRequest();

            var success = _clienteRepository.Create(cliente);

            return Ok(success);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClienteDTO cliente)
        {
            if (cliente == null) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}
