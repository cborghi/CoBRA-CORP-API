using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class MidiaFichaCUP
    {
        public long IdMidia { get; set; }
        public long IdProduto { get; set; }
        public string MidiaTipo { get; set; }
        public string MidiaOutros { get; set; }
    }
}
