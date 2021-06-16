using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IPainelIndicadorMetaAppService
    {
        Task<PainelIndicadorMetaViewModel> ListarPainelIndicadorMetaAprovado();
        Task<PainelIndicadorMetaViewModel> ListarPainelIndicadorMetaPendente();
    }
}
