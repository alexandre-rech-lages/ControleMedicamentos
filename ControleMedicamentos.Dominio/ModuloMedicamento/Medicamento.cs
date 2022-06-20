using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public Medicamento()
        {
            Requisicoes = new List<Requisicao>();
        }

        public Medicamento(string nome, string descricao, string lote, DateTime validade, int quantidadeDisponivel, Fornecedor fornecedor)
        {
            Nome = nome;
            Descricao = descricao;
            Lote = lote;
            Validade = validade;
            QuantidadeDisponivel = quantidadeDisponivel;
            Fornecedor = fornecedor;
            Requisicoes = new List<Requisicao>();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public DateTime Validade { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public List<Requisicao> Requisicoes { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int QuantidadeRequisicoes { get { return Requisicoes.Count; } }

        public override bool Equals(object obj)
        {
            return obj is Medicamento medicamento &&
                   Id == medicamento.Id &&
                   Nome == medicamento.Nome &&
                   Descricao == medicamento.Descricao &&
                   Lote == medicamento.Lote &&
                   Validade.Date == medicamento.Validade.Date &&
                   QuantidadeDisponivel == medicamento.QuantidadeDisponivel &&
                   Fornecedor.Equals(medicamento.Fornecedor) &&
                   QuantidadeRequisicoes == medicamento.QuantidadeRequisicoes;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Nome);
            hash.Add(Descricao);
            hash.Add(Lote);
            hash.Add(Validade);
            hash.Add(QuantidadeDisponivel);
            hash.Add(Requisicoes);
            hash.Add(Fornecedor);
            hash.Add(QuantidadeRequisicoes);
            return hash.ToHashCode();
        }

        public void RegistrarRequisicao(Requisicao requisicao)
        {
            if (Requisicoes.Contains(requisicao))
                return;

            Requisicoes.Add(requisicao);

            requisicao.Medicamento = this;
        }
    }
}
