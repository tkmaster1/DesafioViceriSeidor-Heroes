using Heroes.Common.Util.Entities;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;

namespace Heroes.Core.Domain.Interfaces.Services;

public interface IHeroAppService : IDisposable
{
    Task<Pagination<Hero>> ListByFiltersAsync(HeroFilter filter);

    Task<Hero> GetByCodeAsync(int code);

    Task<int> AddHeroAsync(Hero entity);

    Task<bool> EditHeroAsync(Hero entity);

    Task<bool> DeleteHeroAsync(int code);
}