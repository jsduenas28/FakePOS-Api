using fakePOSApi.DTOs;
using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class VentaRepository : IFacturaRepository<VentaDto, Venta>
    {
        private StoreContext _context;

        public VentaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venta>> Get()
            => await _context.Ventas.ToListAsync();

        public async Task<Venta> GetByID(int id)
            => await _context.Ventas.FindAsync(id);

        public async Task Add(Venta entity)
            => await _context.Ventas.AddAsync(entity);

        public void Update(Venta entity)
        {
            _context.Ventas.Attach(entity);
            _context.Ventas.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Venta entity)
            => _context.Ventas.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public Task<IEnumerable<Venta>> Search(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VentaDto>> GetViewFactura()
        {
            var result = await (
                    from ventas in _context.Ventas
                    join usuarios in _context.Usuarios
                        on ventas.IDUser equals usuarios.IDUser
                    join detalleventa in _context.DetalleVentas
                        on ventas.IDVenta equals detalleventa.IDVenta into detalleGroup
                        from detalle in detalleGroup.DefaultIfEmpty()
                    join productos in _context.Productos
                        on detalle.IDProducto equals productos.IDProducto into productoGroup
                        from producto in productoGroup.DefaultIfEmpty()
                    join categorias in _context.Categorias
                        on producto.IDCategoria equals categorias.IDCategoria into categoriaGroup
                        from categoria in categoriaGroup.DefaultIfEmpty()
                    select new
                    {
                        Venta = ventas,
                        Usuario = usuarios,
                        Detalle = detalle,
                        Producto = producto,
                        Categoria = categoria
                    }
                ).ToListAsync();

            var ventaDto = result.GroupBy(r => r.Venta.IDVenta).Select(g => new VentaDto
            {
                IDVenta = g.Key,
                Factura = g.First().Venta.Factura,
                Fecha = g.First().Venta.Fecha,
                MetodoPago = g.First().Venta.MetodoPago,
                TotalVenta = g.First().Venta.TotalVenta,
                IsContable = g.First().Venta.IsContable,
                Usuario = new UsuarioDto
                {
                    IDUser = g.First().Usuario.IDUser,
                    CodUser = g.First().Usuario.CodUser,
                    UserName = g.First().Usuario.UserName,
                    IsAdmin = g.First().Usuario.IsAdmin,
                    IsActive = g.First().Usuario.IsActive,
                    CreateAt = g.First().Usuario.CreateAt,
                    UpdateAt = g.First().Usuario.UpdateAt
                },
                DetalleVenta = g.Where(r => r.Detalle != null).Select(r => new DetalleVentaDto
                {
                    IDDetalleVenta = r.Detalle.IDDetalleVenta,
                    IDVenta = r.Detalle.IDVenta,
                    Cantidad = r.Detalle.Cantidad,
                    SubTotal = r.Detalle.SubTotal,
                    Producto = r.Producto != null ? new ProductoDto
                    {
                        IDProducto = r.Producto.IDProducto,
                        CodProducto = r.Producto.CodProducto,
                        Descripcion = r.Producto.Descripcion,
                        Stock = r.Producto.Stock,
                        Precio = r.Producto.Precio,
                        CreateAt = r.Producto.CreateAt,
                        UpdateAt = r.Producto.UpdateAt,
                        Categoria = r.Categoria != null ? new CategoriaDto
                        {
                            IDCategoria = r.Categoria.IDCategoria,
                            CodCategoria = r.Categoria.CodCategoria,
                            Descripcion = r.Categoria.Descripcion,
                            CreateAt = r.Categoria.CreateAt,
                            UpdateAt = r.Categoria.UpdateAt
                        } : null
                    } : null
                }).ToList()
            }).ToList();

            return ventaDto;
        }

        public async Task<VentaDto> GetViewFacturaByID(int id)
        {
            var result = await(
                    from ventas in _context.Ventas
                    join usuarios in _context.Usuarios
                        on ventas.IDUser equals usuarios.IDUser
                    join detalleventa in _context.DetalleVentas
                        on ventas.IDVenta equals detalleventa.IDVenta into detalleGroup
                    from detalle in detalleGroup.DefaultIfEmpty()
                    join productos in _context.Productos
                        on detalle.IDProducto equals productos.IDProducto into productoGroup
                    from producto in productoGroup.DefaultIfEmpty()
                    join categorias in _context.Categorias
                        on producto.IDCategoria equals categorias.IDCategoria into categoriaGroup
                    from categoria in categoriaGroup.DefaultIfEmpty()
                    where ventas.IDVenta == id
                    select new
                    {
                        Venta = ventas,
                        Usuario = usuarios,
                        Detalle = detalle,
                        Producto = producto,
                        Categoria = categoria
                    }
                ).ToListAsync();

            var ventaDto = result.GroupBy(r => r.Venta.IDVenta).Select(g => new VentaDto
            {
                IDVenta = g.Key,
                Factura = g.First().Venta.Factura,
                Fecha = g.First().Venta.Fecha,
                MetodoPago = g.First().Venta.MetodoPago,
                TotalVenta = g.First().Venta.TotalVenta,
                IsContable = g.First().Venta.IsContable,
                Usuario = new UsuarioDto
                {
                    IDUser = g.First().Usuario.IDUser,
                    CodUser = g.First().Usuario.CodUser,
                    UserName = g.First().Usuario.UserName,
                    IsAdmin = g.First().Usuario.IsAdmin,
                    IsActive = g.First().Usuario.IsActive,
                    CreateAt = g.First().Usuario.CreateAt,
                    UpdateAt = g.First().Usuario.UpdateAt
                },
                DetalleVenta = g.Where(r => r.Detalle != null).Select(r => new DetalleVentaDto
                {
                    IDDetalleVenta = r.Detalle.IDDetalleVenta,
                    IDVenta = r.Detalle.IDVenta,
                    Cantidad = r.Detalle.Cantidad,
                    SubTotal = r.Detalle.SubTotal,
                    Producto = r.Producto != null ? new ProductoDto
                    {
                        IDProducto = r.Producto.IDProducto,
                        CodProducto = r.Producto.CodProducto,
                        Descripcion = r.Producto.Descripcion,
                        Stock = r.Producto.Stock,
                        Precio = r.Producto.Precio,
                        CreateAt = r.Producto.CreateAt,
                        UpdateAt = r.Producto.UpdateAt,
                        Categoria = r.Categoria != null ? new CategoriaDto
                        {
                            IDCategoria = r.Categoria.IDCategoria,
                            CodCategoria = r.Categoria.CodCategoria,
                            Descripcion = r.Categoria.Descripcion,
                            CreateAt = r.Categoria.CreateAt,
                            UpdateAt = r.Categoria.UpdateAt
                        } : null
                    } : null
                }).ToList()
            }).FirstOrDefault();

            return ventaDto;
        }
    }
}
