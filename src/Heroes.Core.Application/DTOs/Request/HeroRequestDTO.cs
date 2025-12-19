namespace Heroes.Core.Application.DTOs.Request;

public class HeroRequestDTO
{
    /// <summary>
    /// Código
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// Nome real do herói
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Nome do herói (único)
    /// </summary>
    public string HeroName { get; set; }

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
    /// Indica se o super-heróis está Ativo (1) ou Inativo (0)
    /// </summary>
    public bool Status { get; set; }
}