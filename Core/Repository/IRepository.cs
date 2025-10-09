using Core.Entity;
using System.Linq.Expressions;

namespace Core.Repository;

public interface IRepository<T> where T: EntityBase
{
    //IList<T> ObterTodos();
    //T ObterPorID(int id);    
    //void Cadastrar(T entidade);
    //void Alterar (T entidade);
    //void Deletar (int  id);

    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
    Task<T?> BuscarAsync(int id);
    Task<T> AdicionarAsync(T entity);
    Task<T> AlterarAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();

}
