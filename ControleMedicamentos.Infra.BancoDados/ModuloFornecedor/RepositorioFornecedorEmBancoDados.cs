using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorEmBancoDados :
        RepositorioBase<Fornecedor, ValidadorFornecedor, MapeadorFornecedor>,
        IRepositorioFornecedor
    {
        protected override string sqlInserir =>
            @"INSERT INTO [TBFORNECEDOR]
                (
                    [NOME],       
                    [TELEFONE], 
                    [EMAIL],
                    [CIDADE],                    
                    [ESTADO]   
                )
            VALUES
                (
                    @NOME,
                    @TELEFONE,
                    @EMAIL,
                    @CIDADE,
                    @ESTADO
                ); SELECT SCOPE_IDENTITY();";

        protected override string sqlEditar =>
            @" UPDATE [TBFORNECEDOR]
                    SET 
                        [NOME] = @NOME, 
                        [TELEFONE] = @TELEFONE, 
                        [EMAIL] = @EMAIL,
                        [CIDADE] = @CIDADE, 
                        [ESTADO] = @ESTADO
                    WHERE [ID] = @ID";

        protected override string sqlExcluir =>
            @"DELETE FROM [TBMEDICAMENTO]
                WHERE [FORNECEDOR_ID] = @ID
                DELETE FROM [TBFORNECEDOR] 
                    WHERE [ID] = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
                [ID] FORNECEDOR_ID,       
                [NOME] FORNECEDOR_NOME,
                [TELEFONE] FORNECEDOR_TELEFONE,
                [EMAIL] FORNECEDOR_EMAIL,             
                [CIDADE] FORNECEDOR_CIDADE,                    
                [ESTADO] FORNECEDOR_ESTADO
            FROM
                [TBFORNECEDOR]";

        protected override string sqlSelecionarPorId =>
            @"SELECT 
               [ID] FORNECEDOR_ID,       
                [NOME] FORNECEDOR_NOME,
                [TELEFONE] FORNECEDOR_TELEFONE,
                [EMAIL] FORNECEDOR_EMAIL,
                [CIDADE] FORNECEDOR_CIDADE,
                [ESTADO] FORNECEDOR_ESTADO
            FROM
                [TBFORNECEDOR]
            WHERE 
                [ID] = @ID";
    }
}
