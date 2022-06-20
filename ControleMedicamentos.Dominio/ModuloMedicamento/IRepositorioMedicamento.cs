using ControleMedicamentos.Dominio.Compartilhado;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public interface IRepositorioMedicamento : IRepositorioBase<Medicamento>
    {
        List<Medicamento> SelecionarMedicamentosComRequisicoes();

    }
}
