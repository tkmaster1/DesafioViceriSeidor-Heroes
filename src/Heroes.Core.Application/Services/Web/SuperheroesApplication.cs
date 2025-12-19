using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Http;
using Heroes.Common.Util.Response;
using Heroes.Common.Util.Services;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.Services.Web.Interfaces;

namespace Heroes.Core.Application.Services.Web;

public class SuperheroesApplication : ISuperheroesApplication
{
    #region Properties

    private readonly IBaseService _base;

    #endregion

    #region Constructor

    public SuperheroesApplication(IBaseService baseService) => _base = baseService;

    #endregion

    #region Method

    public async Task<ResponseSuccess<PaginationDTO<HeroDTO>>> ListSuperheroesByFilters(
        HeroFilterDTO req, CancellationToken ct = default)
    {
        var request = _base.Post("Heroes/getHeroes", req, "application/json");
        return await BaseServiceExtensions.ToPaginatedResponse<HeroDTO>(_base, request, ct);
    }

    public async Task<ResponseSuccess<HeroDTO>> GetSuperheroByCodeAsync(int code, CancellationToken ct = default)
    {
        var request = _base.Get($"Heroes/getHeroByCode/{code}");
        return await BaseServiceExtensions.ToResponse<HeroDTO>(_base, request, ct);
    }

    #endregion
}