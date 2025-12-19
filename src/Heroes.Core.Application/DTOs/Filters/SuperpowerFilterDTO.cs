using Heroes.Common.Util.DTOs;

namespace Heroes.Core.Application.DTOs.Filters;

public class SuperpowerFilterDTO : FilterBaseDTO
{
    /// <summary>
    /// Codigo
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// Descrição do superpoder
    /// </summary>
    public string Description { get; set; } = null!;
}