using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;

namespace Heroes.Core.Domain.Interfaces.Repositories;

public interface IHeroRepository : IRepositoryBase<Hero>
{
    Task<List<Hero>> GetListByFilterAsync(HeroFilter filter);

    Task<int> CountByFilterAsync(HeroFilter filter);

    //  Task<bool> ExistsByUserCodeAsync(string codeUser);

  //  Task<Hero> GetByCodeAsync(int id, string codeUser);
}