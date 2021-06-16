using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class CadernoCUP
    {
        public long IdCaderno { get; set; }
        public long IdProduto { get; set; }
        public string CadernoTipo { get; set; }
        public string CadernoTipoOutros { get; set; }
        public int CadernoPaginas { get; set; }
        public decimal CadernoAltura { get; set; }
        public decimal CadernoLargura { get; set; }
        public string CadernoPeso { get; set; }
        public string CadernoMioloCores { get; set; }
        public string CadernoMioloTipoPapel { get; set; }
        public string CadernoMioloGramatura { get; set; }
        public string CadernoMioloObservacoes { get; set; }
        public string CadernoCapaCores { get; set; }
        public string CadernoCapaTipoPapel { get; set; }
        public string CadernoCapaGramatura { get; set; }
        public string CadernoCapaOrelha { get; set; }
        public string CadernoCapaAcabamento { get; set; }
        public string CadernoCapaObservacoes { get; set; }
        public string CadernoCapaAcabamentoLombada { get; set; }
    }
}
