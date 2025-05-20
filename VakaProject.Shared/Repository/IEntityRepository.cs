using System.Linq.Expressions;

namespace VakaProject.Shared.Repository;

public interface IEntityRepository<T> where T : class, new()
{
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
    Task<T?> GetWithoutTrackingAsync(Expression<Func<T, bool>> predicate);
    
    Task<T> AddAsync(T entity);

    Task DeleteAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
}