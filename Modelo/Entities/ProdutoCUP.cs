using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Domain.Entities
{
    public class ProdutoCUP
    {
        public long IdProduto { get; set; }
        public int Mercado { get; set; }
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
        public DateTime? DataPublicacao { get; set; }
        public List<AssuntoCUP> LstAssunto { get; set; }
        public List<TemaCUP> LstTemaNorteador { get; set; }
        public int? GeneroTextual { get; set; }
        public int? ConteudoDisciplinar { get; set; }
        public List<DataEspecialCUP> LstDatasEspeciais { get; set; }
        public string Premiacao { get; set; }
        public string Versao { get; set; }
        public string Colecao { get; set; }
        public string Titulo { get; set; }
        public int Edicao { get; set; }
        public int? Midia { get; set; }
        public int UnidadeMedida { get; set; }
        public string Plataforma { get; set; }
        public string ISBN { get; set; }
        public string Status { get; set; }
        public string CodigoBarras { get; set; }
        public string Sinopse { get; set; }
        public string EBSA { get; set; }
        public string NomeCapa { get; set; }
        public int Origem { get; set; }
        public int TipoProduto { get; set; }
        public int SegmentoProtheus { get; set; }
        public string DataPublicacaoProtheus { get; set; }
        public MioloCUP Miolo { get; set; }
        public CapaCUP Capa { get; set; }
        public List<CadernoCUP> LstCaderno { get; set; }
        public List<EncarteCUP> LstEncarte { get; set; }
        public MidiaFichaCUP MidiaFicha { get; set; }
        public List<ControleImpressaoCUP> LstControleImpressao { get; set; }
        public bool? Reformulado { get; set; }
    }
}
