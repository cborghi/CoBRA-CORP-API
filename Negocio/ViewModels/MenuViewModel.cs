namespace CoBRA.Application.ViewModels
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public int? IdPai { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public string Rota { get; set; }
        public bool Ativo { get; set; }
    }
}
