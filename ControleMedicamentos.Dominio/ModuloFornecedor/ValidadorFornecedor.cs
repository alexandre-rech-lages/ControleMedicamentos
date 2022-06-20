using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3)
                .MaximumLength(60)
                .Matches(new Regex(@"^([^0-9]*)$"))
                .NotEmpty();

            RuleFor(x => x.Telefone)
                .Matches(new Regex(@"^\d{2}\d{4,5}\d{4}$"))
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Cidade)
                .MinimumLength(3)
                .MaximumLength(60)
                .Matches(new Regex(@"^[a-zA-Z0-9]{4,50}$"))
                .NotEmpty();

            RuleFor(x => x.Estado)
                .Matches(new Regex(@"^(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)$"))
                .NotEmpty();
        }
    }
}
