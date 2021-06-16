﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class MioloCUP
    {
        public long IdMiolo { get; set; }
        public long IdProduto { get; set; }
        public int mioloPaginas { get; set; }
        public decimal mioloFormatoAltura { get; set; }
        public decimal mioloFormatoLargura { get; set; }
        public decimal mioloPeso { get; set; }
        public string mioloCores { get; set; }
        public string mioloTipoPapel { get; set; }
        public string mioloGramatura { get; set; }
        public string mioloObservacoes { get; set; }
    }
}
