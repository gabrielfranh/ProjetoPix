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

                var result = await dbConnection.QueryFirstOrDefaultAsync<Cliente>(sQuery, new { id });

                return _mapper.Map<ClienteDTO>(result);
            }
        }

        public async Task<ClienteDTO> Create(ClienteDTO cliente)
        {
            var clienteModel = _mapper.Map<Cliente>(cliente);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"INSERT INTO Cliente(Nome)
                                VALUES (@Nome); SELECT CAST(scope_identity() AS INT)";

                dbConnection.Open();

                var clienteId = await dbConnection.ExecuteScalarAsync<int>(sQuery, clienteModel);

                clienteModel.ClienteId = clienteId;

                return _mapper.Map<ClienteDTO>(clienteModel);
            }
        }

        public async Task<ClienteDTO> Update(ClienteDTO cliente)
        {
            var clienteModel = _mapper.Map<Cliente>(cliente);

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"UPDATE Cliente 
                                SET Nome = @Nome 
                                WHERE ClienteId = @ClienteId";

                dbConnection.Open();

                await dbConnection.ExecuteAsync(sQuery, clienteModel);

                return _mapper.Map<ClienteDTO>(clienteModel);
            }
        }

        public async Task DeleteClienteById(long clienteId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sQuery = @"DELETE FROM Cliente 
                                WHERE ClienteId = @clienteId";
                dbConnection.Open();

                var result = await dbConnection.ExecuteAsync(sQuery, new { clienteId });
            }
        }
    }
}
