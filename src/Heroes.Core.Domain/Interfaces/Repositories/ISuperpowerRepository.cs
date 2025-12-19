using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;

namespace Heroes.Core.Domain.Interfaces.Repositories;

public interface ISuperpowerRepository : IRepositoryBase<Superpower>
{
    Task<List<Superpower>> GetListByFilterAsync(SuperpowerFilter filter);

    Task<int> CountByFilterAsync(SuperpowerFilter filter);
}