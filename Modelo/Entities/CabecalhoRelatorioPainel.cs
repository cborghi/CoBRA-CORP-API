using System;

namespace CoBRA.Domain.Entities
{
    public class CabecalhoRelatorioPainel
    {
        public string NomeUsuario { get; set; }
        public string Cargo { get; set; }
        public string Filial { get; set; }
        public DateTime DataInicioCampanha { get; set; }
        public DateTime DataFimCampanha { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
    }
}
