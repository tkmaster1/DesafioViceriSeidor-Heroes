using Heroes.Common.Util.DTOs;

namespace Heroes.Core.Application.DTOs.Filters;

public class HeroFilterDTO : FilterBaseDTO
{
    /// <summary>
    /// Codigo
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// Nome real do herói
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Nome do herói (único)
    /// </summary>
    public string HeroName { get; set; }
}