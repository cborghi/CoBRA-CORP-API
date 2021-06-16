using System;
using System.Collections.Generic;

namespace CoBRA.Domain.Entities
{
    public class TabelaOrigem
    {
        public Guid IdTabela { get; set; }
        public string NomeTabela { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<CampoOrigem> Campos { get; set; }
    }
}
