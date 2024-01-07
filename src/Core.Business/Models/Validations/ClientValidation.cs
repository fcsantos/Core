using FluentValidation;

namespace Core.Business.Models.Validations
{
    public class ClientValidation : AbstractValidator<Client>
    {
        public ClientValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.NIF)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 14).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 250).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
