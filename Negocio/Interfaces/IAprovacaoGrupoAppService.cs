using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IAprovacaoGrupoAppService
    {
        Task<AprovacaoGrupoViewModel> ObterItem(string id);
        Task GravarItem(AprovacaoGrupoViewModel dto);
    }
}
