using System;

namespace CoBRA.Application.ViewModels
{
    public class ArquivoProdutoCUPViewModel
    {
        public string IdProdutoEPUB { get; set; }
        public long IdProduto { get; set; }
        public string ProdutoEBSA { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
