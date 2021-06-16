using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class CabecalhoPainelMetaViewModel
    {
        public CabecalhoPainelMetaViewModel()
        {
            GrupoPainel = new GrupoPainelViewModel();
            StatusPainel = new StatusPainelViewModel();
            LinhasPainel = new List<LinhaPainelMetaViewModel>();
        }

        public Guid? Id { get; set; }
        public GrupoPainelViewModel GrupoPainel { get; set; }
        public StatusPainelViewModel StatusPainel { get; set; }
        public IList<LinhaPainelMetaViewModel> LinhasPainel { get; set; }
        public string Observacao { get; set; }
        public Guid? IdCargo { get; set; }
    }
}
