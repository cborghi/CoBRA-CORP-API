using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public interface IPermissaoRequisicaoService
    {
        Task GravarPermissaoUsuario(PermissaoRequisicaoViewModel permissao, int idUsuarioAcao, string ipRequisicao);
        Task<IList<PermissaoRequisicaoViewModel>> ListarPermissaoUsuario(UsuarioViewModel usuario);

        Task<IList<PermissaoRequisicaoViewModel>> FiltrarPermissaoUsuario(PermissaoRequisicaoViewModel permissao);
    }
}