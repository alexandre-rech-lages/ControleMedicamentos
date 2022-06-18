using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class MapeadorFuncionario : MapeadorBase<Funcionario>
    {
        public override void ConfigurarParametros(Funcionario registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("LOGIN", registro.Login);
            comando.Parameters.AddWithValue("SENHA", registro.Senha);
        }

        public override Funcionario ConverterRegistro(SqlDataReader leitorRegistro)
        {
            var id = Convert.ToInt32(leitorRegistro["FUNCIONARIO_ID"]);
            var nome = Convert.ToString(leitorRegistro["FUNCIONARIO_NOME"]);
            var login = Convert.ToString(leitorRegistro["FUNCIONARIO_LOGIN"]);
            var senha = Convert.ToString(leitorRegistro["FUNCIONARIO_SENHA"]);

            Funcionario funcionario = new Funcionario();
            funcionario.Id = id;
            funcionario.Nome = nome;
            funcionario.Login = login;
            funcionario.Senha = senha;

            return funcionario;
        }
    }
}
