using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class RequisicaoComprasViewModel
    {
        public int Id { get; set; }
        public string CentroCusto { get; set; }
        public string AprovadorSupervisor { get; set; }
        public string AprovadorGerente { get; set; }
        public string AnaliseGerente { get; set; }
        public string AnaliseSupervisor { get; set; }
        public string Solicitante { get; set; }
        public int IdSolicitante { get; set; }
        public DateTime Emissao { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
        public string Cgc { get; set; }
        public DateTime Incluido { get; set; }
        public string Nota { get; set; }
        public string TipoDocumento { get; set; }
        public decimal Valor { get; set; }
        public string Pessoa { get; set; }
        public string FormaPagamento { get; set; }
        public DateTime PrazoPagamento { get; set; }
        public DateTime PrazoEntrega { get; set; }
        public DateTime? EnviadoFinanceiro { get; set; }
        public string Contato { get; set; }
        public ServicoViewModel Servico { get; set; }
        public string Observacao { get; set; }
        public string Telefone { get; set; }
        public string Deposito { get; set; }
        public string DocLink { get; set; }
        public string Departamento { get; set; }
        public List<RequisicaoElvisViewModel> ListaObras { get; set; }
        public string IntegradoProtheus { get; set; }
        public bool Cancelada { get; set; }
        public bool ElvisOrigem { get; set; }
        public string TituloObra { get; set; }
        public bool NotaFiscalCompartilhada { get; set; }
        public string SerieNotaFiscal { get; set; }
    }
}
