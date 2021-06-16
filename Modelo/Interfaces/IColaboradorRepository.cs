using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IColaboradorRepository
    {
        List<Colaborador> ObterColaboradores(Guid idCargo);
        Colaborador ObterColaborador(Guid idUsuario);
        Task<List<Colaborador>> ObterColaboradoresPorSupervisor(Guid idUsuario, Guid? idRegiao);
    }
}
