using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IDadosOrigemPainelRepository
    {
        Task<IEnumerable<DadosOrigemPainel>> ListarDadosOrigemPainel();
    }
}