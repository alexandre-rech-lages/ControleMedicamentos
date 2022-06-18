using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados :
        RepositorioBase<Requisicao, ValidadorRequisicao, MapeadorRequisicao>
    {

        protected override string sqlInserir =>
            @"INSERT INTO [TBREQUISICAO]
                (
                    [FUNCIONARIO_ID],       
                    [PACIENTE_ID], 
                    [MEDICAMENTO_ID],
                    [QUANTIDADEMEDICAMENTO],                    
                    [DATA]      
                )
            VALUES
                (
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,
                    @MEDICAMENTO_ID,
                    @QUANTIDADEMEDICAMENTO,
                    @DATA
                ); SELECT SCOPE_IDENTITY();";

        protected override string sqlEditar =>
            @" UPDATE [TBREQUISICAO]
                    SET 
                        [FUNCIONARIO_ID] = @FUNCIONARIO_ID, 
                        [PACIENTE_ID] = @PACIENTE_ID, 
                        [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                        [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO, 
                        [DATA] = @DATA
                    WHERE [ID] = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM [TBREQUISICAO] 
                WHERE [ID] = @ID";

        protected override string sqlSelecionarTodos =>
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
                    ON MEDICAMENTO.ID = FORNECEDOR.ID";

        protected override string sqlSelecionarPorId =>
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
                    ON MEDICAMENTO.ID = FORNECEDOR.ID
            WHERE
                REQUISICAO.[ID] = @ID";
    }
}

