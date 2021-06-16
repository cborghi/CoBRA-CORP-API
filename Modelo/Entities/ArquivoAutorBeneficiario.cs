using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ArquivoAutorBeneficiario
    {
        public string IdArquivo { get; set; }
        public int IdAutorBeneficiario { get; set; }
        public string Nome { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
