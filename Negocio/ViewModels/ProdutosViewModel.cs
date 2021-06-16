using Newtonsoft.Json;
using System;

namespace CoBRA.Application.ViewModels
{
    public class ProdutosViewModel
    {
        [JsonProperty("PROD_ID")]
        public decimal PROD_ID { get; set; }
        [JsonProperty("PROD_CDIS")]
        public decimal? PROD_CDIS { get; set; }
        [JsonProperty("CONTEUDO")]
        public string CONTEUDO { get; set; }
        [JsonProperty("PROD_TEMA")]
        public decimal? PROD_TEMA { get; set; }
        [JsonProperty("TEMAS")]
        public string TEMAS { get; set; }
        [JsonProperty("TEMAS_TRANS")]
        public string TEMAS_TRANS { get; set; }
        [JsonProperty("PROD_DISC")]
        public decimal? PROD_DISC { get; set; }
        [JsonProperty("PROD_MERCADO")]
        public int? PROD_MERCADO { get; set; }
        [JsonProperty("PROD_PROGRAMA")]
        public string PROD_PROGRAMA { get; set; }
        [JsonProperty("PROD_TIPO")]
        public decimal? PROD_TIPO { get; set; }
        [JsonProperty("PROD_ANO")]
        public string PROD_ANO { get; set; }
        [JsonProperty("PROD_SELO")]
        public string PROD_SELO { get; set; }
        [JsonProperty("PROD_SEGM")]
        public decimal? PROD_SEGM { get; set; }
        [JsonProperty("SEGMENTO")]
        public string SEGMENTO { get; set; }
        [JsonProperty("PROD_AEDU")]
        public decimal? PROD_AEDU { get; set; }
        [JsonProperty("PROD_COMP")]
        public decimal? PROD_COMP { get; set; }
        [JsonProperty("PROD_FAIXAETARIA")]
        public int? PROD_FAIXAETARIA { get; set; }
        [JsonProperty("FAIXAETARIA")]
        public string FAIXAETARIA { get; set; }
        [JsonProperty("PROD_ASSU")]
        public decimal? PROD_ASSU { get; set; }
        [JsonProperty("ASSUNTO")]
        public string ASSUNTO { get; set; }
        [JsonProperty("PROD_GTEX")]
        public decimal? PROD_GTEX { get; set; }
        [JsonProperty("PROD_PREMIACAO")]
        public string PROD_PREMIACAO { get; set; }
        [JsonProperty("PROD_VERSAO")]
        public string PROD_VERSAO { get; set; }
        [JsonProperty("PROD_COLE")]
        public int? PROD_COLE { get; set; }
        [JsonProperty("COLECAO")]
        public string COLECAO { get; set; }
        [JsonProperty("PROD_TITULO")]
        public string PROD_TITULO { get; set; }
        [JsonProperty("PROD_AUTOR")]
        public string PROD_AUTOR { get; set; }
        [JsonProperty("PROD_EDICAO")]
        public decimal? PROD_EDICAO { get; set; }
        [JsonProperty("PROD_MIDI")]
        public int? PROD_MIDI { get; set; }
        [JsonProperty("PROD_PAGINAS")]
        public decimal? PROD_PAGINAS { get; set; }
        [JsonProperty("PROD_PLAT")]
        public int? PROD_PLAT { get; set; }
        [JsonProperty("PROD_FORALTURA")]
        public decimal? PROD_FORALTURA { get; set; }
        [JsonProperty("PROD_FORLARGURA")]
        public decimal? PROD_FORLARGURA { get; set; }
        [JsonProperty("PROD_PESO")]
        public decimal? PROD_PESO { get; set; }
        [JsonProperty("PROD_PUBLICACAO")]
        public DateTime? PROD_PUBLICACAO { get; set; }
        [JsonProperty("PROD_ISBN")]
        public string PROD_ISBN { get; set; }
        [JsonProperty("PROD_STATUS")]
        public string PROD_STATUS { get; set; }
        [JsonProperty("PROD_CODBARRAS")]
        public string PROD_CODBARRAS { get; set; }
        [JsonProperty("PROD_SINOPSE")]
        public string PROD_SINOPSE { get; set; }
        [JsonProperty("PROD_PRECO")]
        public decimal? PROD_PRECO { get; set; }
        [JsonProperty("PROD_EBSA")]
        public string PROD_EBSA { get; set; }
        [JsonProperty("PROD_UNIDADE")]
        public string PROD_UNIDADE { get; set; }
        [JsonProperty("PROD_CODPROT")]
        public string PROD_CODPROT { get; set; }
        [JsonProperty("PROD_PUBLICADO")]
        public bool? PROD_PUBLICADO { get; set; }
        [JsonProperty("PROD_DATAPUBLICACAO")]
        public DateTime? PROD_DATAPUBLICACAO { get; set; }
        [JsonProperty("PROD_VIDEO")]
        public string PROD_VIDEO { get; set; }

        [JsonProperty("CAMINHOARQUIVO")]
        public string CAMINHOARQUIVO { get; set; }

        [JsonProperty("CAPA")]

        public string CAPA { get; set; }
        [JsonProperty("TIPOARQUIVO")]
        public string TIPOARQUIVO { get; set; }
    }
}
