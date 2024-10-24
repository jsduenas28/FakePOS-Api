using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fakePOSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService<UserRegisterDto, UserLoginDto, Usuario> _authService;

        public AuthController(IAuthService<UserRegisterDto, UserLoginDto, Usuario> authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var user = await _authService.Register(dto);

            return user == false ? BadRequest(_authService.Message) : Ok(_authService.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            if(!await _authService.Validate(dto))
            {
                return BadRequest(_authService.Message);
            }

            var token = await _authService.Login(dto);
            return Ok(new {token});
        }
    }
}
