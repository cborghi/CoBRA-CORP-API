using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class CabecalhoRelatorioPainelViewModel
    {
        public string NomeUsuario { get; set; }
        public string Cargo { get; set; }
        public string Filial { get; set; }
        public string RegiaoAtuacao { get; set; }
        public string Periodo { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public string Proporcao { get; set; }

        public static implicit operator List<object>(CabecalhoRelatorioPainelViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
