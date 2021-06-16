using System;

namespace CoBRA.Application.ViewModels
{
    public class LinhaPainelMetaViewModel
    {
        public LinhaPainelMetaViewModel()
        {
            CampoOrigem = new CampoOrigemViewModel();
            SistemaOrigem = new SistemaOrigemViewModel();
            DadosOrigemPainel = new DadosOrigemPainelViewModel();
        }

        public Guid IdLinhaPainelMeta { get; set; }
        public string Indicador { get; set; }
        public string Meta { get; set; }
        public CampoOrigemViewModel CampoOrigem { get; set; }
        public TabelaOrigemViewModel TabelaOrigem { get; set; }
        public SistemaOrigemViewModel SistemaOrigem { get; set; }
        public DadosOrigemPainelViewModel DadosOrigemPainel { get; set; }
        public int Peso { get; set; }
    }
}
