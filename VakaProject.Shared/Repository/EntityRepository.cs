using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace VakaProject.Shared.Repository;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, new()
{
    protected readonly DbContext _context;

    public EntityRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        return await (predicate == null
            ? _context.Set<TEntity>().CountAsync()
            : _context.Set<TEntity>().CountAsync(predicate));
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
    }

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties.Any())
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includeProperties.Any())
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.SingleOrDefaultAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
        return entity;
    }

    public async Task<TEntity?> GetWithoutTrackingAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public IQueryable<TEntity> Query()
    {
        return _context.Set<TEntity>();
    }
}