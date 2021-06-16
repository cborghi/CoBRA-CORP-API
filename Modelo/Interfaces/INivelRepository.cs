using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
     public interface ICargoRepository
     {
        List<Cargo> ObterCargos(Guid idGrupo);
        List<Cargos> Obter();
        Task<IEnumerable<Cargo>> ObterCargosAcompanhamento(int idUsuario);
        Task<IEnumerable<Cargo>> ListarCargosAcompanhamento();
        Task<Guid> ObterIdUsuarioRM(int idUsuario);
    }
}
