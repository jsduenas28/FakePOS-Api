using fakePOSApi.DTOs;
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

        public async Task<IEnumerable<KardexDto>> GetKardexWithUsuario(int id)
        {
            var result = await(
                    from kardex in _context.Kardex
                    join usuario in _context.Usuarios
                    on kardex.IDUser equals usuario.IDUser
                    where kardex.IDProducto == id
                    select new KardexDto
                    {
                        IDKardex = kardex.IDKardex,
                        IDProducto = kardex.IDProducto,
                        NumDocumento = kardex.NumDocumento,
                        TipoMovimiento = kardex.TipoMovimiento,
                        Entrada = kardex.Entrada,
                        Salida = kardex.Salida,
                        Fecha = kardex.Fecha,
                        Usuario = new UsuarioDto
                        {
                            IDUser = usuario.IDUser,
                            CodUser = usuario.CodUser,
                            UserName = usuario.UserName,
                            IsAdmin = usuario.IsAdmin,
                            IsActive = usuario.IsActive,
                            CreateAt = usuario.CreateAt,
                            UpdateAt = usuario.UpdateAt
                        }
                    }
                ).ToListAsync();

            return result;
        }

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
