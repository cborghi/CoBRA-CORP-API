using CoBRA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IPainelRepository
    {
        Task GravarPainelMeta(CabecalhoPainelMeta painel);

        Task<IEnumerable<CabecalhoPainelMeta>> BuscarCabecalhoPainel(CabecalhoPainelMeta painelMeta, Guid? PeriodoId);

        Task<IEnumerable<CabecalhoPainelMeta>> FiltrarCabecalhoPainel(CabecalhoPainelMeta painelMeta);

        Task<IList<LinhaPainelMeta>> BuscarLinhaPainel(LinhaPainelMeta painelMeta);

        Task AtualizarLinhaPainel(LinhaPainelMeta linha);

        Task ExcluirLinhaPainel(LinhaPainelMeta linha);

        Task EnviarPainelAprovacao(CabecalhoPainelMeta cabecalho);

        Task ExcluirCabecalhoPainel(CabecalhoPainelMeta cabecalho);

        Task<byte> ObterPesoTotalMeta(CabecalhoPainelMeta painelMeta);

        Task<bool> VerificarMetaCadastrada(CabecalhoPainelMeta painelMeta);

        Task<CabecalhoRelatorioPainel> BuscarDadosCabecalhoRelatorio(Guid? idUsuario);

        Task<IEnumerable<PeriodoCampanha>> BuscarPeriodo();

        int BuscarPorcentagemMinimaPagamento();

        int BuscarPorcentagemPagamento(decimal? porcentagemRealizada);
    }
}
