using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IUnidadeMedidaRepository
    {
        Task<IEnumerable<UnidadeMedida>> ListaUnidadeMedida();
    }
}