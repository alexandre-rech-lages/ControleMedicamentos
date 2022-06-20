using FluentValidation.Results;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.Compartilhado
{
    public interface IRepositorioBase<T> where T : EntidadeBase<T>
    {
        ValidationResult Inserir(T registro);

        ValidationResult Editar(T registro);

        void Excluir(T registro);

        T SelecionarPorId(int id);

        List<T> SelecionarTodos();
    }
}
