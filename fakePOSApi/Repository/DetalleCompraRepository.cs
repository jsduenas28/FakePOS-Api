using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class DetalleCompraRepository : IDetalleRepository<DetalleCompra>
    {
        private StoreContext _context;

        public DetalleCompraRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleCompra>> Get()
            => await _context.DetalleCompras.ToListAsync();

        public async Task<IEnumerable<DetalleCompra>> GetAllByID(int id)
            => await _context.DetalleCompras.Where(dt => dt.IDCompra == id).ToListAsync();

        public async Task<DetalleCompra> GetByID(int id)
            => await _context.DetalleCompras.FindAsync(id);

        public async Task Add(DetalleCompra entity)
            => await _context.DetalleCompras.AddAsync(entity);

        public void Update(DetalleCompra entity)
        {
            _context.DetalleCompras.Attach(entity);
            _context.DetalleCompras.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(DetalleCompra entity)
            => _context.DetalleCompras.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public Task<IEnumerable<DetalleCompra>> Search(string search)
        {
            throw new NotImplementedException();
        }
    }
}
