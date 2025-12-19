using Heroes.Common.Util.Services;
using Heroes.Core.Application.Facades.Interfaces;
using Heroes.Core.Application.Services.Web;
using Heroes.Core.Application.Services.Web.Interfaces;

namespace Heroes.Core.Application.Facades;

public class ApiFacade : IApiFacade
{
    #region Properties

    private readonly IBaseService _urlApi;

    #endregion

    #region Constructor

    public ApiFacade(IBaseServiceResolver resolver)
    {
        _urlApi = resolver.For(resolver.DefaultApiName); // ex.: AuthApi
    }

    #endregion

    #region Method

    public ISuperheroesApplication SuperheroesApp => new SuperheroesApplication(_urlApi);

    #endregion
}