using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface ICampoOrigemRepository
    {
        Task<IEnumerable<CampoOrigem>> BuscarTabelaOrigem(TabelaOrigem tabela);

        Task<IEnumerable<CampoOrigem>> ListarCampoOrigem();
    }
}