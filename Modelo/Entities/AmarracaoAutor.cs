using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class AmarracaoAutor
    {
        public int IdAmarracaoAutor { get; set; }
        public int IdAmarracao { get; set; }
        public int IdAutor { get; set; }
        public string AmarracaoDescricao { get; set; }
        public string Loja { get; set; }
    }
}
