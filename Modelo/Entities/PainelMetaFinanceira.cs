using System;

namespace CoBRA.Domain.Entities
{
    public class PainelMetaFinanceira
    {
        public Guid IdMetaFinanceira { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdLinhaMeta { get; set; }
        public Guid IdStatus { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Grupo { get; set; }
        public string Situacao { get; set; }
        public string Observacao { get; set; }
        public decimal MetaReceitaLiquida { get; set; }
        public decimal? MetaReceitaLiquidaCalc { get; set; }
        public decimal? ValorRecebimento { get; set; }

        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Regiao { get; set; }
    }
}
