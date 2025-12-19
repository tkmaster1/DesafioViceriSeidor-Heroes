using Heroes.Common.Util.Validators;
using Heroes.Core.Domain.Entities;

namespace Heroes.Core.Application.Validators;

public class SuperpowerViewModelValidation : BaseValidator<Superpower>
{
    public SuperpowerViewModelValidation()
    {
        RuleRequired(x => x.Description, "Description");
    }
}