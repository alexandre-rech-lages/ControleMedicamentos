using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class MapeadorFornecedor : MapeadorBase<Fornecedor>
    {
        public override void ConfigurarParametros(Fornecedor fornecedor, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", fornecedor.Id);
            comando.Parameters.AddWithValue("NOME", fornecedor.Nome);
            comando.Parameters.AddWithValue("TELEFONE", fornecedor.Telefone);
            comando.Parameters.AddWithValue("EMAIL", fornecedor.Email);
            comando.Parameters.AddWithValue("CIDADE", fornecedor.Cidade);
            comando.Parameters.AddWithValue("ESTADO ", fornecedor.Estado);
        }
        public override Fornecedor ConverterRegistro(SqlDataReader leitorFornecedor)
        {
            var id = Convert.ToInt32(leitorFornecedor["ID"]);
            var nome = Convert.ToString(leitorFornecedor["NOME"]);
            var telefone = Convert.ToString(leitorFornecedor["TELEFONE"]);
            var email = Convert.ToString(leitorFornecedor["EMAIL"]);
            var cidade = Convert.ToString(leitorFornecedor["CIDADE"]);
            var estado = Convert.ToString(leitorFornecedor["ESTADO"]);

            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Id = id;
            fornecedor.Nome = nome;
            fornecedor.Telefone = telefone;
            fornecedor.Email = email;
            fornecedor.Cidade = cidade;
            fornecedor.Estado = estado;

            return fornecedor;
        }
    }
}
