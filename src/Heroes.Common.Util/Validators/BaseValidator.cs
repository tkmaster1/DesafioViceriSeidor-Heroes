using FluentValidation;
using Heroes.Common.Util.Messages;
using System.Linq.Expressions;

namespace Heroes.Common.Util.Validators;

/// <summary>
/// Classe base genérica para validadores FluentValidation, com métodos auxiliares reutilizáveis.
/// </summary>
/// <typeparam name="T">Tipo da classe a ser validada.</typeparam>
public abstract class BaseValidator<T> : AbstractValidator<T>
{
    /// <summary>
    /// Aplica uma regra de campo obrigatório com mensagem personalizada.
    /// </summary>
    /// <typeparam name="TProperty">Tipo da propriedade.</typeparam>
    /// <param name="expression">Expressão da propriedade a ser validada.</param>
    /// <param name="fieldName">Nome do campo para compor a mensagem.</param>
    protected void RuleRequired<TProperty>(Expression<Func<T, TProperty>> expression, string fieldName)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage(ValidationMessages.RequiredField(fieldName));
    }
}
