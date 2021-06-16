using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class CorrespondenciaDA
    {
        public long IdCorrespondencia { get; set; }
        public int IdAutorBeneficiario { get; set; }
        public int Agenda { get; set; }
        public int CodigoInterno { get; set; }
        public string Assunto { get; set; }
        public string Obs { get; set; }
        public bool Ativo { get; set; }
        public List<LogCorrespAutorBeneficiario> LstLog { get; set; }
    }
}
