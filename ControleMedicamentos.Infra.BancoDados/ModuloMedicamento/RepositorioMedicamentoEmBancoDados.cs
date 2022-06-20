using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados :
        RepositorioBase<Medicamento, ValidadorMedicamento, MapeadorMedicamento>,
        IRepositorioMedicamento
    {
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;

        protected override string sqlInserir =>
            @"INSERT INTO [TBMEDICAMENTO]
            (
                [NOME],
                [DESCRICAO],
                [LOTE],
                [VALIDADE],
                [QUANTIDADEDISPONIVEL],
                [FORNECEDOR_ID]
            )
	        VALUES
             (
                @NOME,
                @DESCRICAO,
                @LOTE,
                @VALIDADE,
                @QUANTIDADEDISPONIVEL,
                @FORNECEDOR_ID
             );SELECT SCOPE_IDENTITY(); ";

        protected override string sqlEditar =>
         @"UPDATE [TBMEDICAMENTO]	
		        SET
                    [NOME] = @NOME,
                    [DESCRICAO] = @DESCRICAO,
                    [LOTE] = @LOTE,
                    [VALIDADE] = @VALIDADE,
                    [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL,
                    [FORNECEDOR_ID] = @FORNECEDOR_ID
		        WHERE
			        [ID] = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM [TBMEDICAMENTO]
                WHERE
                [ID] = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
                MEDICAMENTO.[ID] MEDICAMENTO_ID,       
                MEDICAMENTO.[NOME] MEDICAMENTO_NOME,
                MEDICAMENTO.[DESCRICAO] MEDICAMENTO_DESCRICAO,
                MEDICAMENTO.[LOTE] MEDICAMENTO_LOTE,             
                MEDICAMENTO.[VALIDADE] MEDICAMENTO_VALIDADE,                    
                MEDICAMENTO.[QUANTIDADEDISPONIVEL] MEDICAMENTO_QUANTIDADEDISPONIVEL,                                
                
                FORNECEDOR.[ID] FORNECEDOR_ID,
                FORNECEDOR.[NOME] FORNECEDOR_NOME,                
                FORNECEDOR.[TELEFONE] FORNECEDOR_TELEFONE,
                FORNECEDOR.[EMAIL] FORNECEDOR_EMAIL, 
                FORNECEDOR.[CIDADE] FORNECEDOR_CIDADE,
                FORNECEDOR.[ESTADO] FORNECEDOR_ESTADO
            FROM
                [TBMEDICAMENTO] AS MEDICAMENTO 

            INNER JOIN [TBFORNECEDOR] AS FORNECEDOR
                ON FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID";

        protected override string sqlSelecionarPorId =>
            @"SELECT 
                MEDICAMENTO.[ID] MEDICAMENTO_ID,       
                MEDICAMENTO.[NOME] MEDICAMENTO_NOME,
                MEDICAMENTO.[DESCRICAO] MEDICAMENTO_DESCRICAO,
                MEDICAMENTO.[LOTE] MEDICAMENTO_LOTE,             
                MEDICAMENTO.[VALIDADE] MEDICAMENTO_VALIDADE,                    
                MEDICAMENTO.[QUANTIDADEDISPONIVEL] MEDICAMENTO_QUANTIDADEDISPONIVEL,        

                FORNECEDOR.[ID] FORNECEDOR_ID,
                FORNECEDOR.[NOME] FORNECEDOR_NOME,                
                FORNECEDOR.[TELEFONE] FORNECEDOR_TELEFONE,
                FORNECEDOR.[EMAIL] FORNECEDOR_EMAIL, 
                FORNECEDOR.[CIDADE] FORNECEDOR_CIDADE,
                FORNECEDOR.[ESTADO] FORNECEDOR_ESTADO

            FROM
                [TBMEDICAMENTO] AS MEDICAMENTO INNER JOIN 
                [TBFORNECEDOR] AS FORNECEDOR
            ON
                FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID
            WHERE 
                MEDICAMENTO.[ID] = @ID";

        private string sqlSelecionarRequisicoesPorMedicamento =>
            @"SELECT 
                REQUISICAO.[ID] REQUISICAO_ID,       
                REQUISICAO.[FUNCIONARIO_ID] REQUISICAO_FUNCIONARIO_ID,
                REQUISICAO.[PACIENTE_ID] REQUISICAO_PACIENTE_ID,
                REQUISICAO.[MEDICAMENTO_ID] REQUISICAO_MEDICAMENTO_ID,             
                REQUISICAO.[QUANTIDADEMEDICAMENTO] REQUISICAO_QUANTIDADEMEDICAMENTO, 
                REQUISICAO.[DATA] REQUISICAO_DATA,

                FUNCIONARIO.[ID] FUNCIONARIO_ID,
                FUNCIONARIO.[NOME] FUNCIONARIO_NOME,              
                FUNCIONARIO.[LOGIN] FUNCIONARIO_LOGIN,                    
                FUNCIONARIO.[SENHA] FUNCIONARIO_SENHA,  

                PACIENTE.[ID] PACIENTE_ID,
                PACIENTE.[NOME] PACIENTE_NOME,
                PACIENTE.[CARTAOSUS] PACIENTE_CARTAOSUS,

                FORNECEDOR.[ID] FORNECEDOR_ID,
                FORNECEDOR.[NOME] FORNECEDOR_NOME,                
                FORNECEDOR.[TELEFONE] FORNECEDOR_TELEFONE,
                FORNECEDOR.[EMAIL] FORNECEDOR_EMAIL, 
                FORNECEDOR.[CIDADE] FORNECEDOR_CIDADE,
                FORNECEDOR.[ESTADO] FORNECEDOR_ESTADO,

                MEDICAMENTO.[ID] MEDICAMENTO_ID,   
                MEDICAMENTO.[NOME] MEDICAMENTO_NOME,
                MEDICAMENTO.[DESCRICAO] MEDICAMENTO_DESCRICAO,
                MEDICAMENTO.[LOTE] MEDICAMENTO_LOTE,
                MEDICAMENTO.[VALIDADE] MEDICAMENTO_VALIDADE,
                MEDICAMENTO.[QUANTIDADEDISPONIVEL] MEDICAMENTO_QUANTIDADEDISPONIVEL

            FROM
                [TBREQUISICAO] AS REQUISICAO 

                INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO
                    ON REQUISICAO.FUNCIONARIO_ID = FUNCIONARIO.ID

                INNER JOIN [TBPACIENTE] AS PACIENTE 
                    ON REQUISICAO.PACIENTE_ID = PACIENTE.ID

                INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                    ON REQUISICAO.MEDICAMENTO_ID = MEDICAMENTO.ID

                INNER JOIN [TBFORNECEDOR] AS FORNECEDOR
                    ON MEDICAMENTO.FORNECEDOR_ID = FORNECEDOR.ID

            WHERE
    
                 MEDICAMENTO.[ID] = @MEDICAMENTO_ID";

        public List<Medicamento> SelecionarMedicamentosComRequisicoes()
        {
            var medicamentos = SelecionarTodos();

            foreach (var medicamento in medicamentos)
                CarregarRequisicao(medicamento);

            return medicamentos;
        }

        private void CarregarRequisicao(Medicamento medicamento)
        {
            var conexaoComBanco = new SqlConnection(enderecoBanco);

            var comandoSelecao = new SqlCommand(sqlSelecionarRequisicoesPorMedicamento, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("MEDICAMENTO_ID", medicamento.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorRegistros = comandoSelecao.ExecuteReader();

            var mapeadorRequisicao = new MapeadorRequisicao();

            while (leitorRegistros.Read())
                medicamento.RegistrarRequisicao(mapeadorRequisicao.ConverterRegistro(leitorRegistros));

            conexaoComBanco.Close();
        }
    }
}