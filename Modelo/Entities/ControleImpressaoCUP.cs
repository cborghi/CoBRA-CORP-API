using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ControleImpressaoCUP
    {
        public long IdControleImpressao { get; set; }
        public long IdProduto { get; set; }
        public string ContImpEdicao { get; set; }
        public string ContImpImpressao { get; set; }
        public string ContImpGrafica { get; set; }
        public DateTime? ContImpData { get; set; }
        public int ContImpTiragem { get; set; }
        public string ContImpObservacoesGerais { get; set; }
    }
}
