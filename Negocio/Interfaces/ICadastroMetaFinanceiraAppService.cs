using CoBRA.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface ICadastroMetaFinanceiraAppService
    {
        Task<AgrupadorMetaFinanceiraViewModel> ObterTodos();
        Task<MetaFinanceiraViewModel> ConsultarMetaFinanceira(Guid? UsuarioId, decimal? ValorMeta, Guid? MetaFinanceiraId);
        Task<MetaFinanceiraViewModel> AprovarItem(MetaFinanceiraViewModel dto);
        Task<MetaFinanceiraViewModel> GravarItem(MetaFinanceiraViewModel dto);
        Task<MetaFinanceiraViewModel> ModificarItem(MetaFinanceiraViewModel dto);
        Task<AgrupadorMetaFinanceiraViewModel> ListarMetaFinanceira(Guid? IdPeriodo);
     }
}
