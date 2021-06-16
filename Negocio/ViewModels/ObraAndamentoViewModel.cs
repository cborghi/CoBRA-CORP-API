using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class ObraAndamentoViewModel
    {
        public string CodigoObra { get; set; }
        public string Titulo { get; set; }
        public int QuantidadeCollection { get; set; }
        public decimal ValorGasto { get; set; }
        public decimal ValorOrcado { get; set; }
        public decimal PorcentagemGasta { get; set; }

        public string PorcentagemGastaPercentual
        {
            get { return string.Concat(PorcentagemGasta, "%"); }
        }


        public DateTime DtCadastro { get; set; }
        public int TotalItens { get; set; }
        public string Programa { get; set; }
        public decimal ValorImagensAprovadas { get; set; }

    }
}
