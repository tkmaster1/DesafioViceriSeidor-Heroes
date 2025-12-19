using Heroes.Common.Util.Http;
using Heroes.Common.Util.Services;
using Heroes.Core.Application.Services.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heroes.Core.Application.Services.Web;

public sealed class BaseServiceResolver : IBaseServiceResolver
{
    #region Properties

    private readonly IServiceProvider _provider;

    private readonly IConfiguration _cfg;

    public string DefaultApiName { get; }

    #endregion

    #region Constructor

    public BaseServiceResolver(IServiceProvider provider, IConfiguration cfg)
    {
        _provider = provider;
        _cfg = cfg;

        DefaultApiName = _cfg["ApiClient:DefaultApi"]
            ?? _cfg.GetSection("ApiClient:Apis").GetChildren().First().Key;
    }

    #endregion

    #region Methods

    public IBaseService For(string apiName)
    {
        // Resolve dependências necessárias
        var factory = _provider.GetRequiredService<IHttpClientFactory>();
        var http = _provider.GetRequiredService<IHttpContextAccessor>();

        // IMPORTANTÍSSIMO: cria uma NOVA instância de BaseHttpService para cada apiName
        return new BaseHttpService(factory, http, _cfg, apiName);
    }

    #endregion

}