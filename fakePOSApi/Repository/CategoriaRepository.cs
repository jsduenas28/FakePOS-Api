using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class CategoriaRepository : IRepository<Categoria>
    {
        private StoreContext _context;

        public CategoriaRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> Get()
            => await _context.Categorias.ToListAsync();

        public async Task<Categoria> GetByID(int id)
            => await _context.Categorias.FindAsync(id);

        public async Task<IEnumerable<Categoria>> Search(string search)
            => await _context.Categorias.Where(c => c.CodCategoria.Contains(search)).ToListAsync();

        public async Task Add(Categoria entity)
            => await _context.Categorias.AddAsync(entity);

        public void Update(Categoria entity)
        {
            _context.Categorias.Attach(entity);
            _context.Categorias.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Categoria entity)
            => _context.Categorias.Remove(entity);

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
