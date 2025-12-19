namespace Heroes.Common.Util.Messages;

/// <summary>
/// Contém mensagens padrão de validação utilizadas na aplicação.
/// </summary>
public static class ValidationMessages
{
    /// <summary>
    /// Mensagem genérica de campo obrigatório.
    /// </summary> 
    public static string RequiredField(string fieldName)
        => $"O campo {fieldName} é obrigatório.";

    /// <summary>
    /// Mensagem para formatos inválidos (ex: e-mail, CPF).
    /// </summary>
    public static string InvalidFormat(string fieldName) =>
        $"O campo {fieldName} está em um formato inválido.";

    /// <summary>
    /// Mensagem genérica de falha em validação.
    /// </summary>
    public static string ValidationError(string fieldName) =>
        $"Erro de validação no campo {fieldName}.";

    /// <summary>
    /// Mensagem para campos que devem ser únicos (ex: e-mail, usuário).
    /// </summary>
    public static string AlreadyExists(string fieldName) =>
        $"Já existe um registro com o mesmo valor para {fieldName}.";

    /// <summary>
    /// Mensagem de Objeto nulo
    /// </summary>
    public static string MSG_NULL_OBJECT(string fieldName) =>
        $"O objeto {fieldName} está nulo.";

    public static string MSG_PAGESIZE(string fieldName) =>
        $"O tamanho máximo da página permitido é de {fieldName}.";

    public static string MSG_SUCCESSFUL(string fieldName) =>
        $"{fieldName} realizado(a) com sucesso.";

    public static string MSG_FAILED(string fieldName) =>
        $"Não foi possível realizar a/o {fieldName}.";
}