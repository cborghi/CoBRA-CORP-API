using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class AutoresBeneficiariosPaginadoViewModel
    {
        public int NumeroPagina { get; set; }
        public int RegistrosPagina { get; set; }
        public int Contagem { get; set; }
        public int QtdePaginas { get; set; }
        public List<AutoresBeneficiariosViewModel> LstAutoresBeneficiarios { get; set; }
    }
}
