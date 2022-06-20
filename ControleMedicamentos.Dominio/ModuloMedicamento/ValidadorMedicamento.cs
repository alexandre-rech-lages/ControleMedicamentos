using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3)
                .MaximumLength(60)
                .Matches(new Regex(@"^[a-zA-Z0-9]{4,60}$"))
                .NotEmpty();

            RuleFor(x => x.Descricao)
                 .MinimumLength(3)
                .MaximumLength(80)
                .Matches(new Regex(@"^[ a-zA-Z-à-ü]"))
                .NotEmpty();

            RuleFor(x => x.Lote)
                .MaximumLength(5)
                .Matches(new Regex(@"^[-/a-zA-Z-0-9]"))
                .NotEmpty();

            RuleFor(x => x.Validade)
                .NotEqual(DateTime.MinValue);

            RuleFor(x => x.QuantidadeDisponivel)
                .GreaterThan(0);

            RuleFor(x => x.Fornecedor)
                .NotNull();
        }
    }
}
