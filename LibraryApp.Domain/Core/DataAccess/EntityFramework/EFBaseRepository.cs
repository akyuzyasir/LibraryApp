using LibraryApp.Domain.Core.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace LibraryApp.Domain.Core.DataAccess.EntityFramework;

/// <summary>
/// Abstract base class providing Entity Framework Core implementation for generic repository operations
/// </summary>
/// <typeparam name="TEntity">Entity type deriving from BaseEntity</typeparam>
public abstract class EFBaseRepository<TEntity> : IAsyncOrderableRepository<TEntity>,
                                                 IAsyncFindableRepository<TEntity>,
                                                 IAsyncQueryableRepository<TEntity>,
                                                 IAsyncInsertableRepository<TEntity>,
                                                 IAsyncUpdateableRepository<TEntity>,
                                                 IAsyncDeleteableRepository<TEntity>,
                                                 IAsyncRepository,
                                                 IAsyncTransactionRepository,
                                                 IRepository
    where TEntity : BaseEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _table;

    /// <summary>
    /// Initializes a new instance of the repository with database context
    /// </summary>
    /// <param name="context">DbContext for database operations</param>
    public EFBaseRepository(DbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    /// <summary>
    /// Asynchronously adds a new entity to the database
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>Task representing async operation with added entity</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _table.AddAsync(entity);
        return entry.Entity;
    }

    /// <summary>
    /// Asynchronously adds multiple entities to the database
    /// </summary>
    /// <param name="entities">Collection of entities to add</param>
    /// <returns>Task representing async operation</returns>
    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return _table.AddRangeAsync(entities);
    }

    /// <summary>
    /// Checks if any entities exist matching optional filter condition
    /// </summary>
    /// <param name="expression">Optional filter condition</param>
    /// <returns>Task with boolean result</returns>
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        return expression is null ? GetAllActives().AnyAsync() : GetAllActives().AnyAsync(expression);
    }

    /// <summary>
    /// Immediately deletes an entity from the database
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    public void Delete(TEntity entity)
    {
        _table.Remove(entity);
    }

    /// <summary>
    /// Asynchronously deletes an entity from the database
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    /// <returns>Task representing async operation</returns>
    public Task DeleteAsync(TEntity entity)
    {
        return Task.FromResult(_table.Remove(entity));
    }

    /// <summary>
    /// Retrieves all active entities from the database
    /// </summary>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Task with collection of entities</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
    {
        return await GetAllActives(tracking).ToListAsync();
    }

    /// <summary>
    /// Retrieves active entities matching filter condition
    /// </summary>
    /// <param name="expression">Filter condition</param>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Task with filtered entity collection</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        return await GetAllActives(tracking).Where(expression).ToListAsync();
    }

    /// <summary>
    /// Retrieves active entities sorted by specified key
    /// </summary>
    /// <typeparam name="TKey">Sort key type</typeparam>
    /// <param name="orderby">Sort selector</param>
    /// <param name="orderDesc">Descending order flag (default: false)</param>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Task with sorted entity collection</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = GetAllActives(tracking);
        return orderDesc ? await values.OrderByDescending(orderby).ToListAsync() : await values.OrderBy(orderby).ToListAsync();
    }

    /// <summary>
    /// Retrieves filtered active entities sorted by specified key
    /// </summary>
    /// <typeparam name="TKey">Sort key type</typeparam>
    /// <param name="expression">Filter condition</param>
    /// <param name="orderby">Sort selector</param>
    /// <param name="orderDesc">Descending order flag (default: false)</param>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Task with filtered, sorted entity collection</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = GetAllActives(tracking).Where(expression);
        return orderDesc ? await values.OrderByDescending(orderby).ToListAsync() : await values.OrderBy(orderby).ToListAsync();
    }

    /// <summary>
    /// Retrieves first active entity matching filter condition
    /// </summary>
    /// <param name="expression">Filter condition</param>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Task with matching entity or null</returns>
    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        return GetAllActives(tracking).FirstOrDefaultAsync(expression);
    }

    /// <summary>
    /// Retrieves active entity by unique identifier
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Task with matching entity or null</returns>
    public Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
    {
        var values = GetAllActives(tracking);
        return values.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Saves all changes made in the context to the database
    /// </summary>
    /// <returns>Number of affected records</returns>
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    /// <summary>
    /// Asynchronously saves all changes made in the context
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task with number of affected records</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing entity in the database
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <returns>Task with updated entity</returns>
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = await Task.FromResult(_table.Update(entity));
        return entry.Entity;
    }

    /// <summary>
    /// Begins a database transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task with database transaction</returns>
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Creates execution strategy for database operations
    /// </summary>
    /// <returns>Task with execution strategy instance</returns>
    public Task<IExecutionStrategy> CreateExecutionStrategy()
    {
        return Task.FromResult(_context.Database.CreateExecutionStrategy());
    }

    /// <summary>
    /// Provides base query with active entities filter
    /// </summary>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Filtered queryable collection</returns>
    protected IQueryable<TEntity> GetAllActives(bool tracking = true)
    {
        var values = _table.Where(x => x.Status != Status.Deleted);
        return tracking ? values : values.AsNoTracking();
    }

    /// <summary>
    /// Asynchronously deletes multiple entities
    /// </summary>
    /// <param name="entities">Entities to delete</param>
    /// <returns>Task representing async operation</returns>
    public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        _table.RemoveRange(entities);
        return _context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all entities including soft-deleted records
    /// </summary>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <returns>Task with complete entity collection</returns>
    public async Task<IEnumerable<TEntity>> GetAllDataAsync(bool tracking = true)
    {
        return tracking ? await _table.ToListAsync() : await _table.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Retrieves entity by ID with included related entities
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <param name="includes">Navigation properties to include</param>
    /// <returns>Task with entity or null</returns>
    public async Task<TEntity?> GetByIdWithIncludeAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetAllActives().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Retrieves filtered entities with included related entities
    /// </summary>
    /// <param name="expression">Filter condition</param>
    /// <param name="tracking">Enable entity tracking (default: true)</param>
    /// <param name="includes">Include specifications</param>
    /// <returns>Task with filtered entity collection</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = tracking ?
            _context.Set<TEntity>().Where(expression) :
            _context.Set<TEntity>().AsNoTracking().Where(expression);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = include(query);
            }
        }

        return await query.ToListAsync();
    }
}