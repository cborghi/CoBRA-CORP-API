using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface ICampoOrigemAppService
    {
        Task<IEnumerable<CampoOrigemViewModel>> BuscarCampoOrigem(TabelaOrigemViewModel tabela);

        Task<IEnumerable<CampoOrigemViewModel>> ListarCampoOrigem();
    }
}