using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(string conta, bool email);
        Usuario Obter(int id);
        Usuario ObterPorEmail(string email);

        Usuario ObterPorContaAd(string contaAd);
        List<Usuario> ObterTodos();
        List<Usuario> ObterFiltrados(string nome, string email, int grupoId);
        List<Filial> ObterFiliais();
        List<Usuario> ObterColaboradores(int idUsuario, int idEstado = 0);
        List<Usuario> ObterColaboradoresGerente(int idUsuario, int idEstado = 0);
        List<Usuario> ObterDivulgadoresDiretoria(int idEstado = 0);
        string Adicionar(Usuario usuario);
        string GerarGuid(Usuario usuario);
        bool ExpiraGuid(int idUsuario);
        Usuario ObterPorGuid(string guid);
        string Editar(Usuario usuario);
        string Ativar(Usuario usuario);
        bool UsuarioCadastrado(string nome, int idUsuario = 0);
        string FavoritarUsuario(UsuarioRamal usuario);
        string ExcluirUsuarioFavoritado(int id);
        List<UsuarioRamal> ListarUsuariosFavoritados(int idUsuario);

        IList<Grupo> ObterGruposUsuario(int idUsuario);
    }
}
