using CostumerAPI.DTO;
using CostumerAPI.Repositories.Interface;

namespace CostumerAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public async Task<ClienteDTO> GetClientById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(ClienteDTO cliente)
        {
        }

        public async Task<ClienteDTO> Update(ClienteDTO cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCliente(long clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
