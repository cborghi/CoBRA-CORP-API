using CoBRA.Application.ViewModels;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface INivelAppService
    {
        List<NivelViewModel> Obter();
    }
}
