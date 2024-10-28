using fakePOSApi.DTOs;
using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class CompraRepository : IRepository<Compra>
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
    }
}
