using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class AcompanhamentoMetaConsultorViewModel : AcompanhamentoMetaViewModel
     {
          public AcompanhamentoMetaConsultorViewModel()
          {
               ListaLinhaMetaIndividual = new List<AcompanhamentoMetaConsultorMetaIndividualViewModel>();
          }
          public List<AcompanhamentoMetaConsultorMetaIndividualViewModel> ListaLinhaMetaIndividual { get; set; }
     }

     public class AcompanhamentoMetaConsultorMetaIndividualViewModel : MetaIndividualViewModel
     {
          public decimal? Realizado { get; set; }
          public decimal? Ponderado { get; set; }
          public decimal? Percentual { get; set; }
     }
}

