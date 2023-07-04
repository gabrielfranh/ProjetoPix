using LoginAPI.DTO;

namespace LoginAPI.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        public Task<CostumerDTO> GetCostumerByLogin(LoginDTO login);
        public Task CreateCostumer(CostumerDTO costumer);
    }
}
