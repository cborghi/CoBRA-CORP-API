using System.Collections.Generic;
using CoBRA.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace CoBRA.Domain.Interfaces
{
    public interface IPainelIndicadorMetaRepository
    {
        Task<IEnumerable<CabecalhoPainelMeta>> BuscarCabecalhoPainel(CabecalhoPainelMeta painelMeta, Guid? PeriodoId);

        Task<IList<LinhaPainelMeta>> BuscarLinhaPainel(LinhaPainelMeta painelMeta);
    }
}
