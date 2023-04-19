using CostumerAPI.DTO;

namespace CostumerAPI.Repositories.Interface
{
    public interface IClienteRepository
    {
        public Task<ClienteDTO> GetClientById(long id);

        public Task<ClienteDTO> Create(ClienteDTO cliente);

        public Task<ClienteDTO> Update(ClienteDTO cliente);

        public Task DeleteClienteById(long clienteId);
    }
}
