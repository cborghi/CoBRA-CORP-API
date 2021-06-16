namespace CoBRA.API.Commands
{
    public class FilialCommand
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public EstadoCommand Estado { get; set; }
        public bool Ativo { get; set; }
    }
}
