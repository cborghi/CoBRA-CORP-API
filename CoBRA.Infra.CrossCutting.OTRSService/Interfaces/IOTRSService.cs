using CoBRA.Infra.CrossCutting.OTRSService.ViewModel;
using System.Threading.Tasks;

namespace CoBRA.Infra.CrossCutting.OTRSService.Interfaces
{
    public interface IOTRSService
    {
        Task<string> GerarSecao();
        Task<AtendimentoOTRS> GerarTicket(string usuario);
        Task<UsuarioOTRS> ObterUsuarioOTRS(string usuario);
    }
}
