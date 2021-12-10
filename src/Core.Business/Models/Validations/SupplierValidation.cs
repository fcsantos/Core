using Core.Business.Models.Validations.Documents;
using FluentValidation;

namespace Core.Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    { 
        public SupplierValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.TypeSupplier == TypeSupplier.PessoaFisica, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CpfValidation.SizeCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f=> CpfValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            When(f => f.TypeSupplier == TypeSupplier.PessoaJuridica, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CnpjValidation.SizeCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}