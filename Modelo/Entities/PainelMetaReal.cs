using System;

namespace CoBRA.Domain.Entities
{
    public class PainelMetaReal
    {
        public Guid IdMetaReal { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdLinhaMeta { get; set; }
        public decimal Realizado { get; set; }
        public decimal Ponderado { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}