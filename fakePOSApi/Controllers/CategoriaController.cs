using fakePOSApi.DTOs;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fakePOSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private IService<CategoriaDto, CategoriaInsertDto, CategoriaUpdateDto> _service;

        public CategoriaController([FromKeyedServices("categoriaService")] IService<CategoriaDto, CategoriaInsertDto, CategoriaUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
        {
            var categorias = await _service.Get();

            return Ok(categorias);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoriaDto>> GetByID(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var categoria = await _service.GetByID(id);

            return Ok(categoria);
        }

        [HttpGet("search/{search}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> Search(string search)
        {
            var categorias = await _service.Search(search);

            return Ok(categorias);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CategoriaDto>> Add(CategoriaInsertDto dto)
        {
            var categoria = await _service.Add(dto);

            return CreatedAtAction(nameof (GetByID), new {id = categoria.IDCategoria}, categoria);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CategoriaDto>> Update(int id, CategoriaUpdateDto dto)
        {
            if (!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var categoria = await _service.Update(id, dto);
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CategoriaDto>> Delete(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var categoria = await _service.Delete(id);
            return Ok(categoria);
        }
    }
}
