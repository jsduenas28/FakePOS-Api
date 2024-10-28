using fakePOSApi.DTOs;
using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class VentaRepository : IRepository<Venta>
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
    }
}
