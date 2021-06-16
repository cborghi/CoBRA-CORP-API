using System;
using System.Collections.Generic;

namespace CoBRA.Application.ViewModels
{
    public class ProdutoCUPViewModel
    {
        public long IdProduto { get; set; }
        public string Mercado { get; set; }
        public string Programa { get; set; }
        public int? AnoPrograma { get; set; }
        public string Selo { get; set; }
        public int Tipo { get; set; }
        public int? Segmento { get; set; }
        public int? Ano { get; set; }
        public int? FaixaEtaria { get; set; }
        public int? Composicao { get; set; }
        public int? Disciplina { get; set; }
        public int? TemaTransversal { get; set; }
        public string DataPublicacao { get; set; }
        public List<AssuntoCUPViewModel> Assunto { get; set; }
        public List<TemaCUPViewModel> TemasNorteadores { get; set; }
        public int? GeneroTextual { get; set; } 
        public int? ConteudoDisciplinar { get; set; }
        public List<DataEspecialCUPViewModel> DatasEspeciais { get; set; }
        public string Premiacao { get; set; }
        public string Versao { get; set; }
        public int Colecao { get; set; }
        public string Titulo { get; set; }
        public int Edicao { get; set; }
        public int? Midia { get; set; }
        public int UnidadeMedida { get; set; }
        public int Plataforma { get; set; }
        public string ISBN { get; set; }
        public int Status { get; set; }
        public string CodigoBarras { get; set; }
        public string Sinopse { get; set; }
        public string EBSA { get; set; }
        public string NomeCapa { get; set; }
        public int Origem { get; set; }
        public int TipoProduto { get; set; }
        public int SegmentoProtheus { get; set; }
        public string UrlImagem { get; set; }
        public string UltimaIntegracaoProtheus { get; set; }
        public MioloCUPViewModel Miolo { get; set; }
        public CapaCUPVIewModel Capa { get; set; }
        public List<CadernoCUPViewModel> Cadernos { get; set; }
        public List<EncarteCUPViewModel> Encartes { get; set; }
        public MidiaFichaCUPViewModel MidiaFicha { get; set; }
        public List<ControleImpressaoCUPViewModel> CtrlImpressao { get; set; }
        public List<AbaCUPViewModel> lstAbaPermissao { get; set; }
        public List<ArquivoProdutoCUPViewModel> EPUB { get; set; }
        public bool? Reformulado { get; set; }
    }
}
