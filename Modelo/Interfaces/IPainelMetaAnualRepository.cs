using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IPainelMetaAnualRepository
    {
        Task<IEnumerable<PainelMetaAnual>> ConsultaPainelMetaAnual(Guid? MetaAnualId, Guid? MetaId, Guid? LinhaMetaId, Guid? GrupoPainelId, Guid? CargoId, Guid? UsuarioId, Guid? StatusId, Guid? PeriodoId);
        Task SalvarPainelMetaAnual(PainelMetaAnual meta);
        Task<IEnumerable<PainelMetaAnual>> GestaoPessoaConsultaPainelMetaAnual(Guid? metaAnualId, Guid? statusId,
            Guid? usuarioId);

        Task<IEnumerable<PainelMetaAnual>> GestaoPessoaConsultaAprovacaoMetaAnual(Guid? usuarioId);

        Task<string> BuscarRegiaoUsuario(PainelMetaAnual meta);
    }
}