using System;

namespace CoBRA.Domain.Entities
{
    public class LinhaPainelMeta
    {
        public LinhaPainelMeta()
        {
            CampoOrigem = new CampoOrigem();
            TabelaOrigem = new TabelaOrigem();
            SistemaOrigem = new SistemaOrigem();
            DadosOrigemPainel = new DadosOrigemPainel();

        }

        public Guid? IdLinhaPainelMeta { get; set; }
        public string Indicador { get; set; }
        public string Meta { get; set; }
        public CampoOrigem CampoOrigem { get; set; }
        public TabelaOrigem TabelaOrigem { get; set; }
        public SistemaOrigem SistemaOrigem { get; set; }
        public DadosOrigemPainel DadosOrigemPainel { get; set; }
        public int Peso { get; set; }
    }
}
