using fakePOSApi.DTOs;
using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class DetalleVentaRepository : IDetalleRepository<DetalleVentaDto, DetalleVenta>
    {
        private StoreContext _context;

        public DetalleVentaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleVenta>> Get()
            => await _context.DetalleVentas.ToListAsync();

        public async Task<IEnumerable<DetalleVenta>> GetAllByID(int id)
            => await _context.DetalleVentas.Where(dt => dt.IDVenta == id).ToListAsync();

        public async Task<DetalleVenta> GetByID(int id)
            => await _context.DetalleVentas.FindAsync(id);

        public async Task Add(DetalleVenta entity)
            => await _context.DetalleVentas.AddAsync(entity);

        public void Update(DetalleVenta entity)
        {
            _context.DetalleVentas.Attach(entity);
            _context.DetalleVentas.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(DetalleVenta entity)
            => _context.DetalleVentas.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public Task<IEnumerable<DetalleVenta>> Search(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetalleVentaDto>> GetDetalleWithProducto()
        {
            var result = await (
                    from detalleventas in _context.DetalleVentas
                    join productos in _context.Productos
                    on detalleventas.IDProducto equals productos.IDProducto
                    join categorias in _context.Categorias
                    on productos.IDCategoria equals categorias.IDCategoria
                    select new DetalleVentaDto
                    {
                        IDDetalleVenta = detalleventas.IDDetalleVenta,
                        IDVenta = detalleventas.IDVenta,
                        Cantidad = detalleventas.Cantidad,
                        SubTotal = detalleventas.SubTotal,
                        Producto = new ProductoDto
                        {
                            IDProducto = productos.IDProducto,
                            CodProducto = productos.CodProducto,
                            Descripcion = productos.Descripcion,
                            Stock = productos.Stock,
                            Precio = productos.Precio,
                            CreateAt = productos.CreateAt,
                            UpdateAt = productos.UpdateAt,
                            Categoria = new CategoriaDto
                            {
                                IDCategoria = categorias.IDCategoria,
                                CodCategoria = categorias.CodCategoria,
                                Descripcion = categorias.Descripcion,
                                CreateAt = categorias.CreateAt,
                                UpdateAt = categorias.UpdateAt
                            }
                        }
                    }
                ).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<DetalleVentaDto>> GetDetalleWithProductoByID(int id)
        {
            var result = await(
                    from detalleventas in _context.DetalleVentas
                    join productos in _context.Productos
                    on detalleventas.IDProducto equals productos.IDProducto
                    join categorias in _context.Categorias
                    on productos.IDCategoria equals categorias.IDCategoria
                    where detalleventas.IDVenta == id
                    select new DetalleVentaDto
                    {
                        IDDetalleVenta = detalleventas.IDDetalleVenta,
                        IDVenta = detalleventas.IDVenta,
                        Cantidad = detalleventas.Cantidad,
                        SubTotal = detalleventas.SubTotal,
                        Producto = new ProductoDto
                        {
                            IDProducto = productos.IDProducto,
                            CodProducto = productos.CodProducto,
                            Descripcion = productos.Descripcion,
                            Stock = productos.Stock,
                            Precio = productos.Precio,
                            CreateAt = productos.CreateAt,
                            UpdateAt = productos.UpdateAt,
                            Categoria = new CategoriaDto
                            {
                                IDCategoria = categorias.IDCategoria,
                                CodCategoria = categorias.CodCategoria,
                                Descripcion = categorias.Descripcion,
                                CreateAt = categorias.CreateAt,
                                UpdateAt = categorias.UpdateAt
                            }
                        }
                    }
                ).ToListAsync();

            return result;
        }
    }
}
