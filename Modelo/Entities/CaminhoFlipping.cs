using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class CaminhoFlipping
    {
        public int IdCaminhoFlipping { get; set; }
        public string Caminho { get; set; }
        public long IdProduto { get; set; }
        public string EBSA { get; set; }
    }
}
