using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                .Matches(new Regex(@"^([^0-9]*)$"))
                .MinimumLength(3)
                .MaximumLength(60)
                .NotEmpty();

            RuleFor(x => x.CartaoSUS)
                .MaximumLength(15)
                .Matches(new Regex(@"^[0-9]"))
                .NotEmpty();
        }
    }
}
