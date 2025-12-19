using Heroes.Common.Util.Filters;
using Heroes.Common.Util.Helpers;

namespace Heroes.Core.Domain.Filters;

public class SuperpowerFilter : FilterBase
{
    /// <summary>
    /// Codigo
    /// </summary>
    [Filterable("==")]
    public int? Code { get; set; }

    /// <summary>
    /// Descrição do superpoder
    /// </summary>
    [Filterable("Contains")]
    public string Description { get; set; }
}