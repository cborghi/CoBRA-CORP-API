using CoBRA.Application.ViewModels;
using System.Collections.Generic;

namespace CoBRA.Application.Interfaces
{
    public interface IRequisicaoElvisAppService
    {
        object ObterRequisicoes(string tipo);
        object ObterRequisicoesPorID(int ID);
        object ObterRequisicaoPorNota(string nota, int id);
        object ObterRequisicaoGeradaPorNota(string nota, int id);
        int InsertRequisicaoServico(int requisicao, string servico);
        List<FornecedorViewModel> ObterFornecedores(string nome);
        int InsertRequisicao(RequisicaoGeradaViewModel requisicao, bool elvis);
        int UpdateRequisicaoObraElvis(int contador, int requisicao, int usuarioId);
        int InsertRequisicaoObra(int requisicao, string obra, decimal valor);
        int UpdateRequisicaoObra(int requisicao, string obra, decimal valor);
        List<RequisicaoObraViewModel> ObterRequisicaoObra(int requisicao, string obra);
        int GerarRequisicao(RequisicaoGeradaViewModel requisicao, bool elvis);
        List<ServicoViewModel> ObterServicosPorCentroCusto(int idUsuario);
    }
}
