using Heroes.Core.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Heroes.Core.Data.Repositories;

/// <summary>
/// Classe base genérica para repositórios com suporte a contexto principal e contexto de identidade (opcional).
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
/// <typeparam name="TContext">Tipo do DbContext principal.</typeparam>
public abstract class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>, IDisposable where TEntity : class, new() where TContext : DbContext
{
    #region Fields and Constructor

    protected readonly TContext _mainContext;
    protected readonly DbContext _identityContext;
    protected readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Construtor com contexto principal e identidade opcional.
    /// </summary>
    /// <param name="dbContext">Contexto principal.</param>
    /// <param name="identityContext">Contexto de identidade (opcional).</param>
    protected RepositoryBase(TContext context, TContext? identityContext = null)
    {
        _mainContext = context;
        _identityContext = identityContext;
        _dbSet = _mainContext.Set<TEntity>();
    }

    #endregion

    #region Basic CRUD

    public virtual async Task<TEntity> GetByCodeAsync(int code) => await _dbSet.FindAsync(code);

    public virtual async Task<TEntity> GetByNameAsync(string name) => await _dbSet.FindAsync(name);

    public virtual async Task<bool> ExistAsync(int code) => await GetByCodeAsync(code) != null;

    public virtual async Task<IEnumerable<TEntity>> ListAllAsync() => await _dbSet.ToListAsync();

    public virtual IQueryable<TEntity> ToObtain() => _dbSet;

    #endregion

    #region Add / Update / Remove

    public virtual void ToAdd(TEntity entity) => _dbSet.Add(entity);

    public virtual void ToAdd(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

    public virtual void ToUpdate(TEntity entity) => _dbSet.Update(entity);

    public virtual void ToUpdate(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

    public virtual void ToRemove(int code)
    {
        var entity = _dbSet.Find(code);
        if (entity != null) _dbSet.Remove(entity);
    }

    public virtual void ToRemove(TEntity entity) => _dbSet.Remove(entity);

    #endregion

    #region Search and Save

    public virtual async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate) =>
        await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

    public virtual async Task<int> ToSaveAsync() => await _mainContext.SaveChangesAsync();

    #endregion

    //#region Identity Context

    //public virtual async Task<int> SaveIdentityAsync()
    //{
    //    if (_identityContext is null)
    //        throw new InvalidOperationException("O contexto de identidade não foi fornecido.");

    //    return await _identityContext.SaveChangesAsync();
    //}

    //public virtual void UpdateIdentity(TEntity entity)
    //{
    //    if (_identityContext is null)
    //        throw new InvalidOperationException("O contexto de identidade não foi fornecido.");

    //    _identityContext.Entry(entity).State = EntityState.Modified;
    //    _identityContext.Set<TEntity>().Attach(entity);
    //}

    //#endregion

    #region IDisposable

    /// <summary>
    /// Libera os recursos utilizados pelo repositório.
    /// </summary>
    public void Dispose()
    {
        _mainContext?.Dispose();
        _identityContext?.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion
}