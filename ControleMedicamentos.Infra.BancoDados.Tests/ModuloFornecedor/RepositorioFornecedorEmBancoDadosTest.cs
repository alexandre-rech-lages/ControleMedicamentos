using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest : IntegrationTestBase
    {
        private RepositorioFornecedorEmBancoDados repositorio;

        public RepositorioFornecedorEmBancoDadosTest()
        {            
            repositorio = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_novo_fornecedor()
        {
            //arrange
            var fornecedor = NovoFornecedor();

            //action
            repositorio.Inserir(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            fornecedorEncontrado.Should().NotBeNull();
            fornecedorEncontrado.Should().Be(fornecedor);
        }
       
        [TestMethod]
        public void Deve_editar_informacoes_fornecedor()
        {
            //arrange
            var fornecedor = NovoFornecedor();
            repositorio.Inserir(fornecedor);

            fornecedor.Nome = "Hera Medicamentos";
            fornecedor.Telefone = "49999292107";
            fornecedor.Email = "contato@heramedicamentos.com";
            fornecedor.Cidade = "Florianopolis";
            fornecedor.Estado = "SC";

            //action
            repositorio.Editar(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            fornecedorEncontrado.Should().NotBeNull();
            fornecedorEncontrado.Should().Be(fornecedor);
        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            //arrange           
            var fornecedor = NovoFornecedor();
            repositorio.Inserir(fornecedor);

            //action           
            repositorio.Excluir(fornecedor);

            //assert
            repositorio.SelecionarPorId(fornecedor.Id)
                .Should().BeNull();
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_fornecedor()
        {
            //arrange          
            var fornecedor = NovoFornecedor();
            repositorio.Inserir(fornecedor);

            //action
            var fornecedorEncontrado = repositorio.SelecionarPorId(fornecedor.Id);

            //assert
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_fornecedores()
        {
            //arrange
            var f0 = new Fornecedor("Althis", "49999292101", "althis@gmail.com", "Lages", "SC");
            var f1 = new Fornecedor("Althermed", "49999292102", "altermed@gmail.com", "Lages", "SC");
            var f2 = new Fornecedor("Riomed", "49999292103", "riomed@gmail.com", "Lages", "SC");

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(f0);
            repositorio.Inserir(f1);
            repositorio.Inserir(f2);

            //action
            var fornecedores = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, fornecedores.Count);

            Assert.AreEqual(f0.Nome, fornecedores[0].Nome);
            Assert.AreEqual(f1.Nome, fornecedores[1].Nome);
            Assert.AreEqual(f2.Nome, fornecedores[2].Nome);
        }


        private Fornecedor NovoFornecedor()
        {
            return new Fornecedor("Grupo Dimed", "49998165491", "contato@grupodimed.com.br", "Curitiba", "PR");
        }
    }
}
