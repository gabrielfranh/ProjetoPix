using AutoMapper;
using CostumerAPI.DTO;
using CostumerAPI.Models;
using CostumerAPI.Repositories.Interface;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CostumerAPI.Repositories
{
    public class CostumerRepository : ICostumerRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public CostumerRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SqlServer");
            _mapper = mapper;
        }

        public async Task<CostumerDTO> GetCostumerById(long id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"SELECT * FROM Costumer
                                WHERE Id = @id";

                dbConnection.Open();

                var result = await dbConnection.QueryFirstOrDefaultAsync<Costumer>(sQuery, new { id });

                return _mapper.Map<CostumerDTO>(result);
            }
        }

        public async Task<CostumerDTO> Create(CostumerDTO costumer)
        {
            var costumerModel = _mapper.Map<Costumer>(costumer);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"INSERT INTO Costumer(Name, PasswordHash, Username, Role)
                                VALUES (@Name, @PasswordHash, @Username, @Role); SELECT CAST(scope_identity() AS INT)";

                dbConnection.Open();

                var Id = await dbConnection.ExecuteScalarAsync<int>(sQuery, costumerModel);

                return costumer;
            }
        }

        public async Task<CostumerDTO> Update(CostumerDTO costumer)
        {
            var costumerModel = _mapper.Map<Costumer>(costumer);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"UPDATE Costumer 
                                SET Name = @Name 
                                WHERE Id = @Id";

                dbConnection.Open();

                await dbConnection.ExecuteAsync(sQuery, costumerModel);

                return _mapper.Map<CostumerDTO>(costumerModel);
            }
        }

        public async Task DeleteCostumerById(long Id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"DELETE FROM Costumer 
                                WHERE Id = @Id";
                dbConnection.Open();

                var result = await dbConnection.ExecuteAsync(sQuery, new { Id });
            }
        }
    }
}
