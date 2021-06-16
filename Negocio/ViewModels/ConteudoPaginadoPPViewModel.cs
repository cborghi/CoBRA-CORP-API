using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class ConteudoPaginadoPPViewModel
    {
        public int NumeroPagina { get; set; }
        public int RegistrosPagina { get; set; }
        public int Contagem { get; set; }
        public int QtdePaginas { get; set; }
        public List<ConteudoPPViewModel> LstConteudosPP { get; set; }
    }
}
