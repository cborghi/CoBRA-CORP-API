using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IAprovacaoIndividualAppService
    {
        Task<AprovacaoIndividualViewModel> ObterItem(string id);
        Task GravarItem(MetaIndividualViewModel dto);
    }
}
