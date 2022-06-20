using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.Tests.Compartilhado;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTest : BaseUnitTest
    {
        private readonly Fornecedor fornecedor;
        private readonly ValidadorFornecedor validador;

        public ValidadorFornecedorTest()
        {
            fornecedor = new()
            {
                Nome = "Alexandre Rech",
                Telefone = "49998165491",
                Email = "Rech@email.com",
                Cidade = "Lages",
                Estado = "SC"
            };

            validador = new ValidadorFornecedor();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Nome = null;

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            fornecedor.Nome = "321Rech_Fernandes";

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Nome);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Telefone = null;

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Telefone);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Valido()
        {
            // arrange
            fornecedor.Telefone = "qwertyuiopa";

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Telefone);
        }

        [TestMethod]
        public void Email_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Email = null;

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Email);
        }

        [TestMethod]
        public void Email_Deve_Ser_Valido()
        {
            // arrange
            fornecedor.Email = "Rechemail.com";

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Email);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Cidade = null;

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Cidade);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Valida()
        {
            // arrange
            fornecedor.Cidade = "_###Lag?es";

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Cidade);
        }

        [TestMethod]
        public void Estado_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Estado = null;

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Estado);
        }

        [TestMethod]
        public void Estado_Deve_Ser_Valido()
        {
            // arrange
            fornecedor.Estado = "XX";

            // action
            var resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(f => f.Estado);
        }
    }
}
