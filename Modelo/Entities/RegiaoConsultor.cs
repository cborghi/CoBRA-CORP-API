using System;

namespace CoBRA.Domain.Entities
{
    public class RegiaoConsultor
    {
        public Guid IdRegiao { get; set; }
        public string Descricao { get; set; }
        public string Uf { get; set; }
    }
}
