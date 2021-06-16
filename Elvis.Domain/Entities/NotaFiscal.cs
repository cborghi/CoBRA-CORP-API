using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elvis.Domain.Entities
{
    public class NotaFiscal
    {
        public string F1_DOC { get; set; }
        public string F1_SERIE { get; set; }
        public string F1_ESPECIE { get; set; }
        public string F1_LOJA { get; set; }
        public DateTime F1_EMISSAO { get; set; }
        public string F1_TIPO { get; set; }
        public string F1_COND { get; set; }
        public Decimal F1_VALMERC { get; set; }
        public Decimal F1_VALBRUT { get; set; }
        public Decimal F1_VALICM { get; set; }
        public List<NotaFiscal> Items { get; set; }

    }
}
