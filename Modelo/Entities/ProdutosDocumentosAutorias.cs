namespace CoBRA.Domain.Entities
{
    public class ProdutosDocumentosAutorias
    {
        public decimal PROD_ID { get; set; }
        public decimal AUTO_ID { get; set; }
        public string DOC_ID { get; set; }
        public string TITULO { get; set; }
        public string AUTORES { get; set; }
        public int? FAIXAETARIA { get; set; }
        public string CAMINHOARQUIVO { get; set; }
        public string CAPA { get; set; }
        public string ASSU_DESCRICAO { get; set; }
        public string CDIS_DESCRICAO { get; set; }
        public string FETA_DESCRICAO { get; set; }
        public string TIPOARQUIVO { get; set; }
    }
}
