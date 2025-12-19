using Heroes.Common.Util.Entities;
using Heroes.Common.Util.Messages;
using Heroes.Core.Application.Validators;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;
using Heroes.Core.Domain.Interfaces.Repositories;
using Heroes.Core.Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Heroes.Core.Application.Services;

public class HeroAppService : IHeroAppService
{
    #region Properties

    private readonly IHeroRepository _heroRepository;

    #endregion

    #region Constructor

    public HeroAppService(IHeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }

    #endregion

    #region Methods Public

    public async Task<Pagination<Hero>> ListByFiltersAsync(HeroFilter filter)
    {
        if (filter == null)
            throw new ValidationException(ValidationMessages.MSG_NULL_OBJECT("Filtro"));

        if (filter.PageSize > 100)
            throw new ValidationException(ValidationMessages.MSG_NULL_OBJECT("100"));

        if (filter.CurrentPage <= 0) filter.PageSize = 1;

        var total = await _heroRepository.CountByFilterAsync(filter);

        if (total == 0) return new Pagination<Hero>();

        var paginateResult = await _heroRepository.GetListByFilterAsync(filter);

        var result = new Pagination<Hero>
        {
            Count = total,
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            Result = paginateResult.ToList()
        };

        return result;
    }

    public async Task<Hero> GetByCodeAsync(int code)
   => await _heroRepository.GetByCodeAsync(code);

    public async Task<int> AddHeroAsync(Hero heroEntity)
    {
        if (heroEntity == null)
            throw new ValidationException(ValidationMessages.MSG_NULL_OBJECT("Hero"));

        Validate(heroEntity);

        heroEntity.DateCreate = DateTime.Now;
        _heroRepository.ToAdd(heroEntity);

        await _heroRepository.ToSaveAsync();

        return heroEntity.Code ?? 0;
    }

    public async Task<bool> EditHeroAsync(Hero heroEntity)
    {
        Validate(heroEntity, true);

        var model = await _heroRepository.GetByCodeAsync(heroEntity.Code??0);

        if (model != null)
        {
            model.Name = heroEntity.Name != model.Name ? heroEntity.Name : model.Name;
            model.HeroName = heroEntity.HeroName != model.HeroName ? heroEntity.HeroName : model.HeroName;
            model.BirthDate = heroEntity.BirthDate != model.BirthDate ? heroEntity.BirthDate : model.BirthDate;
            model.Height = heroEntity.Height != model.Height ? heroEntity.Height : model.Height;
            model.Weight = heroEntity.Weight != model.Weight ? heroEntity.Weight : model.Weight;

            model.DateChange = DateTime.Now;
            model.Status = heroEntity.Status;

            _heroRepository.ToUpdate(model);
        }

        return await _heroRepository.ToSaveAsync() > 0;
    }

    public async Task<bool> DeleteHeroAsync(int code)
    {
        var hero = await _heroRepository.GetByCodeAsync(code);

        if (hero == null)
            throw new ArgumentException("Hero não encontrado.");

        _heroRepository.ToRemove(hero);
        return await _heroRepository.ToSaveAsync() > 0;
    }

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion

    #region Methods Private

    private void Validate(Hero heroEntity, bool update = false)
    {
        var validator = new HeroViewModelValidation();
        var result = validator.Validate(heroEntity);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
                throw new ValidationException(error.ErrorMessage); // ou retorna para o front
        }

        if (update)
            ValidateCode(heroEntity.Code??0);
    }

    private void ValidateCode(int code)
    {
        if (code == 0)
            throw new ValidationException(ValidationMessages.RequiredField("Código"));
    }

    #endregion
}