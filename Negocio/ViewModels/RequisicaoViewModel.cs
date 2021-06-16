using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class RequisicaoViewModel
    {
        public int IdRequisicao { get; set; }
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
        public List<ObraViewModel> RequisicaoABD { get; set; }
        public List<RequisicaoArquivosViewModel> Arquivos { get; set; }
        public List<ParcelaRequisicaoViewModel> Parcelas { get; set; }
        public string IntegradoProtheus { get; set; }
        public bool Cancelada { get; set; }
        public bool ElvisOrigem { get; set; }
        public bool NotaFiscalCompartilhada { get; set; }
        public string SerieNotaFiscal { get; set; }
    }
}
