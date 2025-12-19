namespace Heroes.Core.Application.DTOs.Request;

public class SuperpowerRequestDTO
{
    /// <summary>
    /// Código
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// Descrição do Superpoder
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Indica se o superpoder está Ativo (1) ou Inativo (0)
    /// </summary>
    public bool Status { get; set; }
}