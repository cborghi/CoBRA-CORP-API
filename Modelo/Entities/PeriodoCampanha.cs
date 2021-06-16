using System;

namespace CoBRA.Domain.Entities
{
    public class PeriodoCampanha
    {
        public string Id_Periodo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Dat_Fim { get; set; }
    }
}
