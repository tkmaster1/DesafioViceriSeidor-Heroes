using Heroes.Common.Util.Entities;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;

namespace Heroes.Core.Domain.Interfaces.Services;

public interface ISuperpowerAppService : IDisposable
{
    Task<Pagination<Superpower>> ListByFiltersAsync(SuperpowerFilter filter);

    Task<Superpower> GetByCodeAsync(int code);

    Task<int> AddSuperpowerAsync(Superpower entity);

    Task<bool> EditSuperpowerAsync(Superpower entity);

    Task<bool> DeleteSuperpowerAsync(int code);
}
