using KeyAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KeyAPI.Repositories.Interfaces
{
    public interface IKeyRepository
    {
        public Task<List<KeyDTO>> GetAllKeysByCostumer(int costumerId);

        public Task<KeyDTO> GetKeyByCostumer(int keyId, int costumerId);

        public Task<IActionResult> Create([FromBody] KeyDTO key);

        public Task<IActionResult> Update([FromBody] KeyDTO key);

        public Task<IActionResult> Delete(int keyId, int costumerId);
    }
}
