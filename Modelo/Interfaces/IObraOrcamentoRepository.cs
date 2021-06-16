using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IObraOrcamentoRepository
    {
        Task<IEnumerable<ObraOrcamento>> ListarObraOrcamento();

        Task GravarOrcamentoLivro(ObraOrcamento obra);
    }
}