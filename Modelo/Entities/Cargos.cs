using System;

namespace CoBRA.Domain.Entities
{
    public class Cargos
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
