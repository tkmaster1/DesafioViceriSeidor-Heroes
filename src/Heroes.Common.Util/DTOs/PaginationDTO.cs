using Heroes.Common.Util.Abstractions;

namespace Heroes.Common.Util.DTOs;

/// <summary>
/// Pagination container for API DTOs.
/// </summary>
public class PaginationDTO<TDto> : IPagedResult<TDto> where TDto : class, new()
{
    public int CurrentPage { get; set; }

    public int Count { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public List<TDto> Result { get; set; } = new();
}