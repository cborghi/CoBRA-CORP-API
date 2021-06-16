using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class AutoresBeneficiariosViewModel
    {
        public int IdAutorBeneficiario { get; set; }
        public short IdTipoCadastro { get; set; }
        public string TipoPessoa { get; set; }
        public int IdEstado { get; set; }
        public bool Ativo { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string TelResidencial { get; set; }
        public string Celular { get; set; }
    }
}
