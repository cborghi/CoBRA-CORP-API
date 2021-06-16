using CoBRA.Application.ViewModels;

namespace CoBRA.Application.Interfaces
{
    public interface ILoginAppService
    {
        object Login(UsuarioViewModel usuario);
        bool ValidaEmail(string email);
        string EsqueceuSenha(string email);
        UsuarioViewModel ObterUsuarioGuid(string guid);
        bool ExpiraGuid(UsuarioViewModel usuario);
        bool ValidaSenha(string senha);
    }
}
