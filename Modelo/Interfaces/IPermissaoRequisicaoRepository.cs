using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IPermissaoRequisicaoRepository
    {
        Task GravarPermissaoUsuario(PermissaoRequisicao permissao);
        Task<IList<PermissaoRequisicao>> ListarPermissaoUsuario(Usuario usuario);
        Task<IList<PermissaoRequisicao>> FiltrarPermissaoUsuario(PermissaoRequisicao permissao);
    }
}