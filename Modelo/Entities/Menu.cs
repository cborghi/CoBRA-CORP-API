namespace CoBRA.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public int? IdPai { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public string Rota { get; set; }
        public bool Ativo { get; set; }
    }
}
