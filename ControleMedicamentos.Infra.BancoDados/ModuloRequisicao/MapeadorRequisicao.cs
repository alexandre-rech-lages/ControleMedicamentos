using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class MapeadorRequisicao : MapeadorBase<Requisicao>
    {
        public override void ConfigurarParametros(Requisicao registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", registro.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", registro.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", registro.Medicamento.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", registro.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", registro.Data);
        }

        public override Requisicao ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Convert.ToInt32(leitorRegistro["REQUISICAO_ID"]);            
            var quantidadeMedicamento = Convert.ToInt32(leitorRegistro["REQUISICAO_QUANTIDADEMEDICAMENTO"]);
            var data = Convert.ToDateTime(leitorRegistro["REQUISICAO_DATA"]);

            Requisicao requisicao = new Requisicao();
            requisicao.Id = id;
            requisicao.QtdMedicamento = quantidadeMedicamento;
            requisicao.Data = data;

            requisicao.Funcionario = new MapeadorFuncionario().ConverterRegistro(leitorRegistro);
            requisicao.Paciente = new MapeadorPaciente().ConverterRegistro(leitorRegistro);
            requisicao.Medicamento = new MapeadorMedicamento().ConverterRegistro(leitorRegistro);

            return requisicao;
        }
    }
}
