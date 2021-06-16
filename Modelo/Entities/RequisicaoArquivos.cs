namespace CoBRA.Domain.Entities
{
    public class RequisicaoArquivos
    {
        public long Id { get; set; }
        public long Requisicao { get; set; }
        public string Caminho { get; set; }
        public string Nome { get; set; }
    }
}
