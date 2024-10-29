using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;

namespace fakePOSApi.Services
{
    public class CompraService : IService<CompraDto, CompraInsertDto, CompraUpdateDto>
    {
        private IFacturaRepository<CompraDto, Compra> _compraRepository;
        private IDetalleRepository<DetalleCompra> _detalleCompraRepository;
        private IProductoRepository<Producto> _productoRepository;
        private IKardexRepository<Kardex> _kardexRepository;
        private IHttpContextAccessor _httpContext;
        public List<string> Message { get; private set; } = new List<string>();

        public CompraService([FromKeyedServices("compraRepository")] IFacturaRepository<CompraDto, Compra> compraRepository, 
                             [FromKeyedServices("detalleCompraRepository")] IDetalleRepository<DetalleCompra> detalleCompraRepository, 
                             IProductoRepository<Producto> productoRepository, 
                             IKardexRepository<Kardex> kardexRepository,
                             IHttpContextAccessor httpContext)
        {
            _compraRepository = compraRepository;
            _detalleCompraRepository = detalleCompraRepository;
            _productoRepository = productoRepository;
            _kardexRepository = kardexRepository;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<CompraDto>> Get()
        {
            var compra = await _compraRepository.GetViewFactura();
            return compra;
        }

        public async Task<CompraDto> GetByID(int id)
        {
            var compra = await _compraRepository.GetViewFacturaByID(id);
            return compra;
        }

        public async Task<CompraDto> Add(CompraInsertDto dto)
        {
            var user = _httpContext.HttpContext.User;
            var idUser = user.FindFirst("IDUser")?.Value;
            double auxTotal = 0;

            var compra = new Compra()
            {
                Factura = dto.Factura,
                Fecha = dto.Fecha,
                MetodoPago = dto.MetodoPago,
                TotalCompra = 0,
                IDUser = int.Parse(idUser)
            };

            await _compraRepository.Add(compra);
            await _compraRepository.Save();

            foreach (var dt in dto.DetalleCompra)
            {
                var producto = await _productoRepository.GetByID(dt.IDProducto);

                var detalleCompra = new DetalleCompra()
                {
                    IDCompra = compra.IDCompra,
                    IDProducto = producto.IDProducto,
                    Cantidad = dt.Cantidad,
                    SubTotal = dt.Cantidad * producto.Precio
                };

                var kardex = new Kardex()
                {
                    IDProducto = producto.IDProducto,
                    NumDocumento = compra.Factura,
                    TipoMovimiento = "Compra",
                    Entrada = detalleCompra.Cantidad,
                    Salida = 0,
                    Fecha = compra.Fecha,
                    IDUser = compra.IDUser
                };

                await _kardexRepository.Add(kardex);
                await _detalleCompraRepository.Add(detalleCompra);
                await _productoRepository.ChangeStock(detalleCompra.IDProducto, detalleCompra.Cantidad, false);
                auxTotal += detalleCompra.SubTotal;
            }

            compra.TotalCompra = auxTotal;
            await _compraRepository.Save();
            await _detalleCompraRepository.Save();
            await _kardexRepository.Save();

            var compraDto = await _compraRepository.GetViewFacturaByID(compra.IDCompra);
            return compraDto;
        }

        public async Task<CompraDto> Update(int id, CompraUpdateDto dto)
        {
            var compra = await _compraRepository.GetByID(id);

            compra.Factura = dto.Factura;
            compra.Fecha = dto.Fecha;
            compra.MetodoPago = dto.MetodoPago;

            _compraRepository.Update(compra);
            await _compraRepository.Save();

            var compraDto = await _compraRepository.GetViewFacturaByID(compra.IDCompra);
            return compraDto;
        }

        public async Task<CompraDto> Delete(int id)
        {
            var compra = await _compraRepository.GetByID(id);

            var compraDto = await _compraRepository.GetViewFacturaByID(compra.IDCompra);

            _compraRepository.Delete(compra);
            await _compraRepository.Save();

            return compraDto;
        }

        public Task<IEnumerable<CompraDto>> Search(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Validate(int id)
        {
            var compra = await _compraRepository.GetByID(id);

            if(compra == null)
            {
                Message.Add("El IDCompra no existe.");
                return false;
            }

            return true;
        }

        public async Task<bool> Validate(CompraInsertDto dto)
        {
            foreach(var dt in dto.DetalleCompra)
            {
                var producto = await _productoRepository.GetByID(dt.IDProducto);

                if(producto == null)
                {
                    Message.Add("El IDProducto no existe");
                    return false;
                }
            }

            return true;
        }

        public Task<bool> Validate(CompraUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
