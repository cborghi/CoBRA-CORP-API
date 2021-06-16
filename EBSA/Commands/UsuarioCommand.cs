namespace CoBRA.API.Commands
{
    public class UsuarioCommand
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public GrupoCommand Grupo { get; set; }
        public int IdGrupo { get; set; }
        public string Ramal { get; set; }
        public string Email { get; set; }
        public string ContaAd { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string CodUsuario { get; set; }
        public FilialCommand Filial { get; set; }
        public bool Ativo { get; set; }
    }
}
