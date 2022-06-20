using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.Tests.Compartilhado;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTest : BaseUnitTest
    {
        private readonly Paciente paciente;
        private readonly ValidadorPaciente validador;

        public ValidadorPacienteTest()
        {
            paciente = new()
            {
                Nome = "Rech",
                CartaoSUS = "123456789123456"
            };

            validador = new ValidadorPaciente();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente.Nome = null;

            // action
            var result = validador.TestValidate(paciente);

            // assert
            result.ShouldHaveValidationErrorFor(paciente => paciente.Nome);            
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            paciente.Nome = "Alan@-0g--gkdglsdgpo2";

            // action
            var resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(paciente => paciente.Nome);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente.CartaoSUS = null;

            // action
            var resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(paciente => paciente.CartaoSUS);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Valido()
        {
            // arrange
            paciente.CartaoSUS = "1674859GFGF687451450";

            // action
            var resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(paciente => paciente.CartaoSUS);
        }

    }
}
