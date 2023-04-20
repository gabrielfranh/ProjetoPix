using CostumerAPI.DTO;

namespace CostumerAPI.Repositories.Interface
{
    public interface ICostumerRepository
    {
        public Task<CostumerDTO> GetCostumerById(long id);

        public Task<CostumerDTO> Create(CostumerDTO costumer);

        public Task<CostumerDTO> Update(CostumerDTO costumer);

        public Task DeleteCostumerById(long Id);
    }
}
