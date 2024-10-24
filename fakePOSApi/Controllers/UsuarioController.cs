using fakePOSApi.DTOs;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fakePOSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService<UsuarioDto, UsuarioUpdateDto, UsuarioChangePasswordDto> _service;

        public UsuarioController(IUsuarioService<UsuarioDto, UsuarioUpdateDto, UsuarioChangePasswordDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> Get()
        {
            var usuarios = await _service.Get();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDto>> GetByID(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var usuario = await _service.GetByID(id);

            return Ok(usuario);
        }

        [HttpGet("search/{codUser}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> Search(string codUser)
        {
            var usuarios = await _service.Search(codUser);

            return Ok(usuarios);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDto>> Update(int id, UsuarioUpdateDto dto)
        {
            if (!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var usuario = await _service.Update(id, dto);
            return Ok(usuario);
        }

        [HttpPut("changeIsActive/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> ChangeIsActive(int id)
        {
            if (!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            await _service.ChangeIsActive(id);

            return Ok(_service.Message);
        }

        [HttpPut("changeIsAdmin/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> changeIsAdmin(int id)
        {
            if (!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            await _service.ChangeIsAdmin(id);

            return Ok(_service.Message);
        }

        [HttpPut("changePassword/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> changePassword(int id, UsuarioChangePasswordDto dto)
        {
            if (!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            if (!await _service.Validate(id, dto))
            {
                return BadRequest(_service.Message);
            }

            await _service.ChangePassword(id, dto);

            return Ok(_service.Message);
        }
    }
}
