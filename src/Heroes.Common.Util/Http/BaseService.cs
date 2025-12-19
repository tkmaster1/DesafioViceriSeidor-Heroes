using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Response;
using Heroes.Common.Util.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Heroes.Common.Util.Http;

/// <summary>
/// Base HTTP client service implementing the shared IBaseService contract.
/// Uses HttpClientFactory, propagates Authorization, supports Polly via DI.
/// </summary>
public class BaseHttpService : IBaseService
{
    #region Properties

    private readonly IHttpClientFactory _factory;
    private readonly IHttpContextAccessor _http;
    private readonly string _mediaType;
    private readonly string _clientName; // <- NOME DO CLIENT (apiName) que este service usará

    public string UrlBase { get; }

    private static readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    #endregion

    #region Constructor

    // Construtor padrão (usa DefaultApi)
    public BaseHttpService(IHttpClientFactory factory, IHttpContextAccessor http, IConfiguration cfg)
        : this(factory, http, cfg, cfg["ApiClient:DefaultApi"] ?? cfg.GetSection("ApiClient:Apis").GetChildren().First().Key)
    { }

    // Construtor específico por API (o resolver vai usar este)
    public BaseHttpService(IHttpClientFactory factory, IHttpContextAccessor http, IConfiguration cfg, string apiName)
    {
        _factory = factory;
        _http = http;

        _mediaType = cfg["ApiClient:DefaultMediaType"] ?? "application/json";
        _clientName = apiName;

        // Garante a UrlBase correta para ESTE apiName
        UrlBase = (cfg[$"ApiClient:Apis:{apiName}"] ?? string.Empty).TrimEnd('/');
    }

    #endregion

    #region Request Builders

    public HttpRequestMessage Get(string url) => new(HttpMethod.Get, url);
    public HttpRequestMessage Delete(string url) => new(HttpMethod.Delete, url);

    public HttpRequestMessage Post(string url, object? body = null, string? contentType = "application/json")
        => BuildRequest(HttpMethod.Post, url, body, contentType);

    public HttpRequestMessage Put(string url, object? body = null, string? contentType = "application/json")
        => BuildRequest(HttpMethod.Put, url, body, contentType);

    #endregion

    #region Send + parse

    public async Task<ResponseSuccess<T>> BuildResponse<T>(HttpRequestMessage request, CancellationToken ct = default) where T : class
    {
        var client = CreateClient();
        var response = await client.SendAsync(request, ct);
        return await ReadResponse<ResponseSuccess<T>>(response);
    }

    public async Task<ResponseSuccess<IEnumerable<T>>> BuildListResponse<T>(HttpRequestMessage request, CancellationToken ct = default) where T : class
    {
        var client = CreateClient();
        var response = await client.SendAsync(request, ct);
        return await ReadResponse<ResponseSuccess<IEnumerable<T>>>(response);
    }
    
    public async Task<ResponseSuccess<PaginationDTO<T>>> BuildPaginatedResponse<T>(HttpRequestMessage request, CancellationToken ct = default) where T : class, new()
    {
        var client = CreateClient();
        var response = await client.SendAsync(request, ct);
        return await ReadResponse<ResponseSuccess<PaginationDTO<T>>>(response);
    }

    #endregion
    
    #region Private

    private HttpClient CreateClient(string? mediaType = null)
    {
        // Usa SEMPRE o named client que foi configurado no DI para ESTE apiName
        var client = _factory.CreateClient(_clientName);

        // Se o AddHttpClient já setou BaseAddress, ótimo; senão garantimos:
        if (client.BaseAddress is null && !string.IsNullOrEmpty(UrlBase))
            client.BaseAddress = new Uri(UrlBase + "/");

        client.DefaultRequestHeaders.Accept.Clear();

        var mt = mediaType ?? _mediaType;
        if (mt.Equals("multipart", StringComparison.OrdinalIgnoreCase))
        {
            client.DefaultRequestHeaders.TransferEncodingChunked = true;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        }
        else
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //if (string.IsNullOrEmpty(mediaType))
        //{
        //    // Associar o token aos headers do objeto do tipo HttpClient
        //    var token = _http.HttpContext.User.GetUserToken();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        return client;
    }

    private static async Task<TResponse> ReadResponse<TResponse>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<TResponse>();
            return data ?? throw new InvalidOperationException("Resposta nula inesperada.");
        }

        var error = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Erro na chamada: {response.StatusCode} - {error}");
    }

    private HttpRequestMessage BuildRequest(HttpMethod method, string url, object? body = null, string? mediaType = null)
    {
        var req = new HttpRequestMessage(method, url);
        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, _json);

            var finalMediaType = string.IsNullOrWhiteSpace(mediaType) ? _mediaType : mediaType;

            req.Content = new StringContent(json, Encoding.UTF8, finalMediaType);
        }

        return req;
    }

    #endregion
}