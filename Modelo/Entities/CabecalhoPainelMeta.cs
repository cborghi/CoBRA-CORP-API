using System;
using System.Collections.Generic;

namespace CoBRA.Domain.Entities
{
    public class CabecalhoPainelMeta
    {
        public CabecalhoPainelMeta()
        {
            GrupoPainel = new GrupoPainel();
            StatusPainel = new StatusPainel();
            LinhasPainel = new List<LinhaPainelMeta>();
        }

        public Guid? Id { get; set; }
        public GrupoPainel GrupoPainel { get; set; }
        public StatusPainel StatusPainel { get; set; }
        public IList<LinhaPainelMeta> LinhasPainel { get; set; }
        public string Observacao { get; set; }

    }
}
