using System;

namespace CoBRA.Application.ViewModels
{
    public class ResultadoPagamentoMetaViewModel
    {
        public Guid? IdResultadoPagamento { get; set; }
        public int PorcentagemResultado { get; set; }
        public int PorcentagemPagamento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
