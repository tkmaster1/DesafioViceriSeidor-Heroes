using Heroes.Common.Util.Abstractions;
using Heroes.Common.Util.Entities;

namespace Heroes.Common.Util.Response;

/// <summary>
/// Standard successful response envelope with a strongly-typed payload.
/// </summary>
public class ResponseSuccess<T> where T : class
{
    /// <summary>
    /// Indicates that the operation succeeded.
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// Successful payload.
    /// </summary>
    public T Data { get; set; } = default!;
}

/// <summary>
/// Standard failure response envelope with a list of error messages.
/// </summary>
public class ResponseFailure
{
    /// <summary>
    /// Always false for failures.
    /// </summary>
    public bool Success { get; set; } = false;

    /// <summary>
    /// List of error messages (human-readable).
    /// </summary>
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
}

public class ResponseBaseEntity : ResponseSuccess<Entity>
{
}

/// <summary>
/// Success response envelope specifically for paginated payloads,
/// but abstracted by IPagedResult to avoid coupling to specific DTOs.
/// </summary>
public class ResponsePaged<TDto> : ResponseSuccess<IPagedResult<TDto>> where TDto : class, new() { }
