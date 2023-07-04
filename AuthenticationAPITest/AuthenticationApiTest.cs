using AuthenticationAPI.Controllers;
using AuthenticationAPI.DTO;
using AuthenticationAPI.Repositories.Interfaces;
using AuthenticationAPI.Services.Interfaces;
using CostumerAPI.Services;
using Moq;

namespace AuthenticationAPITest
{
    public class AuthenticationApiTest
    {
        private LoginService _service;
        private Mock<ILoginRepository> _repository;

        public AuthenticationApiTest()
        {
            _repository = new Mock<ILoginRepository>();
            _service = new LoginService(_repository.Object);
        }

        [Fact]
        public void TestAuthTokenGeneration()
        {
            var login =  new LoginDTO()
            {
                User = "gabriel",
                Password = "password",
                Role = "role"
            };

            var token = _service.GenerateAuthToken(login);

            Assert.NotEmpty(token);
        }
    }
}