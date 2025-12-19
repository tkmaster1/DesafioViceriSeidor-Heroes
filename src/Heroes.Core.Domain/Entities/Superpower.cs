using Heroes.Common.Util.Entities;

namespace Heroes.Core.Domain.Entities;

public class Superpower: Entity
{
    /// <summary>
    /// Descrição do superpoder
    /// </summary>
    public string Description { get; set; } = null!;

    public ICollection<HeroSuperpower> HeroSuperpowers { get; set; }
        = new List<HeroSuperpower>();
}