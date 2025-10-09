using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services
{
    public class AuthService
    {

        private readonly IUsuarioRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginOutput> LoginAsync(LoginInput inputlogin)
        {
            var user = await _userRepository.ObterUsuarioPorEmailAsync(inputlogin.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
            }
            else if (user.Email != inputlogin.Email || user.Senha != inputlogin.Senha)
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
            }
                        
            var token = GenerateToken(user);

            return new LoginOutput
            {
                Token = token,
                Nome = user.Nome,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
        }

        private string GenerateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
