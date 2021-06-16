using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IAcompanhamentoMetaConsultorAppService
    {
        Task<AcompanhamentoMetaConsultorViewModel> ObterAcompanhamento(string idUsuario, string idStatus, Guid? PeriodoId);

        List<PainelMetaAnualViewModel> CalculoPotencial(Guid? MetaAnualId, Guid? MetaId, Guid? LinhaMetaId, Guid? GrupoPainelId, Guid? CargoId, Guid? UsuarioId, Guid? StatusId, Guid? PeriodoId);

        Task<CabecalhoRelatorioPainelViewModel> BuscarDadosCabecalhoRelatorio(Guid idUsuario);

        PainelMetaAnualGrupoViewModel CalculoPotencialGrupo(Guid? GrupoId, Guid? CargoId, Guid? PeriodoId);
    }
}
