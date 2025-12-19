using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Heroes.Common.Util.Helpers;

/// <summary>
/// Classe estática utilitária que centraliza métodos genéricos para construção dinâmica de filtros e ordenações sobre queries (IQueryable). Utiliza System.Linq.Dynamic.Core para construir expressões de forma dinâmica.
/// </summary>
public static class QueryHelper
{
    /// <summary>
    /// Aplica ordenação dinâmica à consulta com base no nome da propriedade e direção informada.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade da consulta.</typeparam>
    /// <param name="query">Consulta base a ser ordenada.</param>
    /// <param name="orderBy">Nome da propriedade da entidade usada para ordenação.</param>
    /// <param name="sortDirection">Direção da ordenação ("asc" ou "desc"). Padrão é "asc".</param>
    /// <returns>Consulta ordenada dinamicamente.</returns>
    public static IQueryable<T> ApplySorting<T>(
        IQueryable<T> query,
        string sortField,
        string sortDirection = "asc")
    {
        if (string.IsNullOrWhiteSpace(sortField))
            return query;

        sortDirection = sortDirection?.ToLower() == "desc" ? "descending" : "ascending";
        var sortExpression = $"{sortField} {sortDirection}";

        return query.OrderBy(sortExpression);
    }

    /// <summary>
    /// Aplica dinamicamente filtros à consulta com base nas propriedades da classe de filtro TFilter,
    /// utilizando apenas aquelas marcadas com o atributo [Filterable].
    /// </summary>
    /// <typeparam name="TFilter">Tipo da classe de filtro.</typeparam>
    /// <typeparam name="TEntity">Tipo da entidade da consulta.</typeparam>
    /// <param name="query">Consulta base a ser filtrada.</param>
    /// <param name="filter">Objeto contendo os valores a serem filtrados.</param>
    /// <returns>Consulta com filtros aplicados dinamicamente.</returns>
    public static IQueryable<TEntity> ApplyFilter<TFilter, TEntity>(
        IQueryable<TEntity> query,
        TFilter filter)
    {
        if (filter == null)
            return query;

        var filterProps = typeof(TFilter).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in filterProps)
        {
            var attr = prop.GetCustomAttribute<FilterableAttribute>();
            if (attr == null) continue;

            var value = prop.GetValue(filter);
            if (value == null) continue;

            if (value is string strVal && string.IsNullOrWhiteSpace(strVal))
                continue;

            // Campo de destino na entidade (pode ajustar nomes aqui se forem diferentes)
            string targetPropName = prop.Name
                .Replace("From", "")  // para suporte a intervalos
                .Replace("To", "");

            var comparison = attr.Comparison;

            // Monta expressão dinâmica
            var condition = comparison == "Contains"
                ? $"{targetPropName}.Contains(@0)"
                : $"{targetPropName} {comparison} @0";

            query = query.Where(condition, value);
        }

        return query;
    }
}