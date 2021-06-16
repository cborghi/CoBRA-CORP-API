using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IPainelMetaRealAppService
    {
        Task<IEnumerable<PainelMetaRealViewModel>> ConsultaPainelMetaReal(Guid? MetaRealId, Guid? UsuarioId, Guid? LinhaMetaId);
    }
}