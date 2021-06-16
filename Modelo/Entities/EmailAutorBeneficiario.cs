using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class EmailAutorBeneficiario
    {
        public int IdEmail { get; set; }
        public int IdAutorBeneficiario { get; set; }
        public  string Destinatario { get; set; }
    }
}
