using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<UsuarioViewModel> Login(string conta);
        string FavoritarUsuario(UsuarioRamalViewModel usuario);
        UsuarioViewModel Obter(int id);
        string ExcluirUsuarioFavoritado(int id);
        List<UsuarioRamalViewModel> ListarUsuariosFavoritados(int idUsuario);
        List<UsuarioViewModel> ObterTodos();
        List<UsuarioViewModel> ObterFiltrados(string nome, string email, int grupoId);
        List<FilialViewModel> ObterFiliais();
        string Adicionar(UsuarioViewModel usuario);
        string Editar(UsuarioViewModel usuario);
        string AtivarUsuario(UsuarioViewModel usuario);
        List<UsuarioViewModel> ObterColaboradoresGerente(int idUsuario, int idEstado = 0);
        List<UsuarioViewModel> ObterDivulgadoresDiretoria(int idEstado = 0);
        List<UsuarioViewModel> ObterColaboradores(int idUsuario, int idEstado = 0);
        List<UsuarioViewModel> Colaboradores(int idHierarquia, int idUsuario, int idEstado = 0);
        IList<int> ObterGruposUsuario(int idUsuario);
    }
}
