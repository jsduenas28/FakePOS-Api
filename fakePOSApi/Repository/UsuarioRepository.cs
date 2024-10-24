using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository<Usuario>
    {
        private StoreContext _context;

        public UsuarioRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> Get()
            => await _context.Usuarios.ToListAsync();

        public async Task<Usuario> GetByID(int id)
            => await _context.Usuarios.FindAsync(id);

        public async Task<IEnumerable<Usuario>> Search(string codUser)
            => await _context.Usuarios.Where(u => u.CodUser.Contains(codUser)).ToListAsync();

        public void Update(Usuario entity)
        {
            _context.Usuarios.Attach(entity);
            _context.Usuarios.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
