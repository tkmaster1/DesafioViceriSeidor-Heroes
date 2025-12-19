using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Response;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.DTOs.Request;

namespace Heroes.Core.Application.Facades.Interfaces;

public interface IHeroFacade : IDisposable
{
    /// <summary>
    /// Listar por filtros
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    Task<PaginationDTO<HeroDTO>> ListByFiltersAsync(HeroFilterDTO filterDto);

    /// <summary>
    /// Obter por Código
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<HeroDTO> GetByCodeAsync(int code);

    /// <summary>
    /// Criação de um novo super-herói
    /// </summary>
    /// <param name="requestDTO"></param>
    /// <returns></returns>
    Task<int> CreateHeroAsync(HeroRequestDTO requestDTO);

    /// <summary>
    /// Atualizar dados de um super-herói
    /// </summary>
    /// <param name="requestDTO"></param>
    /// <returns></returns>
    Task<bool> UpdateHeroAsync(HeroRequestDTO requestDTO);

    /// <summary>
    /// Remover um super-herói
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<ResponseMessage> RemoveHeroAsync(int code);
}