using fakePOSApi.DTOs;
using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class CompraRepository : IFacturaRepository<CompraDto, Compra>
    {
        private StoreContext _context;

        public CompraRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Compra>> Get()
            => await _context.Compras.ToListAsync();

        public async Task<Compra> GetByID(int id)
            => await _context.Compras.FindAsync(id);

        public async Task Add(Compra entity)
            => await _context.Compras.AddAsync(entity);

        public void Update(Compra entity)
        {
            _context.Compras.Attach(entity);
            _context.Compras.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Compra entity)
            => _context.Compras.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public Task<IEnumerable<Compra>> Search(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompraDto>> GetViewFactura()
        {
            var result = await (
                    from compras in _context.Compras
                    join usuarios in _context.Usuarios
                        on compras.IDUser equals usuarios.IDUser
                    join detallecompra in _context.DetalleCompras
                        on compras.IDCompra equals detallecompra.IDCompra into detalleGroup
                        from detalle in detalleGroup.DefaultIfEmpty()
                    join productos in _context.Productos
                        on detalle.IDProducto equals productos.IDProducto into productoGroup
                        from producto in productoGroup.DefaultIfEmpty()
                    join categorias in _context.Categorias
                        on producto.IDCategoria equals categorias.IDCategoria into categoriaGroup
                        from categoria in categoriaGroup.DefaultIfEmpty()
                    select new
                    {
                        Compra = compras,
                        Usuario = usuarios,
                        Detalle = detalle,
                        Producto = producto,
                        Categoria = categoria
                    }
                ).ToListAsync();

            var compraDto = result.GroupBy(r => r.Compra.IDCompra).Select(g => new CompraDto
            {
                IDCompra = g.First().Compra.IDCompra,
                Factura = g.First().Compra.Factura,
                Fecha = g.First().Compra.Fecha,
                MetodoPago = g.First().Compra.MetodoPago,
                TotalCompra = g.First().Compra.TotalCompra,
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
                DetalleCompra = g.Where(r => r.Detalle != null).Select(r => new DetalleCompraDto
                {
                    IDDetalleCompra = r.Detalle.IDDetalleCompra,
                    IDCompra = r.Detalle.IDCompra,
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

            return compraDto;
        }

        public async Task<CompraDto> GetViewFacturaByID(int id)
        {
            var result = await(
                    from compras in _context.Compras
                    join usuarios in _context.Usuarios
                        on compras.IDUser equals usuarios.IDUser
                    join detallecompra in _context.DetalleCompras
                        on compras.IDCompra equals detallecompra.IDCompra into detalleGroup
                    from detalle in detalleGroup.DefaultIfEmpty()
                    join productos in _context.Productos
                        on detalle.IDProducto equals productos.IDProducto into productoGroup
                    from producto in productoGroup.DefaultIfEmpty()
                    join categorias in _context.Categorias
                        on producto.IDCategoria equals categorias.IDCategoria into categoriaGroup
                    from categoria in categoriaGroup.DefaultIfEmpty()
                    where compras.IDCompra == id
                    select new
                    {
                        Compra = compras,
                        Usuario = usuarios,
                        Detalle = detalle,
                        Producto = producto,
                        Categoria = categoria
                    }
                ).ToListAsync();

            var compraDto = result.GroupBy(r => r.Compra.IDCompra).Select(g => new CompraDto
            {
                IDCompra = g.First().Compra.IDCompra,
                Factura = g.First().Compra.Factura,
                Fecha = g.First().Compra.Fecha,
                MetodoPago = g.First().Compra.MetodoPago,
                TotalCompra = g.First().Compra.TotalCompra,
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
                DetalleCompra = g.Where(r => r.Detalle != null).Select(r => new DetalleCompraDto
                {
                    IDDetalleCompra = r.Detalle.IDDetalleCompra,
                    IDCompra = r.Detalle.IDCompra,
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

            return compraDto;
        }
    }
}
