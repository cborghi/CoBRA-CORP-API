using System;

namespace CoBRA.Domain.Entities
{
    public class Cargo
    {
        public Guid IdCargo { get; set; }
        public string Descricao { get; set; }
        public Guid IdHierarquia { get; set; }
        public Guid IdGrupo { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
