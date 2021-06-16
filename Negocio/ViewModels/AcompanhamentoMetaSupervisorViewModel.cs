using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class AcompanhamentoMetaSupervisorViewModel : AcompanhamentoMetaViewModel
    {
        public AcompanhamentoMetaSupervisorViewModel()
        {
            ListaLinhaMetaIndividual = new List<AcompanhamentoMetaSupervisorMetaIndividualViewModel>();
        }
        public List<AcompanhamentoMetaSupervisorMetaIndividualViewModel> ListaLinhaMetaIndividual { get; set; }
    }

    public class AcompanhamentoMetaSupervisorMetaIndividualViewModel : MetaIndividualViewModel
    {
        public string Realizado { get; set; }
        public string Ponderado { get; set; }
        public string Porcentagem { get; set; }
    }
}

