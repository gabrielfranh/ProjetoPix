using LoginAPI.DTO;
using LoginAPI.Models;
using LoginAPI.Repositories.Interfaces;
using AutoMapper;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace LoginAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public LoginRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SqlServer");
            _mapper = mapper;
        }

        public async Task<CostumerDTO> GetCostumerByLogin(LoginDTO login)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"SELECT * FROM Costumer
                                WHERE User = @user AND Password = @Password AND Role = @Role";

                dbConnection.Open();

                var result = await dbConnection.QueryFirstOrDefaultAsync<Costumer>(sQuery, new { login.Username, login.Password, login.Role });

                return _mapper.Map<CostumerDTO>(result);
            }
        }

        public async Task CreateCostumer(CostumerDTO costumer)
        {
            var costumerModel = _mapper.Map<Costumer>(costumer);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"INSERT INTO Costumer(Name, PasswordHash, Username, Role, Salt)
                                VALUES (@Name, @PasswordHash, @Username, @Role, @Salt); SELECT CAST(scope_identity() AS INT)";

                dbConnection.Open();

                var Id = await dbConnection.ExecuteScalarAsync<int>(sQuery, costumerModel);
            }
        }
    }
}
