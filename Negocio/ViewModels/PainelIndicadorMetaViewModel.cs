using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class PainelIndicadorMetaViewModel
    {
        public PainelIndicadorMetaViewModel()
        {
            ListaLinhaPainelIndicadorMeta = new List<LinhaPainelIndicadorMetaViewModel>();
        }
        public string Status { get; set; }

        public List<LinhaPainelIndicadorMetaViewModel> ListaLinhaPainelIndicadorMeta { get; set; }
    }

    public class LinhaPainelIndicadorMetaViewModel
    {
        public string Id { get; set; }
        public string Grupo { get; set; }
        public string Status { get; set; }
    }
}

