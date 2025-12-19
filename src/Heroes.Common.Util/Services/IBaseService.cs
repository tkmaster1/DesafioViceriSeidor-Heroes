using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Response;

namespace Heroes.Common.Util.Services;

public interface IBaseService
{
    #region Properties

    string UrlBase { get; }

    #endregion

    #region Methods

    HttpRequestMessage Get(string url);
    HttpRequestMessage Delete(string url);
    HttpRequestMessage Post(string url, object? body = null, string? contentType = null);
    HttpRequestMessage Put(string url, object? body = null, string? contentType = null);

    Task<ResponseSuccess<T>> BuildResponse<T>(HttpRequestMessage request, CancellationToken ct = default)
            where T : class;

    Task<ResponseSuccess<IEnumerable<T>>> BuildListResponse<T>(HttpRequestMessage request, CancellationToken ct = default)
        where T : class;

    Task<ResponseSuccess<PaginationDTO<T>>> BuildPaginatedResponse<T>(HttpRequestMessage request, CancellationToken ct = default)
        where T : class, new();

    #endregion
}