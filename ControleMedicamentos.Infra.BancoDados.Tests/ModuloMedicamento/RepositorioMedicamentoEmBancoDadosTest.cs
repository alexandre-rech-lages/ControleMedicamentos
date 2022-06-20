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
    public class RepositorioMedicamentoEmBancoDeDadosTest : IntegrationTestBase
    {
        private Fornecedor fornecedor;
        private Paciente paciente;
        private Funcionario funcionario;

        private RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;
        public RepositorioMedicamentoEmBancoDeDadosTest()
        {
            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();           

            fornecedor = new Fornecedor();
            fornecedor.Nome = "Grupo Dimed";
            fornecedor.Email = "contato@grupodimed.com.br";
            fornecedor.Telefone = "49999292107";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";
            repositorioFornecedor.Inserir(fornecedor);
        }


        [TestMethod]
        public void Deve_inserir_novo_medicamento()
        {
            //arrange
            var medicamento = new Medicamento("Paracetamol", "Analgésico", "P-001", new DateTime(2022, 8, 20), 50, fornecedor);

            //action            
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
            var medicamento = new Medicamento("Paracetamol", "Analgésico", "P-001", new DateTime(2022, 8, 20), 50, fornecedor);
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
            var medicamento = new Medicamento("Paracetamol", "Analgésico", "P-001", new DateTime(2022, 8, 20), 50, fornecedor);
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
            var medicamento = new Medicamento("Paracetamol", "Analgésico", "P-001", new DateTime(2022, 8, 20), 50, fornecedor);
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
            var medicamento = new Medicamento("Paracetamol", "Analgésico", "P-001", new DateTime(2022, 8, 20), 50, fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            var outroFornecedor = new Fornecedor("Flavio Augusto", "11984675506", "Flavio@email.com", "Guarulhos", "SP");
            repositorioFornecedor.Inserir(outroFornecedor);

            var outroMedicamento = new Medicamento("Nimesulida", "Anti-Inflamatório", "N-001", new DateTime(2025, 5, 15), 100, outroFornecedor);            
            repositorioMedicamento.Inserir(outroMedicamento);

            // action
            var medicamentosEncontrados = repositorioMedicamento.SelecionarTodos();

            // assert
            Assert.AreEqual(2, medicamentosEncontrados.Count);

            Assert.AreEqual(medicamento, medicamentosEncontrados[0]);
            Assert.AreEqual(outroMedicamento, medicamentosEncontrados[1]);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_medicamentos_com_requisicoes()
        {
            //arrange
            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            var repositorioPaciente = new RepositorioPacienteEmBancoDados();
            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            funcionario = new Funcionario();
            funcionario.Nome = "Almir Sater";
            funcionario.Login = "username.954";
            funcionario.Senha = "P@ssw0rd";

            paciente = new Paciente();
            paciente.Nome = "Alexandre Rech";
            paciente.CartaoSUS = "123456789123456";
            repositorioFornecedor.Inserir(fornecedor);

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

