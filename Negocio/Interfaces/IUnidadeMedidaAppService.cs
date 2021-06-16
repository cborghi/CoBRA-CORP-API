using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IUnidadeMedidaAppService
    {
        Task<IEnumerable<UnidadeMedidaViewModel>> ListarUnidadeMedida();
    }
}