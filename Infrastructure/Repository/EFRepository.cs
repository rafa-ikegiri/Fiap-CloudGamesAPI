using Core.Entity;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public class EFRepository<T> : IRepository<T> where T : EntityBase
{
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbSet;

    public EFRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

    public async Task<T?> BuscarAsync(int id) =>
        await _dbSet.FindAsync(id);

    public async Task<T> AdicionarAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> AlterarAsync(T entity)
    {
        var result = await _dbSet.FindAsync(entity.Id);
        if (result is null)
            return null;

        _context.Entry(result)
            .CurrentValues
            .SetValues(entity);

        await _context.SaveChangesAsync();
        return entity;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _dbSet.FindAsync(id);
        if (result is null)
            return false;

        _dbSet.Remove(result);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
