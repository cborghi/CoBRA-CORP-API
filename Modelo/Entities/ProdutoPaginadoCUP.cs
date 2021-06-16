using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ProdutoPaginadoCUP
    {
        public int NumeroPagina { get; set; }
        public int RegistrosPagina { get; set; }
        public int Contagem { get; set; }
        public int QtdePaginas { get; set; }
        public List<ProdutoBaseCUP> LstProdutos { get; set; }
        public List<AbaCUP> lstAbaPermissao { get; set; }
    }
}
