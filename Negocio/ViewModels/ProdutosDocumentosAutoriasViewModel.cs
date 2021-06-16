using Newtonsoft.Json;
using System;

namespace CoBRA.Application.ViewModels
{
    public class ProdutosDocumentosAutoriasViewModel
    {
        [JsonProperty("PROD_ID")]
        public decimal PROD_ID { get; set; }
        [JsonProperty("AUTO_ID")]
        public decimal AUTO_ID { get; set; }
        [JsonProperty("DOC_ID")]
        public string DOC_ID { get; set; }
        [JsonProperty("TITULO")]
        public string TITULO { get; set; }
        [JsonProperty("AUTORES")]
        public string AUTORES { get; set; }
        [JsonProperty("FAIXAETARIA")]
        public int? FAIXAETARIA { get; set; }
        [JsonProperty("CAMINHOARQUIVO")]
        public string CAMINHOARQUIVO { get; set; }
        [JsonProperty("CAPA")]
        public string CAPA { get; set; }
        [JsonProperty("ASSU_DESCRICAO")]
        public string ASSU_DESCRICAO { get; set; }
        [JsonProperty("CDIS_DESCRICAO")]
        public string CDIS_DESCRICAO { get; set; }
        public string FETA_DESCRICAO { get; set; }
        public string TIPOARQUIVO { get; set; }
    }
}