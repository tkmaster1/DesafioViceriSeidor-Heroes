using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Response;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;

namespace Heroes.Core.Application.Services.Web.Interfaces;

public interface ISuperheroesApplication
{
    Task<ResponseSuccess<PaginationDTO<HeroDTO>>> ListSuperheroesByFilters(HeroFilterDTO req, CancellationToken ct = default);

    Task<ResponseSuccess<HeroDTO>> GetSuperheroByCodeAsync(int code, CancellationToken ct = default);

    /// <summary>
    /// Deletar dados do Super-herói
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<ResponseSuccess<object>> RemoveSuperhero(int code, CancellationToken ct = default);
}