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

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest : BaseTest
    {
        private Requisicao requisicao;
        private Medicamento medicamento;
        private Fornecedor fornecedor;
        private Funcionario funcionario;
        private Paciente paciente;

        private RepositorioRequisicaoEmBancoDados repositorioRequisicao;
        private RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;
        private RepositorioFuncionarioEmBancoDados repositorioFuncionario;
        private RepositorioPacienteEmBancoDados repositorioPaciente;

        public RepositorioRequisicaoEmBancoDadosTest()
        {
            medicamento = gerarMedicamento();
            fornecedor = gerarFornecedor();
            paciente = gerarPaciente();
            funcionario = gerarFuncionario();
            requisicao = gerarRequisicao();

            medicamento.Fornecedor = fornecedor;
            requisicao.Medicamento = medicamento;
            requisicao.Funcionario = funcionario;
            requisicao.Paciente = paciente;

            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
        }

        public Requisicao gerarRequisicao()
        {
            Requisicao requisicao = new Requisicao();
            requisicao.Data = new DateTime(2022, 01, 09, 09, 15, 00);
            requisicao.QuantidadeMedicamento = 2;

            return requisicao;
        }

        public Medicamento gerarMedicamento()
        {
            Medicamento medicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Analgésico",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
            };

            return medicamento;
        }

        public Fornecedor gerarFornecedor()
        {
            fornecedor = new()
            {
                Nome = "Rech",
                Telefone = "49998165491",
                Email = "Rech@email.com",
                Cidade = "Lages",
                Estado = "SC"
            };

            return fornecedor;
        }

        public Paciente gerarPaciente()
        {
            Paciente paciente = new()
            {
                Nome = "Rech",
                CartaoSUS = "123456789123456"
            };

            return paciente;
        }

        public Funcionario gerarFuncionario()
        {
            Funcionario funcionario = new()
            {
                Nome = "Rech",
                Login = "username.954",
                Senha = "Password"
            };

            return funcionario;
        }

        [TestMethod]
        public void Deve_inserir_nova_requisicao()
        {
            //action
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioRequisicao.Inserir(requisicao);

            //assert
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_editar_informacoes_requisicao()
        {
            //arrange                      
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioRequisicao.Inserir(requisicao);

            //action
            requisicao.QuantidadeMedicamento = 5;
            repositorioRequisicao.Editar(requisicao);

            //assert
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            //arrange           
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioRequisicao.Inserir(requisicao);

            //action           
            repositorioRequisicao.Excluir(requisicao);

            //assert
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);
            Assert.IsNull(requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_uma_requisicao()
        {
            //arrange          
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioRequisicao.Inserir(requisicao);

            //action
            var requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_todos_as_requisicoes()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            var requisicao1 = new Requisicao(medicamento, paciente, 3, new DateTime(2022, 01, 09, 09, 15, 00), funcionario);
            var requisicao2 = new Requisicao(medicamento, paciente, 7, new DateTime(2022, 03, 09, 09, 15, 00), funcionario);

            repositorioRequisicao.Inserir(requisicao1);
            repositorioRequisicao.Inserir(requisicao2);

            //action
            var requisicoes = repositorioRequisicao.SelecionarTodos();

            //assert

            Assert.AreEqual(2, requisicoes.Count);

            Assert.AreEqual(requisicao1.Paciente.Nome, requisicoes[0].Paciente.Nome);
            Assert.AreEqual(requisicao2.Paciente.Nome, requisicoes[1].Paciente.Nome);

        }
    }
}