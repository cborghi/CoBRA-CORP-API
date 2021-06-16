using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class AprovacaoGrupoViewModel
    {
        public AprovacaoGrupoViewModel()
        {
            ListaLinhaAprovacaoGrupo = new List<LinhaAprovacaoGrupoViewModel>();
        }

        public string Id { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public string Aprovacao { get; set; }
        public List<LinhaAprovacaoGrupoViewModel> ListaLinhaAprovacaoGrupo { get; set; }
    }

    public class LinhaAprovacaoGrupoViewModel
    {

        public string Id { get; set; }
        public string Indicador { get; set; }
        public string Meta { get; set; }
        public string OrigemDados { get; set; }
        public int Peso { get; set; }
    }
}

