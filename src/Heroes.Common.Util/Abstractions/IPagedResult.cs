namespace Heroes.Common.Util.Abstractions;

/// <summary>
/// Minimal contract for paginated results.
/// Implement this in DTOs or domain models that represent paged lists.
/// </summary>
public interface IPagedResult<T> where T : class
{
    int CurrentPage { get; }

    int Count { get; }

    int PageSize { get; }

    int TotalPages { get; }

    List<T> Result { get; }
}