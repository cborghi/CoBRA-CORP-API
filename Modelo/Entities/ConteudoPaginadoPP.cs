using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ConteudoPaginadoPP
    {
        public int NumeroPagina { get; set; }
        public int RegistrosPagina { get; set; }
        public int Contagem { get; set; }
        public int QtdePaginas { get; set; }
        public List<ConteudoPP> LstConteudosPP { get; set; }
    }
}
