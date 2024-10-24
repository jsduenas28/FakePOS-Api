using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;

namespace fakePOSApi.Services
{
    public class UsuarioService : IUsuarioService<UsuarioDto, UsuarioUpdateDto, UsuarioChangePasswordDto>
    {
        private IUsuarioRepository<Usuario> _repository;
        public List<string> Message { get; private set; } = new List<string>();

        public UsuarioService(IUsuarioRepository<Usuario> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioDto>> Get()
        {
            var usuarios = await _repository.Get();

            return usuarios.Select(u => new UsuarioDto
            {
                IDUser = u.IDUser,
                CodUser = u.CodUser,
                UserName = u.UserName,
                IsAdmin = u.IsAdmin,
                IsActive = u.IsActive
            });
        }

        public async Task<UsuarioDto> GetByID(int id)
        {
            var usuario = await _repository.GetByID(id);

            return new UsuarioDto
            {
                IDUser = usuario.IDUser,
                CodUser = usuario.CodUser,
                UserName = usuario.UserName,
                IsAdmin = usuario.IsAdmin,
                IsActive = usuario.IsActive
            };
        }

        public async Task<IEnumerable<UsuarioDto>> Search(string CodUser)
        {
            var usuarios = await _repository.Search(CodUser);

            return usuarios.Select(u => new UsuarioDto
            {
                IDUser = u.IDUser,
                CodUser = u.CodUser,
                UserName = u.UserName,
                IsAdmin = u.IsAdmin,
                IsActive = u.IsActive
            });
        }

        public async Task<UsuarioDto> Update(int id, UsuarioUpdateDto dto)
        {
            var usuario = await _repository.GetByID(id);

            usuario.UserName = dto.UserName;
            _repository.Update(usuario);
            await _repository.Save();

            return new UsuarioDto
            {
                IDUser = usuario.IDUser,
                CodUser = usuario.CodUser,
                UserName = usuario.UserName,
                IsAdmin = usuario.IsAdmin,
                IsActive = usuario.IsActive
            };
        }

        public async Task ChangeIsActive(int id)
        {
            var usuario = await _repository.GetByID(id);

            if(usuario.IsActive)
            {
                Message.Add("El estado del Usuario: " + usuario.CodUser + " cambio a inactivo.");
                usuario.IsActive = false;
            } else
            {
                Message.Add("El estado del Usuario: " + usuario.CodUser + " cambio a activo.");
                usuario.IsActive = true;
            }

            _repository.Update(usuario);
            await _repository.Save();
        }

        public async Task ChangeIsAdmin(int id)
        {
            var usuario = await _repository.GetByID(id);

            if(usuario.IsAdmin)
            {
                Message.Add("El Usuario: " + usuario.CodUser + " ahora ya no tiene permisos de Administrador");
                usuario.IsAdmin = false;
            } else
            {
                Message.Add("El Usuario: " + usuario.CodUser + " ahora tiene permisos de Administrador");
                usuario.IsAdmin = true;
            }

            _repository.Update(usuario);
            await _repository.Save();
        }

        public async Task ChangePassword(int id, UsuarioChangePasswordDto dto)
        {
            var usuario = await _repository.GetByID(id);

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _repository.Update(usuario);
            await _repository.Save();

            Message.Add("Cambio de contraseña exitoso.");
        }

        public async Task<bool> Validate(int id)
        {
            var usuario = await _repository.GetByID(id);

            if(usuario == null)
            {
                Message.Add("El IDUser no existe.");
                return false;
            }

            return true;
        }

        public async Task<bool> Validate(int id, UsuarioChangePasswordDto dto)
        {
            var usuario = await _repository.GetByID(id);

            if(!BCrypt.Net.BCrypt.Verify(dto.OldPassword, usuario.Password))
            {
                Message.Add("Error al verificar la contraseña actual.");
                return false;
            }

            if(dto.OldPassword == dto.NewPassword)
            {
                Message.Add("La nueva contraseña no puede ser igual que la anterior.");
                return false;
            }

            if(dto.NewPassword != dto.RepeatNewPassword)
            {
                Message.Add("La nueva contraseña y la confirmación no son iguales.");
                return false;
            }

            return true;
        }
    }
}
