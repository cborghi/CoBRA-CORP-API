using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class AcompanhamentoMetaGerenteViewModel : AcompanhamentoMetaViewModel
    {
        public AcompanhamentoMetaGerenteViewModel()
        {
            ListaLinhaMetaIndividual = new List<AcompanhamentoMetaGerenteMetaIndividualViewModel>();
        }
        public List<AcompanhamentoMetaGerenteMetaIndividualViewModel> ListaLinhaMetaIndividual { get; set; }
    }

    public class AcompanhamentoMetaGerenteMetaIndividualViewModel : MetaIndividualViewModel
    {
        public string Realizado { get; set; }
        public string Ponderado { get; set; }
        public string Porcentagem { get; set; }
    }
}

