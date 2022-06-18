using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloMedicamento
{
    public class MapeadorMedicamento : MapeadorBase<Medicamento>
    {      
        public override void ConfigurarParametros(Medicamento registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", registro.Descricao);
            comando.Parameters.AddWithValue("LOTE", registro.Lote);
            comando.Parameters.AddWithValue("VALIDADE", registro.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", registro.QuantidadeDisponivel);

            comando.Parameters.AddWithValue("FORNECEDOR_ID", registro.Fornecedor.Id);
        }

        public override Medicamento ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Convert.ToInt32(leitorRegistro["MEDICAMENTO_ID"]);
            var nome = Convert.ToString(leitorRegistro["MEDICAMENTO_NOME"]);
            var descricao = Convert.ToString(leitorRegistro["MEDICAMENTO_DESCRICAO"]);
            var lote = Convert.ToString(leitorRegistro["MEDICAMENTO_LOTE"]);
            var validade = Convert.ToDateTime(leitorRegistro["MEDICAMENTO_VALIDADE"]);
            var quantidadeDisponivel = Convert.ToInt32(leitorRegistro["MEDICAMENTO_QUANTIDADEDISPONIVEL"]);

            Medicamento medicamento = new Medicamento();
            medicamento.Id = id;
            medicamento.Nome = nome;
            medicamento.Descricao = descricao;
            medicamento.Lote = lote;
            medicamento.Validade = validade;
            medicamento.QuantidadeDisponivel = quantidadeDisponivel;

            var fornecedor = new MapeadorFornecedor().ConverterRegistro(leitorRegistro);

            medicamento.Fornecedor = fornecedor;

            return medicamento;
        }
    }
}
