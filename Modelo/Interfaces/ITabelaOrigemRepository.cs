using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface ITabelaOrigemRepository
    {
        Task<IEnumerable<TabelaOrigem>> BuscarTabelaOrigem(SistemaOrigem sistema);

        Task<IEnumerable<TabelaOrigem>> ListarTabelaOrigem();
    }
}