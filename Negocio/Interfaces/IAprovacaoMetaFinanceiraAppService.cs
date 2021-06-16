using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IAprovacaoMetaFinanceiraAppService
    {
        Task<AprovacaoMetaFinanceiraViewModel> ObterItem(string id);
        Task GravarItem(MetaFinanceiraViewModel dto);
    }
}
