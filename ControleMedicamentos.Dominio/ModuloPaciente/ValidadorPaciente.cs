using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
               .Matches(new Regex(@"^([^0-9]*)$"))//.WithMessage("Nome informado é inválido.")
               .NotEmpty(); //.WithMessage("Campo 'Nome' é obrigatório.");

            RuleFor(x => x.CartaoSUS)
                .MaximumLength(15).WithMessage("Cartão SUS informado é inválido.")
                .Matches(new Regex(@"^[0-9]")).WithMessage("Cartão SUS informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Cartão SUS' é obrigatório.");        
        }
    }
}
