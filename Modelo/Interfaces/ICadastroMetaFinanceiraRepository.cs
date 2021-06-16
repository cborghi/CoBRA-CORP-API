using System;
using System.Collections.Generic;
using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface ICadastroMetaFinanceiraRepository
    {
        Task<IEnumerable<PainelMetaFinanceira>> BuscarCadastroMetaFinanceira();
        Task<PainelMetaFinanceira> ConsultarCadastroMetaFinanceira(Guid? UsuarioId, Guid? MetaFinanceiraId);
        Task GravarMetaFinanceira(PainelMetaFinanceira meta);
        Task ModificarMetaFinanceira(PainelMetaFinanceira meta);

        Task<IEnumerable<PainelMetaFinanceira>> ListarMetaFinanceira(Guid? IdPeriodo);
    }
}
