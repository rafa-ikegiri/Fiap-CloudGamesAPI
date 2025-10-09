using Core.Entity;

namespace Core.Repository;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> ObterUsuarioPorEmailAsync(string email);
}
