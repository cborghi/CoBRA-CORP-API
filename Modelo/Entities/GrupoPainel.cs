using System;

namespace CoBRA.Domain.Entities
{
    public class GrupoPainel
    {
        public Guid? IdGrupo { get; set; }
        public string Descricao { get; set; }
        public DateTime Dt_Cadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
