using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class CadastroProdutoViewModel
    {
        public string CodigoEbsa { get; set; }
        public IList<DicionarioViewModel> Mercados { get; set; }
        public IList<DicionarioViewModel> Anos { get; set; }
        public IList<DicionarioViewModel> Disciplinas { get; set; }
        public IList<DicionarioViewModel> Midias { get; set; }
        public IList<DicionarioViewModel> Tipos { get; set; }
        public IList<DicionarioViewModel> Versoes { get; set; }
        public int Edicao { get; set; }
        public int SequencialProduto { get; set; }
    }
}
