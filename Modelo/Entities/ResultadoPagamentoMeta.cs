using System;

namespace CoBRA.Domain.Entities
{
    public class ResultadoPagamentoMeta
    {
        public Guid? IdResultadoPagamento { get; set; }
        public int PorcentagemResultado { get; set; }
        public int PorcentagemPagamento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
