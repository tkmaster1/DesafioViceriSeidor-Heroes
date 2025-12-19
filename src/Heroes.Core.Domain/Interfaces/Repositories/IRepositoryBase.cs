using System.Linq.Expressions;

namespace Heroes.Core.Domain.Interfaces.Repositories;

/// <summary>
/// Interface genérica para repositórios de entidades.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IRepositoryBase<TEntity>
    where TEntity : class, new()
{
    #region Search

    /// <summary>
    /// Busca entidade pelo código.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<TEntity> GetByCodeAsync(int code);

    /// <summary>
    /// Busca entidade pelo nome.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<TEntity> GetByNameAsync(string name);

    /// <summary>
    /// Verifica existência da entidade por código.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<bool> ExistAsync(int code);

    /// <summary>
    /// Busca entidades com base em um predicado.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Lista todas as entidades.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> ListAllAsync();

    /// <summary>
    /// Retorna IQueryable da entidade.
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> ToObtain();

    #endregion

    #region Add / Update / Remove

    /// <summary>
    /// Adiciona uma nova entidade.
    /// </summary>
    /// <param name="entity"></param>
    void ToAdd(TEntity entity);

    /// <summary>
    /// Adiciona múltiplas entidades.
    /// </summary>
    /// <param name="entities"></param>
    void ToAdd(IEnumerable<TEntity> entities);

    /// <summary>
    /// Atualiza uma entidade existente.
    /// </summary>
    /// <param name="entity"></param>
    void ToUpdate(TEntity entity);

    /// <summary>
    /// Atualiza múltiplas entidades.
    /// </summary>
    /// <param name="entities"></param>
    void ToUpdate(IEnumerable<TEntity> entities);

    /// <summary>
    /// Remove entidade pelo código.
    /// </summary>
    /// <param name="code"></param>
    void ToRemove(int code);

    /// <summary>
    /// Remove uma entidade específica.
    /// </summary>
    /// <param name="entity"></param>
    void ToRemove(TEntity entity);

    #endregion

    #region Save

    /// <summary>
    /// Salva as alterações no contexto principal.
    /// </summary>
    /// <returns></returns>
    Task<int> ToSaveAsync();

    ///// <summary>
    ///// Atualiza a entidade no contexto de identidade.
    ///// </summary>
    ///// <param name="entity"></param>
    //void UpdateIdentity(TEntity entity);

    ///// <summary>
    ///// Salva alterações no contexto de identidade.
    ///// </summary>
    ///// <returns></returns>
    //Task<int> SaveIdentityAsync();

    #endregion
}