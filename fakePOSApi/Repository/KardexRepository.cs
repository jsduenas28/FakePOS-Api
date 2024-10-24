using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class KardexRepository : IKardexRepository<Kardex>
    {
        private StoreContext _context;

        public KardexRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task Add(Kardex entity)
            => await _context.Kardex.AddAsync(entity);

        public async Task<IEnumerable<Kardex>> GetByID(int id)
            => await _context.Kardex.Where(k => k.IDProducto == id).ToListAsync();

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
