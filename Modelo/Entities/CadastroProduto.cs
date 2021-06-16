using System.Collections.Generic;

namespace CoBRA.Domain.Entities
{
    public class CadastroProduto
    {
        public string CodigoEbsa { get; set; }
        public IList<Dicionario> Mercados { get; set; }
        public IList<Dicionario> Anos { get; set; }
        public IList<Dicionario> Disciplinas { get; set; }
        public IList<Dicionario> Midias { get; set; }
        public IList<Dicionario> Tipos { get; set; }
        public IList<Dicionario> Versoes { get; set; }
        public int Edicao { get; set; }
        public int SequencialProduto { get; set; }
    }
}
