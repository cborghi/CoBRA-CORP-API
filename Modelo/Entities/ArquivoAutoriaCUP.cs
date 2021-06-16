using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ArquivoAutoriaCUP
    {
        public int IdDoc { get; set; }
        public long IdAutoria { get; set; }
        public string Caminho { get; set; }
        public string Nome { get; set; }
    }
}
