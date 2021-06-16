using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IGrupoPainelAppService
    {
        Task<IEnumerable<GrupoPainelViewModel>> ListarGrupoPainel(Guid? StatusId);
    }
}