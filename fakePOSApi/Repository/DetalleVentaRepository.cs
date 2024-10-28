using fakePOSApi.DTOs;
using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class DetalleVentaRepository : IDetalleRepository<DetalleVenta>
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
    }
}
