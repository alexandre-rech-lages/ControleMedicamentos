using FluentValidation;
using System;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Funcionario)
                .NotNull();

            RuleFor(x => x.Paciente)
                .NotNull();

            When(r => r.Medicamento == null, () =>
            {
                RuleFor(x => x.Medicamento)
                    .NotNull();

            }).Otherwise(() =>

            {
                RuleFor(x => x.QuantidadeMedicamento)
                    .GreaterThan(0)
                    .LessThan(x => x.Medicamento.QuantidadeDisponivel);
            });

            RuleFor(x => x.Data)
                .NotEqual(DateTime.MinValue);
        }
    }
}
