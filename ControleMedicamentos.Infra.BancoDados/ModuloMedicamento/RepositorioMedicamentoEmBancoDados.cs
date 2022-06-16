using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados :
        RepositorioBase<Medicamento, ValidadorMedicamento, MapeadorMedicamento>
    {
        protected override string sqlInserir =>
            @"INSERT INTO TBMEDICAMENTO";

        protected override string sqlEditar =>
            "";

        protected override string sqlExcluir =>
            "";

        protected override string sqlSelecionarPorId =>
            "";

        protected override string sqlSelecionarTodos =>
            "";
    }
}
