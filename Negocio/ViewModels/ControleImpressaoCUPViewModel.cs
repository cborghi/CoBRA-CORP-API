using System;

namespace CoBRA.Application.ViewModels
{
    public class ControleImpressaoCUPViewModel
    {
        public long IdControleImpressao { get; set; }
        public long IdProduto { get; set; }
        public string ContImpEdicao { get; set; }
        public string ContImpImpressao { get; set; }
        public string ContImpGrafica { get; set; }
        public string ContImpData { get; set; }
        public int ContImpTiragem { get; set; }
        public string ContImpObservacoesGerais { get; set; }
    }
}
