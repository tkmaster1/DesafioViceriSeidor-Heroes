using Heroes.Common.Util.Entities;
using Heroes.Common.Util.Messages;
using Heroes.Core.Application.Validators;
using Heroes.Core.Domain.Entities;
using Heroes.Core.Domain.Filters;
using Heroes.Core.Domain.Interfaces.Repositories;
using Heroes.Core.Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Heroes.Core.Application.Services;

public class SuperpowerAppService : ISuperpowerAppService
{
    #region Properties

    private readonly ISuperpowerRepository _superpowerRepository;

    #endregion

    #region Constructor

    public SuperpowerAppService(ISuperpowerRepository superpowerRepository)
    {
        _superpowerRepository = superpowerRepository;
    }

    #endregion

    #region Methods Public

    public async Task<Pagination<Superpower>> ListByFiltersAsync(SuperpowerFilter filter)
    {
        if (filter == null)
            throw new ValidationException(ValidationMessages.MSG_NULL_OBJECT("Filtro"));

        if (filter.PageSize > 100)
            throw new ValidationException(ValidationMessages.MSG_NULL_OBJECT("100"));

        if (filter.CurrentPage <= 0) filter.PageSize = 1;

        var total = await _superpowerRepository.CountByFilterAsync(filter);

        if (total == 0) return new Pagination<Superpower>();

        var paginateResult = await _superpowerRepository.GetListByFilterAsync(filter);

        var result = new Pagination<Superpower>
        {
            Count = total,
            CurrentPage = filter.CurrentPage,
            PageSize = filter.PageSize,
            Result = paginateResult.ToList()
        };

        return result;
    }

    public async Task<Superpower> GetByCodeAsync(int code)
   => await _superpowerRepository.GetByCodeAsync(code);

    public async Task<int> AddSuperpowerAsync(Superpower entity)
    {
        if (entity == null)
            throw new ValidationException(ValidationMessages.MSG_NULL_OBJECT("Superpower"));

        Validate(entity);

        entity.DateCreate = DateTime.Now;
        _superpowerRepository.ToAdd(entity);

        await _superpowerRepository.ToSaveAsync();

        return entity.Code ?? 0;
    }

    public async Task<bool> EditSuperpowerAsync(Superpower entity)
    {
        Validate(entity, true);

        var model = await _superpowerRepository.GetByCodeAsync(entity.Code??0);

        if (model != null)
        {
            model.Description = entity.Description != model.Description ? entity.Description : model.Description;

            model.DateChange = DateTime.Now;
            model.Status = entity.Status;

            _superpowerRepository.ToUpdate(model);
        }

        return await _superpowerRepository.ToSaveAsync() > 0;
    }

    public async Task<bool> DeleteSuperpowerAsync(int code)
    {
        var hero = await _superpowerRepository.GetByCodeAsync(code);

        if (hero  == null)
            throw new ArgumentException("Superpower não encontrado.");

        _superpowerRepository.ToRemove(hero);
        return await _superpowerRepository.ToSaveAsync() > 0;
    }

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion

    #region Methods Private

    private void Validate(Superpower entity, bool update = false)
    {
        var validator = new SuperpowerViewModelValidation();
        var result = validator.Validate(entity);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
                throw new ValidationException(error.ErrorMessage); // ou retorna para o front
        }

        if (update)
            ValidateCode(entity.Code??0);
    }

    private void ValidateCode(int code)
    {
        if (code == 0)
            throw new ValidationException(ValidationMessages.RequiredField("Código"));
    }

    #endregion
}