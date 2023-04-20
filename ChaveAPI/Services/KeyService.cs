using KeyAPI.DTO;
using KeyAPI.Repositories.Interfaces;
using KeyAPI.Services.Interfaces;
using KeyAPI.Useful;
using Microsoft.AspNetCore.Mvc;

namespace KeyAPI.Services
{
    public class KeyService : IKeyService
    {
        private readonly IKeyRepository _keyRepository;
        private readonly HttpClient _httpClient;
        public string BasePath = "api/v1/Costumer";
        private readonly ILogger<KeyService> _logger;

        public KeyService(IKeyRepository keyRepository, HttpClient httpClient, ILogger<KeyService> logger)
        {
            _keyRepository = keyRepository;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<KeyDTO>> GetAllKeysByCostumer(int costumerId)
        {
            _logger.LogInformation($"Calling CostumerAPI GetCostumerById {costumerId}");
            var result = await _httpClient.GetAsync(BasePath + $"?id={costumerId}");

            var costumer = await HttpClientExtensions.ReadContentAs<CostumerDTO>(result, _logger);

            if (costumer == null) 
            {
                _logger.LogError($"Failed to find costumer with id {costumerId}");
                return new List<KeyDTO>();
            }

            var keys = await _keyRepository.GetAllKeysByCostumer(costumerId);

            return keys;
        }

        public async Task<KeyDTO> GetKeyByCostumer(int keyId, int costumerId)
        {
            //var costumer = await GetCostumerById(costumerId);

            var key = await _keyRepository.GetKeyByCostumer(keyId, costumerId);

            return key;
        }

        public async Task<KeyDTO> Create(KeyDTO key)
        {
            //var costumer = await GetCostumerById(key.CostumerId);

            var createdKey = await _keyRepository.Create(key);

            return createdKey;
        }

        public async Task<KeyDTO> Update([FromBody] KeyDTO key)
        {
            //var costumer = await GetCostumerById(key.CostumerId);

            var updatedKey = await _keyRepository.Update(key);

            return updatedKey;
        }

        public async Task Delete(int keyId, int costumerId)
        {
            //var costumer = await GetCostumerById(costumerId);

            await _keyRepository.Delete(keyId, costumerId);
        }

        private async Task<CostumerDTO?> GetCostumerById(int costumerId)
        {
            return null;
        }
    }
}
