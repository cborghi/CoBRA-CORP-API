using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IRegiaoConsultorRepository
    {
        Task<IEnumerable<RegiaoConsultor>> BuscarRegiaoConsultor(Guid idUsuarioRM);

        Task<IEnumerable<RegiaoConsultor>> ListarRegiaoConsultor();
    }
}