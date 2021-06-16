namespace CoBRA.Application.ViewModels
{
    public class FilialViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public EstadoViewModel Estado { get; set; }
        public bool Ativo { get; set; }
    }
}
