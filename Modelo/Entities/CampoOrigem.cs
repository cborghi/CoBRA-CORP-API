using System;

namespace CoBRA.Domain.Entities
{
    public class CampoOrigem
    {
        public Guid IdCampo { get; set; }
        public string NomeCampo { get; set; }
        public  string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Script { get; set; }
    }
}
