using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IGrupoPainelRepository
    {
        Task<IEnumerable<GrupoPainel>> ListaGrupoPainel(Guid? StatusId);
    }
}