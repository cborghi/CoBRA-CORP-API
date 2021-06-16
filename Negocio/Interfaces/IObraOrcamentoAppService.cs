using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IObraOrcamentoAppService
    {
        Task<IEnumerable<ObraOrcamentoViewModel>> ListarObraOrcamento();
        Task GravarOrcamentoLivro(ObraOrcamentoViewModel obra);
    }
}