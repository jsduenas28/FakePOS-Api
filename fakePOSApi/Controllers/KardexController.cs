using fakePOSApi.DTOs;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fakePOSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KardexController : ControllerBase
    {
        private IKardexService<KardexListDto> _service;

        public KardexController(IKardexService<KardexListDto> service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<KardexListDto>> GetByID(int id)
        {
            if(!await _service.Validate(id))
            {
                return NotFound(_service.Message);
            }

            var kardex = await _service.GetByID(id);
            return Ok(kardex);
        }

        [HttpGet("search/{CodProducto}")]
        [Authorize]
        public async Task<ActionResult<KardexListDto>> GetByCodProducto(string CodProducto)
        {
            if (!await _service.Validate(CodProducto))
            {
                return NotFound(_service.Message);
            }

            var kardex = await _service.GetByCodProducto(CodProducto);
            return Ok(kardex);
        }
    }
}
