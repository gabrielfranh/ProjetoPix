using AutoMapper;
using KeyAPI.Repositories.Interfaces;
using KeyAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using KeyAPI.Models;

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
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"SELECT * FROM PixKey WHERE CostumerId = @costumerId";

                dbConnection.Open();

                var keys = await dbConnection.QueryAsync<List<Key>>(sQuery, new { costumerId });

                return _mapper.Map<List<KeyDTO>>(keys);
            }
        }

        public async Task<KeyDTO> GetKeyByCostumer(int keyId, int costumerId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"SELECT TOP 1 * FROM PixKey WHERE CostumerId = @costumerId and Id = @keyId";

                dbConnection.Open();

                var keys = await dbConnection.QueryAsync<List<Key>>(sQuery, new { costumerId, keyId });

                return _mapper.Map<KeyDTO>(keys);
            }
        }

        public async Task<KeyDTO> Create([FromBody] KeyDTO key)
        {
            var keyModel = _mapper.Map<Key>(key);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"INSERT INTO PixKey(CreationDate, Type, KeyNumber, CostumerId)
                                VALUES (@CreationDate, @Type, @KeyNumber, @CostumerId); SELECT CAST(scope_identity() AS INT)";

                dbConnection.Open();

                var Id = await dbConnection.ExecuteScalarAsync<int>(sQuery, keyModel);

                keyModel.Id = Id;

                return _mapper.Map<KeyDTO>(keyModel);
            }
        }

        public async Task Delete(int keyId, int costumerId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"DELETE FROM PixKey 
                                WHERE Id = @Id and CostumerId = @CostumerId";
                dbConnection.Open();

                var result = await dbConnection.ExecuteAsync(sQuery, new { keyId, costumerId });
            }
        }
    }
}
