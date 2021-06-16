using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class ProdutoPaginadoCUPViewModel
    {
        public int NumeroPagina { get; set; }
        public int RegistrosPagina { get; set; }
        public int Contagem { get; set; }
        public int QtdePaginas { get; set; }
        public List<ProdutoBaseCUPViewModel> LstProdutos { get; set; }
        public List<AbaCUPViewModel> lstAbaPermissao { get; set; }
    }
}
