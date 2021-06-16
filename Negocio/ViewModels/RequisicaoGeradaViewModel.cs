using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class RequisicaoGeradaViewModel
    {
        public string TipoDocumento { get; set; }
        public string Nota { get; set; }
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
        public FornecedorViewModel Fornecedor { get; set; }
        public UsuarioViewModel Usuario { get; set; } 
        public List<RequisicaoElvisViewModel> RequisicaoABD { get; set; }
    }
}
