using fakePOSApi.DTOs;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fakePOSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private IVentaService _service;

        public VentaController(IVentaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VentaDto>>> Get()
        {
            var venta = await _service.Get();

            return Ok(venta);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<VentaDto>> GetByID(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var venta = await _service.GetByID(id);

            return Ok(venta);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VentaDto>> Add(VentaInsertDto dto)
        {
            if(!await _service.Validate(dto))
            {
                return BadRequest(_service.Message);
            }

            var venta = await _service.Add(dto);

            return CreatedAtAction(nameof (GetByID), new {id = venta.IDVenta}, venta);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<VentaDto>> Update(int id, VentaUpdateDto dto)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var venta = await _service.Update(id, dto);

            return Ok(venta);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<VentaDto>> Delete(int id)
        {
            if (!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            if(!await _service.ValidateStateContable(id))
            {
                return BadRequest(_service.Message);
            }

            var venta = await _service.Delete(id);

            return Ok(venta);
        }

        [HttpPut("anular/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AnularVenta(int id)
        {
            if (!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var venta = await _service.AnularVenta(id);

            return venta == false ? BadRequest(_service.Message) : Ok(_service.Message);
        }
    }
}
