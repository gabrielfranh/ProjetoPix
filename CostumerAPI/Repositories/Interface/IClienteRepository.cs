using CostumerAPI.DTO;

namespace CostumerAPI.Repositories.Interface
{
    public interface IClienteRepository
    {
        public Task<ClienteDTO> GetClientById(long id);

        public Task<bool> Create(ClienteDTO cliente);

        public Task<bool> Update(ClienteDTO cliente);

        public Task<bool> DeleteClienteById(long clienteId);
    }
}
