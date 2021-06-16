using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class ProdutoRelatorioCUPViewModel
    {
        public string Titulo { get; set; }
        public string Colecao { get; set; }
        public string Edicao { get; set; }
        public string Mercado { get; set; }
        public string PNLD { get; set; }
        public string Tipo { get; set; }
        public string Capa { get; set; }
        public List<AutoresCUPViewModel> Autores { get; set; }
        public List<ProdutoCUPViewModel> Livros { get; set; }
        public List<ArquivoProdutoCUPViewModel> EPUB { get; set; }
    }
}
