using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Response;
using Heroes.Common.Util.Services;

namespace Heroes.Common.Util.Http;

public static class BaseServiceExtensions
{
    public static Task<ResponseSuccess<T>> ToResponse<T>(this IBaseService baseService, HttpRequestMessage request, CancellationToken ct = default)
            where T : class
            => baseService.BuildResponse<T>(request, ct);

    public static Task<ResponseSuccess<IEnumerable<T>>> ToResponseList<T>(this IBaseService baseService, HttpRequestMessage request, CancellationToken ct = default)
        where T : class
        => baseService.BuildListResponse<T>(request, ct);

    public static Task<ResponseSuccess<PaginationDTO<T>>> ToPaginatedResponse<T>(this IBaseService baseService, HttpRequestMessage request, CancellationToken ct = default)
        where T : class, new()
        => baseService.BuildPaginatedResponse<T>(request, ct);
}