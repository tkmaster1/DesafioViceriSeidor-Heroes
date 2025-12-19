namespace Heroes.Common.Util.Helpers;

/// <summary>
/// Atributo customizado usado para marcar propriedades em classes de filtro que devem ser consideradas na construção de filtros dinâmicos.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class FilterableAttribute : Attribute
{
    public string Comparison { get; }

    public FilterableAttribute(string comparison = "==")
    {
        Comparison = comparison;
    }
}