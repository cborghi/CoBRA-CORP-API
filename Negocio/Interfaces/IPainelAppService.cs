using CoBRA.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IPainelAppService
    {

        Task InserirPainel(CabecalhoPainelMetaViewModel painel);
        Task<IEnumerable<CabecalhoPainelMetaViewModel>> BuscarPainel(CabecalhoPainelMetaViewModel painel, Guid? PeriodoId);
        Task<IEnumerable<CabecalhoPainelMetaViewModel>> FiltrarPainel(CabecalhoPainelMetaViewModel painel);
        Task<IList<LinhaPainelMetaViewModel>> BuscarLinhaPainel(LinhaPainelMetaViewModel painel);
        Task AtualizarLinhaPainel(LinhaPainelMetaViewModel painel);
        Task ExcluirLinhaPainel(LinhaPainelMetaViewModel painel);
        Task EnviarPainelAprovacao(CabecalhoPainelMetaViewModel cabecalho);
        Task ExcluirCabecalhoPainel(CabecalhoPainelMetaViewModel cabecalho);
        Task<byte> ObterPesoTotalMeta(CabecalhoPainelMetaViewModel cabecalho);
        Task<bool> VerificarMetaCadastrada(CabecalhoPainelMetaViewModel cabecalho);
        Task<List<PeriodoCampanhaViewModel>> BuscarPeriodo();
    }
}
