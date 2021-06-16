namespace CoBRA.Domain.Entities
{
    public class Filial
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Estado Estado { get; set; }
        public bool Ativo { get; set; }
    }
}
