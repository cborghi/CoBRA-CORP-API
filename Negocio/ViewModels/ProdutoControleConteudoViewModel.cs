﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Application.ViewModels
{
    public class ProdutoControleConteudoViewModel
    {
        public long ProdId { get; set; }
        public string ProdTitulo { get; set; }
        public string ProdNomeCapa { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Foto { get; set; }
        public string ProdSinopse { get; set; }
        public bool Controle { get; set; }
    }
}