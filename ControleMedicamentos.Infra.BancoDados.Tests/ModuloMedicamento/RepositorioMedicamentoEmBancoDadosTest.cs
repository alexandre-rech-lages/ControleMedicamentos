using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{

    [TestClass]
    public class RepositorioMedicamentoEmBancoDeDadosTest : BaseTest
    {
        private Fornecedor fornecedor;
        private Medicamento medicamento;

        private RepositorioMedicamentoEmBancoDados repositorioMedicamento;        
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;

        public RepositorioMedicamentoEmBancoDeDadosTest()
        {
            medicamento = new Medicamento();
            medicamento.Nome = "Doril";
            medicamento.Descricao = "Tomou doril a dor sumiu.";
            medicamento.Lote = "231AS1";
            medicamento.Validade = new DateTime(2022, 01, 09, 09, 15, 00);
            medicamento.QuantidadeDisponivel = 10;

            fornecedor = new Fornecedor();
            fornecedor.Nome = "Rogerio";
            fornecedor.Email = "RogerinDoYoutube@gmail.com";
            fornecedor.Telefone = "4002-8922";
            fornecedor.Cidade = "Lages";
            fornecedor.Estado = "SC";

            medicamento.Fornecedor = fornecedor;

            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
        }     
     

        [TestMethod]
        public void Deve_inserir_novo_medicamento()
        {
            //action
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            //assert
            var medicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_editar_informacoes_medicamento()
        {
            //arrange                      
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            //action
            medicamento.Nome = "Tylenol";
            medicamento.Descricao = "Contra febre";
            repositorioMedicamento.Editar(medicamento);

            //assert
            var medicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_medicamento()
        {
            //arrange           
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            //action           
            repositorioMedicamento.Excluir(medicamento);

            //assert
            var medicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);
            Assert.IsNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_medicamento()
        {
            //arrange          
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            //action
            var medicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            //assert
            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);
            var medicamento1 = new Medicamento("Neosaldina", "Contra dor de cabeça", "123SAD", new DateTime(2022, 01, 09, 09, 15, 00), 2, fornecedor);
            var medicamento2 = new Medicamento("Dipirona", "Contra dor", "1323AS", new DateTime(2022, 01, 09, 09, 15, 00), 3, fornecedor);

            repositorioMedicamento.Inserir(medicamento1);
            repositorioMedicamento.Inserir(medicamento2);

            //action
            var medicamentos = repositorioMedicamento.SelecionarTodos();

            //assert

            Assert.AreEqual(2, medicamentos.Count);

            Assert.AreEqual(medicamento1.Nome, medicamentos[0].Nome);
            Assert.AreEqual(medicamento2.Nome, medicamentos[1].Nome);

        }
    }
}

