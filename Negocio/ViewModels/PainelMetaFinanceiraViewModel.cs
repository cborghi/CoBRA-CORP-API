using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class PainelMetaFinanceiraViewModel
    {
        public PainelMetaFinanceiraViewModel()
        {
            ListaLinhaGerente = new List<MetaFinanceiraViewModel>();
            ListaLinhaSupervisor = new List<MetaFinanceiraViewModel>();
            ListaLinhaConsultor = new List<MetaFinanceiraViewModel>();
            ListaLinhaConsultorPrime = new List<MetaFinanceiraViewModel>();
        }
        public string Status { get; set; }

        public List<MetaFinanceiraViewModel> ListaLinhaGerente { get; set; }
        public List<MetaFinanceiraViewModel> ListaLinhaSupervisor { get; set; }
        public List<MetaFinanceiraViewModel> ListaLinhaConsultor { get; set; }
        public List<MetaFinanceiraViewModel> ListaLinhaConsultorPrime { get; set; }
    }
    
}