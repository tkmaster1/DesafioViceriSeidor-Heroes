using Heroes.Common.Util.Entities;

namespace Heroes.Core.Domain.Entities;

public class HeroSuperpower: Entity
{
    public int CodeHero { get; set; }

    public Hero Hero { get; set; } = null!;

    public int CodeSuperpower { get; set; }

    public Superpower Superpower { get; set; } = null!;
}
