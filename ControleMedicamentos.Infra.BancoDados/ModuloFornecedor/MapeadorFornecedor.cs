using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class MapeadorFornecedor : MapeadorBase<Fornecedor>
    {
        public override void ConfigurarParametros(Fornecedor registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("TELEFONE", registro.Telefone);
            comando.Parameters.AddWithValue("EMAIL", registro.Email);
            comando.Parameters.AddWithValue("CIDADE", registro.Cidade);
            comando.Parameters.AddWithValue("ESTADO ", registro.Estado);
        }

        public override Fornecedor ConverterRegistro(SqlDataReader leitorRegistro)
        {
            if (leitorRegistro["FORNECEDOR_ID"] == DBNull.Value)
                return null;

            var id = Convert.ToInt32(leitorRegistro["FORNECEDOR_ID"]);
            var nome = Convert.ToString(leitorRegistro["FORNECEDOR_NOME"]);
            var telefone = Convert.ToString(leitorRegistro["FORNECEDOR_TELEFONE"]);
            var email = Convert.ToString(leitorRegistro["FORNECEDOR_EMAIL"]);
            var cidade = Convert.ToString(leitorRegistro["FORNECEDOR_CIDADE"]);
            var estado = Convert.ToString(leitorRegistro["FORNECEDOR_ESTADO"]);

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
