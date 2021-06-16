using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ArquivoProdutoCUP
    {
        public string IdProdutoEPUB { get; set; }
        public long IdProduto { get; set; }
        public string ProdutoEBSA { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
