using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3)
                .MaximumLength(50)
                .Matches(new Regex(@"^([^0-9]*)$"))
                .NotEmpty();

            RuleFor(x => x.Login)
                 .MinimumLength(3)
                .MaximumLength(20)
                .Matches(new Regex(@"^[.@a-zA-Z-à-ü0-9]"))
                .NotEmpty();

            RuleFor(x => x.Senha)
                .MinimumLength(6)
                .MaximumLength(8)
                .Matches(new Regex(@"^[.!@#$%&*a-zA-Z-à-ü0-9]"))
                .NotEmpty();
        }
    }
}
