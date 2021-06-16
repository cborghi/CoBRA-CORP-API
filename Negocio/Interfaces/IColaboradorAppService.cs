using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IColaboradorAppService
    {
        List<ColaboradorViewModel> ObterPorCargo(Guid idCargo);
        Task<List<ColaboradorViewModel>> ObterPorSupervisor(Guid idUsuario, Guid? idRegiao);
    }
}
