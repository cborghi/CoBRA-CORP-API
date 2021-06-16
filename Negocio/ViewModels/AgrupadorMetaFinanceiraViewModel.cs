using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class AgrupadorMetaFinanceiraViewModel
    {
        public AgrupadorMetaFinanceiraViewModel()
        {
            ListaMetaFinanceira = new List<MetaFinanceiraViewModel>();
        }
        public List<MetaFinanceiraViewModel> ListaMetaFinanceira { get; set; }
    }

    public class MetaFinanceiraViewModel
    {

        public string Id { get; set; }
        public string Uf { get; set; }
        public string Cargo { get; set; }
        public string Nome { get; set; }
        public string Grupo { get; set; }
        public string Situacao { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public decimal MetaReceitaLiquida { get; set; }
        public decimal? MetaReceitaLiquidaCalc { get; set; }
        public decimal? ValorRecebimento { get; set; }

        public string IdMetaFinanceira { get; set; }
        public string IdUsuario { get; set; }
        public string IdLinhaMeta { get; set; }
        public string IdStatus { get; set; }

        public string Aprovacao { get; set; }
        public string Regiao { get; set; }

    }
}

