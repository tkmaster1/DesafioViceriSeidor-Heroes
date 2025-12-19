using Heroes.Common.Util.Filters;
using Heroes.Common.Util.Helpers;

namespace Heroes.Core.Domain.Filters;

public class HeroFilter : FilterBase
{
    /// <summary>
    /// Codigo
    /// </summary>
    [Filterable("==")]
    public int? Code { get; set; }

    /// <summary>
    /// Nome real do herói
    /// </summary>
    [Filterable("Contains")]
    public string Name { get; set; }

    /// <summary>
    /// Nome do herói (único)
    /// </summary>
    [Filterable("Contains")]
    public string HeroName { get; set; }

}