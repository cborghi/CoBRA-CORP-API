using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IPainelMetaAnualAppService
    {
        Task<IEnumerable<PainelMetaAnualViewModel>> ConsultaPainelMetaAnual(Guid? MetaAnualId, Guid? MetaId, Guid? LinhaMetaId, Guid? GrupoPainelId, Guid? CargoId, Guid? UsuarioId, Guid? StatusId, Guid? PeriodoId);
        Task SalvarPainelMetaAnual(PainelMetaAnualViewModel meta);
    }
}