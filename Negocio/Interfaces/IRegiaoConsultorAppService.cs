using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IRegiaoConsultorAppService
    {
        Task<IList<RegiaoConsultorViewModel>> ListarRegiaoConsutor(Guid idUsuarioRM);

        Task<IList<RegiaoConsultorViewModel>> ListarRegiaoConsutor();
    }
}