using ConstrutoraDesbravador.Business.Enums;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using FluentValidation;

namespace ConstrutoraDesbravador.Business.Validations
{
    public class ProjetoValidation : AbstractValidator<Projeto>
    {
        public ProjetoValidation(IProjetoRepository repository)
        {
            RuleFor(c => c.Nome)
               .Must(x => !repository.ExisteNomeProjeto(x)).WithMessage("Esse nome de projeto já existe")
               .When(x => x.Id == 0);

            RuleFor(c => c.Nome)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Descricao)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(2, 250).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}