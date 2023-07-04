using CostumerAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CostumerAPI.Services.Interfaces
{
    public interface ICostumerService
    {
        public Task<CostumerDTO> GetCostumerById(int id);
        public Task<CostumerDTO> Create(CostumerDTO costumer);
        public Task<CostumerDTO> Update(CostumerDTO costumer);
        public Task DeleteCostumerById(int id);
    }
}
