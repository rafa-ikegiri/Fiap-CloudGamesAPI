using Core.Entity;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
{

    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<Usuario> ObterUsuarioPorEmailAsync(string email)
    {
        return _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}
