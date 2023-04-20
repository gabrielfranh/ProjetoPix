using KeyAPI.DTO;
using KeyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KeyAPI.Services
{
    public class KeyService : IKeyService
    {
        public async Task<List<KeyDTO>> GetAllKeysByCostumer(int costumerId)
        {
            var costumer = await GetCostumerById(costumerId);
        }

        public async Task<KeyDTO> GetKeyByCostumer(int keyId, int costumerId)
        {
            var costumer = await GetCostumerById(costumerId);
        }

        public async Task<KeyDTO> Create([FromBody] KeyDTO key)
        {
            var costumer = await GetCostumerById(key.CostumerId);
        }

        public async Task<KeyDTO> Update([FromBody] KeyDTO key)
        {
            var costumer = await GetCostumerById(key.CostumerId);
        }

        public async Task Delete(int keyId, int costumerId)
        {
            var costumer = await GetCostumerById(costumerId);
        }

        private async Task<CostumerDTO?> GetCostumerById(int costumerId)
        {
            return null;
        }
    }
}
