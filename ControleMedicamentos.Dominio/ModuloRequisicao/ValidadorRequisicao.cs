using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            When(x => x.Medicamento == null, () =>
            {
                RuleFor(x => x.Medicamento)
                    .NotNull().NotEmpty();

            }).Otherwise(() =>
            {
                RuleFor(x => x.QtdMedicamento)
                    .LessThanOrEqualTo(x => x.Medicamento.QuantidadeDisponivel);
            });
            
        }
    }
}
