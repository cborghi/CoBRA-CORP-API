using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface ISistemaOrigemRepository
    {
        Task<IEnumerable<SistemaOrigem>> ListaSistemaOrigem();
    }
}