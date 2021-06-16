using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class AutorSimplificado
    {
        public int IdAutorBeneficiario { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string Codigo { get; set; }
        public string Loja { get; set; }
    }
}
