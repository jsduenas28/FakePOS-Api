using fakePOSApi.DTOs;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fakePOSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private IService<ProductoDto, ProductoInsertDto, ProductoUpdateDto> _service;

        public ProductoController([FromKeyedServices("productoService")] IService<ProductoDto, ProductoInsertDto, ProductoUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            var productos = await _service.Get();

            return Ok(productos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductoDto>> GetByID(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var producto = await _service.GetByID(id);

            return Ok(producto);
        }

        [HttpGet("search/{search}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Search(string search)
        {
            var productos = await _service.Search(search);

            return Ok(productos);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ProductoDto>> Add(ProductoInsertDto dto)
        {
            if(!await _service.Validate(dto))
            {
                return BadRequest(_service.Message);
            }

            var producto = await _service.Add(dto);

            return CreatedAtAction(nameof (GetByID), new {id = producto.IDProducto}, producto);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ProductoDto>> Update(int id, ProductoUpdateDto dto)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            if(!await _service.Validate(dto))
            {
                return BadRequest(_service.Message);
            }

            var producto = await _service.Update(id, dto);

            return Ok(producto);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ProductoDto>> Delete(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var producto = await _service.Delete(id);
            return Ok(producto);
        }
    }
}
