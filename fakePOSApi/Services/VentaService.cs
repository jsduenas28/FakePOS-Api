using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;

namespace fakePOSApi.Services
{
    public class VentaService : IVentaService
    {
        private IFacturaRepository<VentaDto, Venta> _ventaRepository;
        private IDetalleRepository<DetalleVenta> _detalleVentaRepository;
        private IProductoRepository<Producto> _productoRepository;
        private IKardexRepository<Kardex> _kardexRepository;
        private IUsuarioRepository<Usuario> _usuarioRepository;
        private IHttpContextAccessor _httpContext;
        public List<string> Message { get; private set; } = new List<string>();

        public VentaService([FromKeyedServices("ventaRepository")] IFacturaRepository<VentaDto, Venta> ventaRepository, 
                            [FromKeyedServices("detalleVentaRepository")] IDetalleRepository<DetalleVenta> detalleVentaRepository, 
                            IProductoRepository<Producto> productoRepository,
                            IKardexRepository<Kardex> kardexRepository,
                            IUsuarioRepository<Usuario> usuarioRepository,
                            IHttpContextAccessor httpContext)
        {
            _ventaRepository = ventaRepository;
            _detalleVentaRepository = detalleVentaRepository;
            _productoRepository = productoRepository;
            _kardexRepository = kardexRepository;
            _usuarioRepository = usuarioRepository;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<VentaDto>> Get()
        {
            var venta = await _ventaRepository.GetViewFactura();
            return venta;
        }

        public async Task<VentaDto> GetByID(int id)
        {
            var venta = await _ventaRepository.GetViewFacturaByID(id);
            return venta;
        }

        public async Task<VentaDto> Add(VentaInsertDto dto)
        {
            var user = _httpContext.HttpContext.User;
            var idUser = user.FindFirst("IDUser")?.Value;

            double auxTotal = 0;

            var venta = new Venta()
            {
                Factura = dto.Factura,
                Fecha = dto.Fecha,
                MetodoPago = dto.MetodoPago,
                TotalVenta = 0,
                IsContable = true,
                IDUser = int.Parse(idUser)
            };

            await _ventaRepository.Add(venta);
            await _ventaRepository.Save();

            foreach(var dt in dto.DetalleVenta)
            {
                var producto = await _productoRepository.GetByID(dt.IDProducto);

                var detalleVenta = new DetalleVenta()
                {
                    IDVenta = venta.IDVenta,
                    IDProducto = producto.IDProducto,
                    Cantidad = dt.Cantidad,
                    SubTotal = dt.Cantidad * producto.Precio
                };

                var kardex = new Kardex()
                {
                    IDProducto = producto.IDProducto,
                    NumDocumento = venta.Factura,
                    TipoMovimiento = "Venta",
                    Entrada = 0,
                    Salida = detalleVenta.Cantidad,
                    Fecha = venta.Fecha,
                    IDUser = venta.IDUser
                };

                await _detalleVentaRepository.Add(detalleVenta);
                await _kardexRepository.Add(kardex);
                await _productoRepository.ChangeStock(detalleVenta.IDProducto, detalleVenta.Cantidad, true);
                auxTotal += detalleVenta.SubTotal;
            }

            venta.TotalVenta = auxTotal;
            await _ventaRepository.Save();
            await _detalleVentaRepository.Save();
            await _kardexRepository.Save();

            var ventaDto = await _ventaRepository.GetViewFacturaByID(venta.IDVenta);
            return ventaDto;
        }

        public async Task<VentaDto> Update(int id, VentaUpdateDto dto)
        {
            var venta = await _ventaRepository.GetByID(id);

            venta.Factura = dto.Factura;
            venta.Fecha = dto.Fecha;
            venta.MetodoPago = dto.MetodoPago;

            _ventaRepository.Update(venta);
            await _ventaRepository.Save();

            var ventaDto = await _ventaRepository.GetViewFacturaByID(venta.IDVenta);
            return ventaDto;
        }

        public async Task<VentaDto> Delete(int id)
        {
            var venta = await _ventaRepository.GetByID(id);
            var ventaDto = await _ventaRepository.GetViewFacturaByID(venta.IDVenta);

            _ventaRepository.Delete(venta);
            await _ventaRepository.Save();

            return ventaDto;
        }

        public async Task<bool> AnularVenta(int id)
        {
            var venta = await _ventaRepository.GetByID(id);
            var detalleVenta = await _detalleVentaRepository.GetAllByID(id);

            var user = _httpContext.HttpContext.User;
            var idUser = user.FindFirst("IDUser")?.Value;
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            if(!venta.IsContable)
            {
                Message.Add("La venta ya fue anulada. No se puede anular dos veces.");
                return false;
            }

            foreach(var dt in detalleVenta)
            {
                var producto = await _productoRepository.GetByID(dt.IDProducto);
                await _productoRepository.ChangeStock(dt.IDProducto, dt.Cantidad, false);

                var kardex = new Kardex()
                {
                    IDProducto = producto.IDProducto,
                    NumDocumento = venta.Factura,
                    TipoMovimiento = "Anulacion de Venta",
                    Entrada = dt.Cantidad,
                    Salida = 0,
                    Fecha = today,
                    IDUser = int.Parse(idUser)
                };

                await _kardexRepository.Add(kardex);
            }

            venta.IsContable = false;
            _ventaRepository.Update(venta);
            await _ventaRepository.Save();
            await _kardexRepository.Save();
            Message.Add("Venta anulada con exito");
            return true;
        }

        public async Task<bool> Validate(int id)
        {
            var venta = await _ventaRepository.GetByID(id);

            if(venta == null)
            {
                Message.Add("EL IDVenta no existe.");
                return false;
            }

            return true;
        }

        public async Task<bool> Validate(VentaInsertDto dto)
        {
            foreach(var dt in dto.DetalleVenta)
            {
                var producto = await _productoRepository.GetByID(dt.IDProducto);
                var validateStock = await _productoRepository.ValidateStock(dt.IDProducto, dt.Cantidad);

                if(producto == null)
                {
                    Message.Add("El IDProducto no existe");
                    return false;
                }

                if(!validateStock)
                {
                    Message.Add("El CodProducto: " + producto.CodProducto +" no tiene suficiente Stock para realizar la venta.");
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> ValidateStateContable(int id)
        {
            var venta = await _ventaRepository.GetByID(id);

            if(venta.IsContable)
            {
                Message.Add("No es posible eliminar una venta que no haya sido anulada antes.");
                return false;
            }

            return true;
        }

        public Task<bool> Validate(VentaUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VentaDto>> Search(string search)
        {
            throw new NotImplementedException();
        }
    }
}
