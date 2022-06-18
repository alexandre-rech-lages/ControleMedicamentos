using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados :
        RepositorioBase<Medicamento, ValidadorMedicamento, MapeadorMedicamento>
    {
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
                [TBMEDICAMENTO] AS MEDICAMENTO LEFT JOIN 
                [TBFORNECEDOR] AS FORNECEDOR
            ON
                FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID";

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
                [TBMEDICAMENTO] AS MEDICAMENTO LEFT JOIN 
                [TBFORNECEDOR] AS FORNECEDOR
            ON
                FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID
            WHERE 
                MEDICAMENTO.[ID] = @ID";

        private string sqlSelecionarTodosComBaixaQuantidade =>
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
		            [TBMEDICAMENTO]

            WHERE 
                QUANTIDADEDISPONIVEL <= @QUANTIDADEDISPONIVEL";


    }
}
