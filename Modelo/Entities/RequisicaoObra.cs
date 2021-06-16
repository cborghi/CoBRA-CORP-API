namespace CoBRA.Domain.Entities
{
    public class RequisicaoObra
    {
        public int RequisicaoId { get; set; }
        public string Obra { get; set; }
        public float Total { get; set; }
        public string Projeto { get; set; }
        public string Titulo { get; set; }
        public float Rateio { get; set; }
    }
}
