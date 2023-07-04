using LoginAPI.DTO;
using LoginAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var loginSuccesful = await _loginService.ValidateLogin(login);

            if (loginSuccesful)
                return Ok(_loginService.GenerateAuthToken(login));

            return BadRequest();
        }
    }
}
