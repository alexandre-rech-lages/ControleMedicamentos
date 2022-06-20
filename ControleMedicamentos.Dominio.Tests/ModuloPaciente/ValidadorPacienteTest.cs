using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.Tests.Compartilhado;
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

            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente.Nome = null;

            // action
            var resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(p => p.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            paciente.Nome = "asass123345";

            // action
            var resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(p => p.Nome);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Obrigatorio()
        {
            // arrange
            paciente.CartaoSUS = null;

            // action
            var resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(p => p.CartaoSUS);
        }

        [TestMethod]
        public void CartaoSUS_Deve_Ser_Valido()
        {
            // arrange
            paciente.CartaoSUS = "1674859GFGF687451450";

            // action
            var resultado = validador.TestValidate(paciente);

            // assert
            resultado.ShouldHaveValidationErrorFor(p => p.CartaoSUS);
        }

    }
}
