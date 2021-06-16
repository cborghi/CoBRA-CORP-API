using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IPainelMetaFinanceiraAppService
    {
        Task<PainelMetaFinanceiraViewModel> ListarPainelMetaFinanceiraAprovado();
        Task<PainelMetaFinanceiraViewModel> ListarPainelMetaFinanceiraPendente();
        
    }
}
