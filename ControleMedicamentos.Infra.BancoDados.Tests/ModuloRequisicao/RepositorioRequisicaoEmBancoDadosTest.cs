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
    public class RepositorioRequisicaoEmBancoDadosTest : IntegrationTestBase
    {
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
            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            funcionario = new Funcionario();
            funcionario.Nome = "Carlos Alberto";
            funcionario.Login = "username.954";
            funcionario.Senha = "P@ssw0rd";
            repositorioFuncionario.Inserir(funcionario);

            paciente = new Paciente();
            paciente.Nome = "Alexandre Rech";
            paciente.CartaoSUS = "123456789123456";
            repositorioPaciente.Inserir(paciente);

            fornecedor = new Fornecedor();
            fornecedor.Nome = "Grupo Dimed";
            fornecedor.Email = "contato@grupodimed.com.br";
            fornecedor.Telefone = "49999292107";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";
            repositorioFornecedor.Inserir(fornecedor);

            medicamento = new Medicamento();
            medicamento.Nome = "Paracetamol";
            medicamento.Descricao = "Analgésico";
            medicamento.Lote = "P-001";
            medicamento.Validade = new DateTime(2022, 8, 20);
            medicamento.QuantidadeDisponivel = 50;
            medicamento.Fornecedor = fornecedor;
            repositorioMedicamento.Inserir(medicamento);                       
        }
    
        [TestMethod]
        public void Deve_inserir_nova_requisicao()
        {
            //arrange
            Requisicao requisicao = new Requisicao(medicamento, paciente, 2, new DateTime(2022, 01, 09, 09, 15, 00), funcionario);

            //action                                                                        
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
            Requisicao requisicao = new Requisicao(medicamento,paciente,2, new DateTime(2022, 01, 09, 09, 15, 00),funcionario);
            requisicao.QuantidadeMedicamento = 5;
            repositorioRequisicao.Inserir(requisicao);

            //action
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
            Requisicao requisicao = new Requisicao(medicamento, paciente, 2, new DateTime(2022, 01, 09, 09, 15, 00), funcionario);
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
            Requisicao requisicao = new Requisicao(medicamento, paciente, 2, new DateTime(2022, 01, 09, 09, 15, 00), funcionario);
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