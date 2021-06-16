using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IPainelIndividualAppService
    {
        Task<PainelIndividualViewModel> ListarPainelIndividualAprovado();
        Task<PainelIndividualViewModel> 
            ListarPainelIndividualPendente();
        
    }
}
