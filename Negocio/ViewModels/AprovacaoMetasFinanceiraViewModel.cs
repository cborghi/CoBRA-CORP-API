using System;

namespace CoBRA.Application.ViewModels
{
    public class AprovacaoMetaFinanceiraViewModel
    {
        public string Id { get; set; }
        public string IdMetaFinanceira { get; set; }
        public string IdUsuario { get; set; }
        public string IdLinhaMeta { get; set; }
        public string IdStatus { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Grupo { get; set; }
        public string Situacao { get; set; }
        public string Observacao { get; set; }
        public decimal MetaReceitaLiquida { get; set; }
        public decimal ValorRecebimento { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}