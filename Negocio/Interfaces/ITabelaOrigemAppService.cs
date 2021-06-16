using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface ITabelaOrigemAppService
    {
        Task<IEnumerable<TabelaOrigemViewModel>> BuscarTabelaOrigem(SistemaOrigemViewModel sistema);

        Task<IEnumerable<TabelaOrigemViewModel>> ListarTabelaOrigem();
    }
}