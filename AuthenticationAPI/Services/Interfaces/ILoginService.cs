using LoginAPI.DTO;

namespace LoginAPI.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<bool> ValidateLogin(LoginDTO login);
        public string GenerateAuthToken(LoginDTO login);
        public Task CreateCostumer(string message);
    }
}
