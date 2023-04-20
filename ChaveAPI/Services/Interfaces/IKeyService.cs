using KeyAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KeyAPI.Services.Interfaces
{
    public interface IKeyService
    {
        public Task<List<KeyDTO>> GetAllKeysByCostumer(int costumerId);
        public Task<KeyDTO> GetKeyByCostumer(int keyId, int costumerId);
        public Task<KeyDTO> Create([FromBody] KeyDTO key);
        public Task<KeyDTO> Update([FromBody] KeyDTO key);
        public Task Delete(int keyId, int costumerId);
    }
}
