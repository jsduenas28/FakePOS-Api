using fakePOSApi.DTOs;
using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class ProductoRepository : IProductoRepository<Producto>
    {
        private StoreContext _context;

        public ProductoRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Producto> GetByID(int id)
            => await _context.Productos.FindAsync(id);

        public async Task<Producto> GetByCodProducto(string CodProducto)
            => await _context.Productos.Where(p => p.CodProducto == CodProducto).FirstOrDefaultAsync();

        public async Task Add(Producto entity)
            => await _context.Productos.AddAsync(entity);

        public void Update(Producto entity)
        {
            _context.Productos.Attach(entity);
            _context.Productos.Entry(entity);
        }

        public void Delete(Producto entity)
            => _context.Productos.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public async Task<bool> ValidateStock(int idProducto, int cantidad)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IDProducto == idProducto && p.Stock >= cantidad);

            if(producto == null)
            {
                return false;
            }

            return true;
        }

        public async Task ChangeStock(int idProducto, int cantidad, bool isDecrement)
        {
            if(isDecrement)
            {
                var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IDProducto == idProducto && p.Stock >= cantidad);
                producto.Stock -= cantidad;
            } else
            {
                var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IDProducto == idProducto);
                producto.Stock += cantidad;
            }
        }

        public async Task<IEnumerable<ProductoDto>> GetProductosConCategoria()
        {
            var result = await (
                    from productos in _context.Productos
                    join categorias in _context.Categorias
                    on productos.IDCategoria equals categorias.IDCategoria
                    select new ProductoDto
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
                ).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ProductoDto>> GetProductosConCategoriaByCodProducto(string CodProducto)
        {
            var result = await(
                    from productos in _context.Productos
                    join categorias in _context.Categorias
                    on productos.IDCategoria equals categorias.IDCategoria
                    where productos.CodProducto.Contains(CodProducto)
                    select new ProductoDto
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
                ).ToListAsync();

            return result;
        }

        public async Task<ProductoDto> GetProductoConCategoriaByID(int id)
        {
            var result = await(
                    from producto in _context.Productos
                    join categoria in _context.Categorias
                    on producto.IDCategoria equals categoria.IDCategoria
                    where producto.IDCategoria == id
                    select new ProductoDto
                    {
                        IDProducto = producto.IDProducto,
                        CodProducto = producto.CodProducto,
                        Descripcion = producto.Descripcion,
                        Stock = producto.Stock,
                        Precio = producto.Precio,
                        CreateAt = producto.CreateAt,
                        UpdateAt = producto.UpdateAt,
                        Categoria = new CategoriaDto
                        {
                            IDCategoria = categoria.IDCategoria,
                            CodCategoria = categoria.CodCategoria,
                            Descripcion = categoria.Descripcion,
                            CreateAt = categoria.CreateAt,
                            UpdateAt = categoria.UpdateAt
                        }
                    }
                ).FirstOrDefaultAsync();

            return result;
        }

        public Task<IEnumerable<Producto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Producto>> Search(string search)
        {
            throw new NotImplementedException();
        }
    }
}
