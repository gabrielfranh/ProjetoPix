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
            await _clienteRepository.GetClientById(id);
            return Ok(id);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDTO cliente)
        {
            if (cliente == null) return BadRequest();

            var status = await _clienteRepository.Create(cliente);

            return Ok(status);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClienteDTO cliente)
        {
            if (cliente == null) return BadRequest();

            var status = await _clienteRepository.Update(cliente);

            return Ok(status);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteRepository.DeleteClienteById(id);
            return Ok(id);
        }
    }
}
