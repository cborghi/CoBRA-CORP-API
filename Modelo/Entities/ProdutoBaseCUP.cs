using System;

namespace CoBRA.Domain.Entities
{
    public class ProdutoBaseCUP
    {
        public int IdProduto { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string EBSA { get; set; }
        public bool Publicado { get; set; }
        public string Integrado { get; set; }
        public string AnoEducacao { get; set; }
        public string Url { get; set; }
        public decimal? Preco { get; set; }
    }
}
