using CoBRA.API.Authorize.Interface;

namespace CoBRA.API.Authorize
{
    public class PerfilRequirement : IPerfilRequirement
    {
       
        public bool ValidarUsuarioMenuAcesso(int id, string path)
        {
            //var teste = _menuAppService.ObterMenusPorGrupo(id).FirstOrDefault(x => x.Link.Split('/').Last() == path);

            return true;
        }
    }
}
