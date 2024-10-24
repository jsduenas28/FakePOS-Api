using fakePOSApi.DTOs;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fakePOSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private IService<CompraDto, CompraInsertDto, CompraUpdateDto> _service;

        public CompraController([FromKeyedServices("compraService")] IService<CompraDto, CompraInsertDto, CompraUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CompraDto>>> Get()
        {
            var compra = await _service.Get();

            return Ok(compra);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CompraDto>> GetByID(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var compra = await _service.GetByID(id);
            return Ok(compra);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CompraDto>> Add(CompraInsertDto dto)
        {
            if(!await _service.Validate(dto))
            {
                return BadRequest(_service.Message);
            }

            var compra = await _service.Add(dto);
            return CreatedAtAction(nameof(GetByID), new { id = compra.IDCompra }, compra);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CompraDto>> Update(int id, CompraUpdateDto dto)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var compra = await _service.Update(id, dto);
            return Ok(compra);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var compra = await _service.Delete(id);
            return Ok(compra);
        }
    }
}
