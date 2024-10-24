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

        public async Task<IEnumerable<Producto>> Get()
            => await _context.Productos.ToListAsync();

        public async Task<Producto> GetByID(int id)
            => await _context.Productos.FindAsync(id);

        public async Task<IEnumerable<Producto>> Search(string search)
            => await _context.Productos.Where(p => p.CodProducto.Contains(search)).ToListAsync();

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
    }
}
