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
            var existsCostumer = await ExistsCostumer(costumerId);

            if (!existsCostumer) return new List<KeyDTO>();

            var keys = await _keyRepository.GetAllKeysByCostumer(costumerId);

            return keys;
        }

        public async Task<KeyDTO> GetKeyByCostumer(int keyId, int costumerId)
        {
            var existsCostumer = await ExistsCostumer(costumerId);

            if (!existsCostumer) return new KeyDTO();

            var key = await _keyRepository.GetKeyByCostumer(keyId, costumerId);

            return key;
        }

        public async Task<KeyDTO> Create(KeyDTO key)
        {
            var existsCostumer = await ExistsCostumer(key.CostumerId);

            if (!existsCostumer) return new KeyDTO();

            var createdKey = await _keyRepository.Create(key);

            return createdKey;
        }

        public async Task<bool> Delete(int keyId, int costumerId)
        {
            var existsCostumer = await ExistsCostumer(costumerId);

            if (!existsCostumer) return false;

            await _keyRepository.Delete(keyId, costumerId);

            return true;
        }

        private async Task<bool> ExistsCostumer(int costumerId)
        {
            _logger.LogInformation($"Calling CostumerAPI GetCostumerById {costumerId}");
            var result = await _httpClient.GetAsync(BasePath + $"?id={costumerId}");

            var costumer = await HttpClientExtensions.ReadContentAs<CostumerDTO>(result, _logger);

            if (costumer == null)
            {
                _logger.LogError($"Failed to find costumer with id {costumerId}");
                return false;
            }

            return true;
        }
    }
}
