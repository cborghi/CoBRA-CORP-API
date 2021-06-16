using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class CapaCUP
    {
        public long IdCapa { get; set; }
        public long IdProduto { get; set; }
        public string CapaCores { get; set; }
        public string CapaTipoPapel { get; set; }
        public string CapaGramatura { get; set; }
        public string CapaOrelha { get; set; }
        public string CapaObservacoes { get; set; }
        public string CapaAcabamento { get; set; }
        public string CapaAcabamentoLombada { get; set; }
    }
}
