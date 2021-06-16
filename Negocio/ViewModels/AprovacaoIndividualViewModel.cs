using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class AprovacaoIndividualViewModel
    {
        public AprovacaoIndividualViewModel()
        {
            ListaLinhaAprovacaoIndividual = new List<MetaIndividualViewModel>();
        }

        public string Id { get; set; }
        public string Cargo { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public string Aprovacao { get; set; }

        public List<MetaIndividualViewModel> ListaLinhaAprovacaoIndividual { get; set; }
    }
}

