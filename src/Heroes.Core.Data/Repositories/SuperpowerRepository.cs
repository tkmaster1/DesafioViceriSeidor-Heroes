using AutoMapper;
using Heroes.Common.Util.Helpers;
using Heroes.Core.Data.Context;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;
using Heroes.Core.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Core.Data.Repositories;

public class SuperpowerRepository : RepositoryBase<Superpower, AppDbContext>, ISuperpowerRepository
{
    #region Properties

    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public SuperpowerRepository(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public async Task<int> CountByFilterAsync(SuperpowerFilter filter)
    {
        var query = _mainContext.TBSuperpowers.AsQueryable();

        query = QueryHelper.ApplyFilter<SuperpowerFilter, Superpower>(query, filter);

        // Agora aplica um comportamento personalizado para o campo "Status"
        if (filter.Status == 1)
            query = query.Where(x => x.Status == true);
        else if (filter.Status == 2)
            query = query.Where(x => x.Status == false);

        return await query.CountAsync();
    }

    public async Task<List<Superpower>> GetListByFilterAsync(SuperpowerFilter filter)
    {
        var query = _mainContext.TBSuperpowers
                                .AsNoTracking()
                                .Include(c => c.HeroSuperpowers)
                                .AsQueryable();

        query = QueryHelper.ApplyFilter<SuperpowerFilter, Superpower>(query, filter);

        // Agora aplica um comportamento personalizado para o campo "Status"
        if (filter.Status == 1)
            query = query.Where(x => x.Status == true);
        else if (filter.Status == 2)
            query = query.Where(x => x.Status == false);

        query = QueryHelper.ApplySorting(query, filter.OrderBy, filter.SortBy);

        if (filter.CurrentPage > 0)
            query = query.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize);

        return await query.ToListAsync();
    }

    #endregion
}