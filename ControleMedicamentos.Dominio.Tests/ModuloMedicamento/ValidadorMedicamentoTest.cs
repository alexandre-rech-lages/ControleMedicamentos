using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.Tests.Compartilhado;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTest : BaseUnitTest
    {
        private readonly Fornecedor fornecedor;
        private readonly Medicamento medicamento;
        private readonly ValidadorMedicamento validador;
        public ValidadorMedicamentoTest()
        {
            fornecedor = new()
            {
                Nome = "Alexandre Rech",
                Telefone = "49998165491",
                Email = "Rech@email.com",
                Cidade = "Lages",
                Estado = "SC"
            };

            medicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Analgésico",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = fornecedor
            };

            validador = new ValidadorMedicamento();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Nome = null;

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            medicamento.Nome = "x_";

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Nome);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Descricao = null;

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Descricao);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Valido()
        {
            // arrange
            medicamento.Descricao = "2%@%_+ _ 2%@%!@ @__ @";

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Descricao);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Lote = null;

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Lote);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Valido()
        {
            // arrange
            medicamento.Lote = "  $@% _a47";

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Lote);
        }

        [TestMethod]
        public void Validade_Deve_Ser_Valida()
        {
            // arrange
            medicamento.Validade = DateTime.MinValue;

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Validade);
        }

        [TestMethod]
        public void QuantidadeDisponivel_Deve_Ser_Valida()
        {
            // arrange
            medicamento.QuantidadeDisponivel = -1;

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.QuantidadeDisponivel);
        }

        [TestMethod]
        public void Fornecedor_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Fornecedor = null;

            // action
            var resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(m => m.Fornecedor);
        }
    }
}
