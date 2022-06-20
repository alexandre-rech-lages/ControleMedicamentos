using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
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
        private Paciente paciente;
        private Funcionario funcionario;

        private RepositorioRequisicaoEmBancoDados repositorioRequisicao;
        private RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;
        private RepositorioFuncionarioEmBancoDados repositorioFuncionario;
        private RepositorioPacienteEmBancoDados repositorioPaciente;

        public RepositorioMedicamentoEmBancoDeDadosTest()
        {
            funcionario = new()
            {
                Nome = "Rech",
                Login = "username.954",
                Senha = "P@ssw0rd"
            };

            paciente = new Paciente();
            paciente.Nome = "Rech";
            paciente.CartaoSUS = "123456789123456";

            medicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Analgésico",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = fornecedor
            };

            fornecedor = new()
            {
                Nome = "Rech",
                Telefone = "49998165491",
                Email = "Rech@email.com",
                Cidade = "Lages",
                Estado = "SC"
            };

            medicamento.Fornecedor = fornecedor;

            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
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
            Fornecedor fornecedor2 = new()
            {
                Nome = "James",
                Telefone = "11984675506",
                Email = "james@email.com",
                Cidade = "Guarulhos",
                Estado = "SP"
            };

            Medicamento medicamento2 = new()
            {
                Nome = "Nimesulida",
                Descricao = "Anti-Inflamatório",
                Lote = "N-001",
                Validade = new DateTime(2025, 5, 15),
                QuantidadeDisponivel = 100,
                Fornecedor = fornecedor
            };

            repositorioFornecedor.Inserir(fornecedor);
            repositorioFornecedor.Inserir(fornecedor2);

            repositorioMedicamento.Inserir(medicamento);
            repositorioMedicamento.Inserir(medicamento2);

            // action
            var medicamentosEncontrados = repositorioMedicamento.SelecionarTodos();

            // assert
            Assert.AreEqual(2, medicamentosEncontrados.Count);

            Assert.AreEqual("Paracetamol", medicamentosEncontrados[0].Nome);
            Assert.AreEqual("Nimesulida", medicamentosEncontrados[1].Nome);

        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos_com_requisicoes()
        {
            //arrange
            repositorioPaciente.Inserir(paciente);

            repositorioFuncionario.Inserir(funcionario);

            repositorioFornecedor.Inserir(fornecedor);

            var medicamento1 = new Medicamento("Neosaldina", "Contra dor de cabeça", "P-001", new DateTime(2022, 01, 09, 09, 15, 00), 2, fornecedor);
            var medicamento2 = new Medicamento("Dipirona", "Contra dor", "P-001", new DateTime(2022, 01, 09, 09, 15, 00), 3, fornecedor);

            repositorioMedicamento.Inserir(medicamento1);
            repositorioMedicamento.Inserir(medicamento2);

            var requisicao1 = new Requisicao(medicamento1, paciente, 1, DateTime.Now, funcionario);
            var requisicao2 = new Requisicao(medicamento2, paciente, 2, DateTime.Now, funcionario);

            repositorioRequisicao.Inserir(requisicao1);
            repositorioRequisicao.Inserir(requisicao2);

            //action
            var medicamentos = repositorioMedicamento.SelecionarMedicamentosComRequisicoes();

            //assert
            Assert.AreEqual(2, medicamentos.Count);

            Assert.AreEqual(1, medicamentos[0].QuantidadeRequisicoes);
            Assert.AreEqual(1, medicamentos[1].QuantidadeRequisicoes);

        }
    }
}

