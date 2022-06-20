using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest : IntegrationTestBase
    {

        private Funcionario funcionario;
        private RepositorioFuncionarioEmBancoDados repositorio;

        public RepositorioFuncionarioEmBancoDadosTest()
        {            
            repositorio = new RepositorioFuncionarioEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_novo_funcionario()
        {
            //action
            funcionario = new Funcionario("Alberto Roberto", "albertoroberto", "P@ssw0rd");
            repositorio.Inserir(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_editar_informacoes_funcionario()
        {
            //arrange
            funcionario = new Funcionario("Alberto Roberto", "albertoroberto", "P@ssw0rd");
            repositorio.Inserir(funcionario);

            //action
            funcionario.Nome = "Pedro Augusto";
            funcionario.Login = "pedro.augusto";
            funcionario.Senha = "P@ssw0rd";

            repositorio.Editar(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            //arrange           
            funcionario = new Funcionario("Alberto Roberto", "albertoroberto", "P@ssw0rd");
            repositorio.Inserir(funcionario);

            //action           
            repositorio.Excluir(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);
            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_funcionario()
        {
            //arrange          
            funcionario = new Funcionario("Alberto Roberto", "albertoroberto", "P@ssw0rd");
            repositorio.Inserir(funcionario);

            //action
            var funcionarioEncontrado = repositorio.SelecionarPorId(funcionario.Id);

            //assert
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_funcionarios()
        {
            //arrange
            var funcionario1 = new Funcionario("Matheus de Souza", "matheus.souza", "P@ssw0rd");
            var funcionario2 = new Funcionario("Camila da Silva", "camila.silva", "P@ssw0rd");
            var funcionario3 = new Funcionario("Joana de Souza", "joana.souza", "P@ssw0rd");

            repositorio.Inserir(funcionario1);
            repositorio.Inserir(funcionario2);
            repositorio.Inserir(funcionario3);

            //action
            var funcionarios = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual(funcionario1.Nome, funcionarios[0].Nome);
            Assert.AreEqual(funcionario2.Nome, funcionarios[1].Nome);
            Assert.AreEqual(funcionario3.Nome, funcionarios[2].Nome);

        }
    }
}
