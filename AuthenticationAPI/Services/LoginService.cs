using LoginAPI.DTO;
using LoginAPI.Repositories.Interfaces;
using LoginAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQService.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly int _iterator = 3;
        private readonly string _pepper;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            _pepper = Environment.GetEnvironmentVariable("Pepper234#$");
        }

        public async Task CreateCostumer(string message)                                                                                    
        {
            var costumerDTO = JsonConvert.DeserializeObject<CostumerDTO>(message);

            await _loginRepository.CreateCostumer(costumerDTO);
        }

        public async Task<bool> ValidateLogin(LoginDTO login)
        {
            var user = await _loginRepository.GetCostumerByLogin(login);

            if (user is null)
                return false;

            var passHash = PasswordHasher.ComputeHash(login.Password, user.Salt, _pepper, _iterator);
            var costumer = _loginRepository.GetCostumerByLogin(login);

            if (costumer == null) return false;

            return true;
        }

        public string GenerateAuthToken(LoginDTO login)
        {
            var secretKey = JwtExxtensions.SecurityKey;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Name, login.Password),
                    new Claim(ClaimTypes.Role, login.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
