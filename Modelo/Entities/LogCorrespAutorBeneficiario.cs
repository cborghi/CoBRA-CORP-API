using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class LogCorrespAutorBeneficiario
    {
        public long IdCorrespLogAutorBeneficiario { get; set; }
        public long IdCorrespondencia { get; set; }
        public DateTime DataLog { get; set; }
        public string DescricaoLog { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
    }
}
