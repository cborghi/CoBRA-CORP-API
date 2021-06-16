using System;
using System.Collections.Generic;
using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IPainelMetaFinanceiraRepository 
    {
        Task<IEnumerable<PainelMetaFinanceira>> BuscarMetaFinanceira(string statusMetaFinanceira);
        Task<PainelMetaFinanceira> BuscarMetaFinanceira(Guid idMetaFinanceira);
        Task<PainelMetaFinanceira> BuscarMetaFinanceiraUsuario(Guid usuarioId);
    }
}
