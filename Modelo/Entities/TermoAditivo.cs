using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class TermoAditivo
    {
        public int IdTermoAditivo { get; set; }
        public int IdAmarracao { get; set; }
        public DateTime DtTermoAditico { get; set; }
        public string ObsTermoAditivo { get; set; }
    }
}
