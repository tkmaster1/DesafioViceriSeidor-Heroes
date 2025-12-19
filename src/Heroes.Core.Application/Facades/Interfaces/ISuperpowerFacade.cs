using Heroes.Common.Util.DTOs;
using Heroes.Common.Util.Response;
using Heroes.Core.Application.DTOs;
using Heroes.Core.Application.DTOs.Filters;
using Heroes.Core.Application.DTOs.Request;

namespace Heroes.Core.Application.Facades.Interfaces;

public interface ISuperpowerFacade : IDisposable
{
    /// <summary>
    /// Listar por filtros
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    Task<PaginationDTO<SuperpowerDTO>> ListByFiltersAsync(SuperpowerFilterDTO filterDto);

    /// <summary>
    /// Obter por Código
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<SuperpowerDTO> GetByCodeAsync(int code);

    /// <summary>
    /// Criação de um novo Superpoder
    /// </summary>
    /// <param name="requestDTO"></param>
    /// <returns></returns>
    Task<int> CreateSuperpowerAsync(SuperpowerRequestDTO requestDTO);

    /// <summary>
    /// Atualizar dados de um Superpoder
    /// </summary>
    /// <param name="requestDTO"></param>
    /// <returns></returns>
    Task<bool> UpdateSuperpowerAsync(SuperpowerRequestDTO requestDTO);

    /// <summary>
    /// Remover um Superpoder
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<ResponseMessage> RemoveSuperpowerAsync(int code);
}