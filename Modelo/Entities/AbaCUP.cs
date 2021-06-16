using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class AbaCUP
    {
        public string NomeAba { get; set; }
        public bool AcessoModificacao { get; set; }
        public bool AcessoVisualizacao { get; set; }
    }
}
