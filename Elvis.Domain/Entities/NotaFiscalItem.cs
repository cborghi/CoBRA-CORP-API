using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elvis.Domain.Entities
{
    public class NotaFiscalItem
    {
        public string D1_DOC { get; set; }
        public string D1_SERIE { get; set; }
        public string D1_FORNECE { get; set; }
        public string D1_LOJA { get; set; }
        public string D1_COD { get; set; }
        public string D1_ITEM { get; set; }
        public int D1_QUANT { get; set; }
        public int D1_VUNIT { get; set; }
        public decimal D1_TOTAL { get; set; }
        public DateTime D1_EMISSAO { get; set; }
        public decimal D1_BASEICM { get; set; }
        public decimal D1_PICM { get; set; }
        public decimal D1_VALICM { get; set; }
    }
}
