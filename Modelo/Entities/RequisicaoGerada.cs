using System;
using System.Collections.Generic;

namespace CoBRA.Domain.Entities
{
    public class RequisicaoGerada
    {
        public string TipoDocumento { get; set; }
        public string Nota { get; set; }
        public string Serie { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Moeda { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; }
        public string ConfirmaPagamento { get; set; }
        public string Cartao { get; set; }
        public string Servico { get; set; }
        public string Observacao { get; set; }
        public string IdElvis { get; set; }
        public string Link { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Usuario Usuario { get; set; }
        public List<RequisicaoElvis> RequisicaoABD { get; set; }
        public List<ParcelaRequisicao> Parcelas { get; set; }
        public bool NotaFiscalCompartilhada { get; set; }
    }
}
