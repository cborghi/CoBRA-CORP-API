using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface IRequisicaoComprasAppService
    {

        List<RequisicaoComprasViewModel> ObterRequisicoes(int idUsuario);
        List<CentroCustoViewModel> ObterCentrosDeCusto();
        RequisicaoComprasViewModel ObterRequisicaoPorId(int id);
        RequisicaoViewModel BuscarRequisicao(int idRequisicao);
        List<ServicoViewModel> ObterServicos();
        List<ParcelaRequisicaoViewModel> ObterParcelasRequisicao(string idRequisicao);
        object ObterRequisicaoGeradaPorNota(string nota, int id);
        int AprovarRequisicao(RequisicaoAprovadaViewModel requisicao);
        int ReprovarRequisicao(RequisicaoAprovadaViewModel requisicao);
        int IncluirRequisicao(RequisicaoGerada requisicao, bool elvis);
        int IncluirRequisicaoObra(int requisicao, string obra, decimal valor);
        int IncluirRequisicaoServico(int requisicao, string servico);
        int GerarRequisicao(RequisicaoGerada requisicao);
        void UpdateRequisicao(RequisicaoAtualizada requisicao);
        void UpdateParcelasRequisicao(string requisicaoId, List<ParcelaRequisicao> parcelas);
        int AtualizarRequisicao(int id, string link);
        int ExcluirRequisicao(RequisicaoExcluidaViewModel requisicao);
        List<ObraViewModel> ObterObras(string Nome);
        void CancelaRequisicao(int idRequisicao, int idUsuario);
    }
}
