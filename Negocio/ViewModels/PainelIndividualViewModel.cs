using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class PainelIndividualViewModel
    {
        public PainelIndividualViewModel()
        {
            ListaLinhaGerente = new List<MetaIndividualViewModel>();
            ListaLinhaSupervisor = new List<MetaIndividualViewModel>();
            ListaLinhaConsultor = new List<MetaIndividualViewModel>();
            ListaLinhaConsultorPrime = new List<MetaIndividualViewModel>();
        }
        public string Status { get; set; }

        public List<MetaIndividualViewModel> ListaLinhaGerente { get; set; }
        public List<MetaIndividualViewModel> ListaLinhaSupervisor { get; set; }
        public List<MetaIndividualViewModel> ListaLinhaConsultor { get; set; }
        public List<MetaIndividualViewModel> ListaLinhaConsultorPrime { get; set; }
    }
}



