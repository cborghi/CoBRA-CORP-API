using System;

namespace CoBRA.Domain.Entities
{
    public class SistemaOrigem
    {
        public Guid IdSistema { get; set; }
        public string Descricao { get; set; }
        public string NomeBanco { get; set; }
        public bool Ativo { get; set; }
        public IEquatable<TabelaOrigem> Tabelas { get; set; }
    }
}
