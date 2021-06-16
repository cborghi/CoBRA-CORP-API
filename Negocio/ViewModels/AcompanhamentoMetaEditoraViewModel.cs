using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class AcompanhamentoMetaEditoraViewModel : AcompanhamentoMetaViewModel
    {
        public AcompanhamentoMetaEditoraViewModel()
        {
            ListaLinhaMetaIndividual = new List<AcompanhamentoMetaEditoraMetaIndividualViewModel>();
        }
        public List<AcompanhamentoMetaEditoraMetaIndividualViewModel> ListaLinhaMetaIndividual { get; set; }
    }
    public class AcompanhamentoMetaEditoraMetaIndividualViewModel : MetaIndividualViewModel
    {
        public string Realizado { get; set; }
        public string Ponderado { get; set; }
        public string Porcentagem { get; set; }
    }
}



