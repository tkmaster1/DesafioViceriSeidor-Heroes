using Heroes.Common.Util.Entities;

namespace Heroes.Core.Domain.Entities;

public class Hero : Entity
{
    /// <summary>
    /// Nome do herói (único)
    /// </summary>
    public string HeroName { get; set; } = null!;

    /// <summary>
    /// Data de Nascimento
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Altura
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Peso
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// (lista de um ou mais superpoderes) 
    /// </summary>
    public ICollection<HeroSuperpower> HeroSuperpowers { get; set; }
        = new List<HeroSuperpower>();
}