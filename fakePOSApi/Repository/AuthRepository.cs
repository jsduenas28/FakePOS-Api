using fakePOSApi.Models;
using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Repository
{
    public class AuthRepository : IAuthRepository<Usuario>
    {
        private StoreContext _context;

        public AuthRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Login(string CodUser)
            => await _context.Usuarios.FirstOrDefaultAsync(u => u.CodUser == CodUser);

        public async Task Register(Usuario entity)
            => await _context.Usuarios.AddAsync(entity);

        public async Task<bool> UserExist(string CodUser)
            => await _context.Usuarios.AnyAsync(u => u.CodUser == CodUser);

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
