using CoBRA.Application.ViewModels;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface IDepartamentoAppService
    {
        List<DepartamentoViewModel> ObterTodos();
    }
}
