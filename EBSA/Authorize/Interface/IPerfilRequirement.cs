using Microsoft.AspNetCore.Authorization;

namespace CoBRA.API.Authorize.Interface
{
    public interface IPerfilRequirement : IAuthorizationRequirement
    {
        bool ValidarUsuarioMenuAcesso(int id, string path);
    }
}
