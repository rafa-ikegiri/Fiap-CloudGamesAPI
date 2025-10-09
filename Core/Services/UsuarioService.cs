using Core.Entity;
using Core.Input;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _userRepository;

        public UsuarioService(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Usuario> RegisterAsync(UsuarioInput inputlogin)
        {
            if (string.IsNullOrWhiteSpace(inputlogin.Email) || !inputlogin.Email.Contains("@"))
                throw new ArgumentException("E-mail inválido.");

            if (!IsPasswordStrong(inputlogin.Senha))
                throw new ArgumentException("Senha fraca. A senha deve ter no mínimo 8 caracteres, incluir números, letras e caracteres especiais.");

            var exists = await _userRepository.ObterUsuarioPorEmailAsync(inputlogin.Email);
            if (exists != null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            var user = new Usuario(
                name: inputlogin.Nome,
                email: inputlogin.Email,
                password: inputlogin.Senha,
                role: inputlogin.IsAdmin ? "admin" : "user",
                dataCriacao: DateTime.UtcNow
            );

            await _userRepository.AdicionarAsync(user);

            return user;
        }

        private bool IsPasswordStrong(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => "!@#$%^&*()_+-=,./?><".Contains(ch));
        }

        public async Task<List<UsuarioInput>> BuscarTodosUsuarios()
        {
            var users = await _userRepository.GetByConditionAsync(u => true);
            return users.Select(u => new UsuarioInput
            {
                Nome = u.Nome,
                Email = u.Email,
                Senha = u.Senha,
                IsAdmin = u.IsAdmin
            }).ToList();
        }

        public async Task<UsuarioInput?> GetByIdAsync(int id)
        {
            var usuario = await _userRepository.BuscarAsync(id);
            if (usuario == null) return null;

            return new UsuarioInput
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                IsAdmin = usuario.IsAdmin
            };
        }

        public async Task<UsuarioInput?> UpdateAsync(int id, UsuarioUpdateInput usuarioupdateinput)
        {
            var usuario = await _userRepository.BuscarAsync(id);
            if (usuario == null) return null;
                                  
            usuario.UsuarioUpdate(
             usuarioupdateinput.Nome,
             usuarioupdateinput.Email,
             usuarioupdateinput.Senha,
             usuarioupdateinput.IsAdmin ? "admin" : "user"
 );

            await _userRepository.AlterarAsync(usuario);

            return new UsuarioInput
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                IsAdmin = usuario.IsAdmin
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.BuscarAsync(id);
            if (user == null)
                return false;

            return await _userRepository.DeleteAsync(id);
        }
    }
}
