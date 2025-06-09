using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HelperService _helperService;
        public AuthController(HelperService service)
        {
            _helperService = service;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.UserName == "admin" && model.Password == "password")
            {
                var token = _helperService.GenerateJwtToken(new LoginModel { UserName = model.UserName });
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

    }
}
