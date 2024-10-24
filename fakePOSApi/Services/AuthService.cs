using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fakePOSApi.Services
{
    public class AuthService : IAuthService<UserRegisterDto, UserLoginDto, Usuario>
    {
        private IAuthRepository<Usuario> _authRepository;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _durationInMinutes;

        public List<string> Message { get; private set; } = new List<string>();

        public AuthService(IAuthRepository<Usuario> authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _durationInMinutes = int.Parse(configuration["Jwt:DurationInMinutes"]);
        }

        public async Task<bool> Register(UserRegisterDto dto)
        {
            if(await _authRepository.UserExist(dto.CodUser))
            {
                Message.Add("El usuario ya existe.");
                return false;
            }

            var user = new Usuario()
            {
                CodUser = dto.CodUser,
                UserName = dto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsAdmin = dto.IsAdmin,
                IsActive = true
            };

            await _authRepository.Register(user);
            await _authRepository.Save();

            Message.Add("Registro de usuario exitoso.");
            return true;
        }

        public async Task<string> Login(UserLoginDto dto)
        {
            var user = await _authRepository.Login(dto.CodUser);

            var token = GenerateToken(user);
            return token;
        }

        public string GenerateToken(Usuario entity)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, entity.CodUser),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("IDUser", entity.IDUser.ToString()),
                new Claim(ClaimTypes.Role, entity.IsAdmin ? "Admin" : "Vendedor"),
                new Claim("IsActive", entity.IsActive.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                                issuer: _issuer,
                                audience: _audience,
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(_durationInMinutes),
                                signingCredentials: creds
                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Validate(UserLoginDto dto)
        {
            var user = await _authRepository.Login(dto.CodUser);

            if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                Message.Add("Creedenciales invalidas.");
                return false;
            }

            if(!user.IsActive)
            {
                Message.Add("El Usuario esta inactivo.");
                return false;
            }

            return true;
        }
    }
}
