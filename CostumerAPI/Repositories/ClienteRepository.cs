using AutoMapper;
using CostumerAPI.DTO;
using CostumerAPI.Models;
using CostumerAPI.Repositories.Interface;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CostumerAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public ClienteRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:SqlServer");
            _mapper = mapper;
        }

        public async Task<ClienteDTO> GetClientById(long id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"SELECT * FROM Cliente
                                WHERE ClienteId = @id";

                dbConnection.Open();

                var result = await dbConnection.QueryFirstAsync<ClienteDTO>(sQuery, id);

                return result;
            }
        }

        public async Task<bool> Create(ClienteDTO cliente)
        {
            var clienteModel = _mapper.Map<Cliente>(cliente);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"INSERT INTO Cliente(Id, Nome)
                                VALUES (@Id, @Nome)";

                dbConnection.Open();

                var result = await dbConnection.ExecuteAsync(sQuery, clienteModel);

                return result == 1 ? true : false;
            }
        }

        public async Task<bool> Update(ClienteDTO cliente)
        {
            var clienteModel = _mapper.Map<Cliente>(cliente);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"UPDATE Cliente 
                                SET Nome = @Nome 
                                WHERE Id = @Id";

                dbConnection.Open();

                var result = await dbConnection.ExecuteAsync(sQuery, clienteModel);

                return result == 1 ? true : false;
            }
        }

        public async Task<bool> DeleteClienteById(long clienteId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"DELETE FROM Cliente 
                                WHERE Id = @clienteId";
                dbConnection.Open();

                var result = await dbConnection.ExecuteAsync(sQuery, clienteId);

                return result == 1 ? true : false;
            }
        }
    }
}
