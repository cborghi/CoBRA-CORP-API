using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IPainelMetaRealRepository
    {
        Task<IEnumerable<PainelMetaReal>> ConsultaPainelMetaReal(Guid? MetaRealId, Guid? UsuarioId, Guid? LinhaMetaId);
    }
}