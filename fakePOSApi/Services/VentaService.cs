using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;

namespace fakePOSApi.Services
{
    public class VentaService : IVentaService
    {
        private IRepository<Venta> _ventaRepository;
        private IDetalleRepository<DetalleVentaDto, DetalleVenta> _detalleVentaRepository;
        private IProductoRepository<Producto> _productoRepository;
        private IKardexRepository<Kardex> _kardexRepository;
        private IUsuarioRepository<Usuario> _usuarioRepository;
        private IHttpContextAccessor _httpContext;
        public List<string> Message { get; private set; } = new List<string>();

        public VentaService([FromKeyedServices("ventaRepository")] IRepository<Venta> ventaRepository, 
                            [FromKeyedServices("detalleVentaRepository")] IDetalleRepository<DetalleVentaDto, DetalleVenta> detalleVentaRepository, 
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
            var venta = await _ventaRepository.Get();
            var usuario = await _usuarioRepository.Get();
            var detalleVenta = await _detalleVentaRepository.GetDetalleWithProducto();

            return venta.Select(v => new VentaDto
            {
                IDVenta = v.IDVenta,
                Factura = v.Factura,
                Fecha = v.Fecha,
                MetodoPago = v.MetodoPago,
                TotalVenta = v.TotalVenta,
                IsContable = v.IsContable,
                Usuario = usuario.Where(u => u.IDUser == v.IDUser).Select(u => new UsuarioDto
                {
                    IDUser = u.IDUser,
                    CodUser = u.CodUser,
                    UserName = u.UserName,
                    IsAdmin = u.IsAdmin,
                    IsActive = u.IsActive,
                    CreateAt = u.CreateAt,
                    UpdateAt = u.UpdateAt
                }).FirstOrDefault(),
                DetalleVenta = detalleVenta.ToList()
            });
        }

        public async Task<VentaDto> GetByID(int id)
        {
            var venta = await _ventaRepository.GetByID(id);
            var usuario = await _usuarioRepository.GetByID(venta.IDUser);
            var detalleVenta = await _detalleVentaRepository.GetDetalleWithProductoByID(id);

            return new VentaDto
            {
                IDVenta = venta.IDVenta,
                Factura = venta.Factura,
                Fecha = venta.Fecha,
                MetodoPago = venta.MetodoPago,
                TotalVenta = venta.TotalVenta,
                IsContable = venta.IsContable,
                Usuario = new UsuarioDto
                {
                    IDUser = usuario.IDUser,
                    CodUser = usuario.CodUser,
                    UserName = usuario.UserName,
                    IsAdmin = usuario.IsAdmin,
                    IsActive = usuario.IsActive,
                    CreateAt = usuario.CreateAt,
                    UpdateAt = usuario.UpdateAt
                },
                DetalleVenta = detalleVenta.ToList()
            };
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

            var usuario = await _usuarioRepository.GetByID(venta.IDUser);
            var detalleVentDto = await _detalleVentaRepository.GetDetalleWithProductoByID(venta.IDVenta);

            return new VentaDto
            {
                IDVenta = venta.IDVenta,
                Factura = venta.Factura,
                Fecha = venta.Fecha,
                MetodoPago = venta.MetodoPago,
                TotalVenta = venta.TotalVenta,
                IsContable = venta.IsContable,
                Usuario = new UsuarioDto
                {
                    IDUser = usuario.IDUser,
                    CodUser = usuario.CodUser,
                    UserName = usuario.UserName,
                    IsAdmin = usuario.IsAdmin,
                    IsActive = usuario.IsActive,
                    CreateAt = usuario.CreateAt,
                    UpdateAt = usuario.UpdateAt
                },
                DetalleVenta = detalleVentDto.ToList()
            };
        }

        public async Task<VentaDto> Update(int id, VentaUpdateDto dto)
        {
            var venta = await _ventaRepository.GetByID(id);

            venta.Factura = dto.Factura;
            venta.Fecha = dto.Fecha;
            venta.MetodoPago = dto.MetodoPago;

            _ventaRepository.Update(venta);
            await _ventaRepository.Save();

            var usuario = await _usuarioRepository.GetByID(venta.IDUser);
            var detalleVentDto = await _detalleVentaRepository.GetDetalleWithProductoByID(venta.IDVenta);

            return new VentaDto
            {
                IDVenta = venta.IDVenta,
                Factura = venta.Factura,
                Fecha = venta.Fecha,
                MetodoPago = venta.MetodoPago,
                TotalVenta = venta.TotalVenta,
                IsContable = venta.IsContable,
                Usuario = new UsuarioDto
                {
                    IDUser = usuario.IDUser,
                    CodUser = usuario.CodUser,
                    UserName = usuario.UserName,
                    IsAdmin = usuario.IsAdmin,
                    IsActive = usuario.IsActive,
                    CreateAt = usuario.CreateAt,
                    UpdateAt = usuario.UpdateAt
                },
                DetalleVenta = detalleVentDto.ToList()
            };
        }

        public async Task<VentaDto> Delete(int id)
        {
            var venta = await _ventaRepository.GetByID(id);
            var usuario = await _usuarioRepository.GetByID(venta.IDUser);
            var detalleVentDto = await _detalleVentaRepository.GetDetalleWithProductoByID(venta.IDVenta);

            var ventaDto = new VentaDto
            {
                IDVenta = venta.IDVenta,
                Factura = venta.Factura,
                Fecha = venta.Fecha,
                MetodoPago = venta.MetodoPago,
                TotalVenta = venta.TotalVenta,
                IsContable = venta.IsContable,
                Usuario = new UsuarioDto
                {
                    IDUser = usuario.IDUser,
                    CodUser = usuario.CodUser,
                    UserName = usuario.UserName,
                    IsAdmin = usuario.IsAdmin,
                    IsActive = usuario.IsActive,
                    CreateAt = usuario.CreateAt,
                    UpdateAt = usuario.UpdateAt
                },
                DetalleVenta = detalleVentDto.ToList()
            };

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
