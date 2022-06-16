using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class MapeadorPaciente : MapeadorBase<Paciente>
    {
        public override void ConfigurarParametros(Paciente paciente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", paciente.Id);
            comando.Parameters.AddWithValue("NOME", paciente.Nome);
            comando.Parameters.AddWithValue("CARTAOSUS", paciente.CartaoSUS);
        }

        public override Paciente ConverterRegistro(SqlDataReader leitorPaciente)
        {
            int id = Convert.ToInt32(leitorPaciente["ID"]);
            string nome = Convert.ToString(leitorPaciente["NOME"]);
            string cartaoSus = Convert.ToString(leitorPaciente["CARTAOSUS"]);

            return new Paciente()
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartaoSus
            };
        }
    }
}
