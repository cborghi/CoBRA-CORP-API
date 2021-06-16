namespace CoBRA.Domain.Entities
{
    public class ObraOrcamento
    {
        public int CodigoProduto { get; set; }
        public string CodigoEbsa { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public string Programa { get; set; }
        public int IdUsuario { get; set; }
    }
}
