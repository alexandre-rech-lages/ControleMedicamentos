using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Dominio.Tests.Compartilhado;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class ValidadorRequisicaoTest : BaseUnitTest
    {
        private readonly Funcionario funcionario;
        private readonly Paciente paciente;
        private readonly Fornecedor fornecedor;
        private readonly Medicamento medicamento;
        private readonly Requisicao requisicao;

        private readonly ValidadorRequisicao validador;

        public ValidadorRequisicaoTest()
        {
            funcionario = new()
            {
                Nome = "Rech",
                Login = "username.954",
                Senha = "459@password!username"
            };

            paciente = new()
            {
                Nome = "Rech",
                CartaoSUS = "123456789123456"
            };

            fornecedor = new()
            {
                Nome = "Rech",
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

            requisicao = new()
            {
                Funcionario = funcionario,
                Paciente = paciente,
                Medicamento = medicamento,
                QuantidadeMedicamento = 10,
                Data = DateTime.Now.Date
            };

            validador = new();
        }

        [TestMethod]
        public void Funcionario_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao.Funcionario = null;

            // action
            var resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(r => r.Funcionario);
        }

        [TestMethod]
        public void Paciente_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao.Paciente = null;

            // action
            var resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(r => r.Paciente);
        }

        [TestMethod]
        public void Medicamento_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao.Medicamento = null;

            // action
            var resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(r => r.Medicamento);
        }

        [TestMethod]
        public void Quantidade_Medicamento_Deve_Ser_Valida()
        {
            // arrange 
            requisicao.QuantidadeMedicamento = 0;

            // action
            var resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(r => r.QuantidadeMedicamento);
        }

        [TestMethod]
        public void Quantidade_Medicamento_Deve_Ser_Menor_Ou_Igual_Que_Quantidade_Disponivel()
        {
            // arrange 
            medicamento.QuantidadeDisponivel = 10;
            requisicao.QuantidadeMedicamento = 20;

            // action
            var resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(r => r.QuantidadeMedicamento);
        }

        [TestMethod]
        public void Data_Requisicao_Deve_Ser_Valida()
        {
            // arrange 
            requisicao.Data = DateTime.MinValue;

            // action
            var resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(r => r.Data);
        }
    }
}
