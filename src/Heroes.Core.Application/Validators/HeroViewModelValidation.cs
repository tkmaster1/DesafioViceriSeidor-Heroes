using Heroes.Common.Util.Validators;
using Heroes.Core.Domain.Entities;

namespace Heroes.Core.Application.Validators;

public class HeroViewModelValidation : BaseValidator<Hero>
{
    public HeroViewModelValidation()
    {
        RuleRequired(x => x.Name, "Name");
        RuleRequired(x => x.HeroName, "HeroName");
        RuleRequired(x => x.BirthDate, "BirthDate");
        RuleRequired(x => x.Height, "Height");
        RuleRequired(x => x.Weight, "Weight");        
    }
}