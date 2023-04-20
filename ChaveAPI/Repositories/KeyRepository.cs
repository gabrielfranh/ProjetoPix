using AutoMapper;
using KeyAPI.Repositories.Interfaces;
using KeyAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KeyAPI.Repositories
{
    public class KeyRepository : IKeyRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public KeyRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SqlServer");
            _mapper = mapper;
        }

        public async Task<List<KeyDTO>> GetAllKeysByCostumer(int costumerId)
        {
            throw new NotImplementedException();
        }

        public async Task<KeyDTO> GetKeyByCostumer(int keyId, int costumerId)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Create([FromBody] KeyDTO key)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update([FromBody] KeyDTO key)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int keyId, int costumerId)
        {
            throw new NotImplementedException();
        }
    }
}
