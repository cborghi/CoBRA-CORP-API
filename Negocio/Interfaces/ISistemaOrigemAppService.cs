using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface ISistemaOrigemAppService
    {
        Task<IEnumerable<SistemaOrigemViewModel>> ListarSistemaOrigem();
    }
}