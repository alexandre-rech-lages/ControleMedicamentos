using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.Tests.Compartilhado;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTest : BaseUnitTest
    {
        private readonly Funcionario funcionario;
        private readonly ValidadorFuncionario validador;

        public ValidadorFuncionarioTest()
        {
            funcionario = new()
            {
                Nome = "Rech",
                Login = "username.954",
                Senha = "459@password!username"
            };

            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario.Nome = null;

            // action
            var resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            funcionario.Nome = "321Afsdgll@#@GSDLgsfd";

            // action
            var resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Nome);
        }

        [TestMethod]
        public void Login_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario.Login = null;

            // action
            var resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Login);
        }

        [TestMethod]
        public void Login_Deve_Ser_Valido()
        {
            // arrange
            funcionario.Login = "username@678811667!3344a7d41x97d1saj4";

            // action
            var resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Login);
        }

        [TestMethod]
        public void Senha_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario.Senha = null;

            // action
            var resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Senha);

        }

        [TestMethod]
        public void Senha_Deve_Ser_Valida()
        {
            // arrange
            funcionario.Senha = "459@@#!  hfdjd  %#@!@#d!use";

            // action
            var resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Senha);
        }
    }
}
