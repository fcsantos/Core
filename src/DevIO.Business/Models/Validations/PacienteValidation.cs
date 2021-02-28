using FluentValidation;
using System;

namespace MRP.Business.Models.Validations
{
    public class PacienteValidation : AbstractValidator<Paciente>
    {
        public PacienteValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.Apelido)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.NumeroUtente)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(9)
                .WithMessage("O campo {PropertyName} precisa ter {exactLength} caracteres");

            RuleFor(p => p.NumeroIdentificacao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(9)
                .WithMessage("O campo {PropertyName} precisa ter {exactLength} caracteres");

            RuleFor(p => p.Email).EmailAddress();

            RuleFor(p => p.DataNascimento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .LessThan(p => DateTime.Now).WithMessage("A data de nascimento não pode ser maior que a data atual");

            RuleFor(p => p.Telemovel)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(9)
                .WithMessage("O campo {PropertyName} precisa ter {exactLength} caracteres");
        }
    }
}
