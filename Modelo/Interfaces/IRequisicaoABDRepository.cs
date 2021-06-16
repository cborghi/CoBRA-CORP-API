using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IRequisicaoABDRepository
    {
        List<RequisicaoElvis> ObterRequisicoes(string tipo);
        List<RequisicaoElvis> ObterRequisicoesPorID(int ID);
        List<RequisicaoElvis> ObterRequisicaoPorNota(string nota);
        int InsertRequisicaoServico(int requisicao, string servico);
        int UpdateRequisicaoObraElvis(int contador, int requisicao, int usuarioId);
        int InsertRequisicao(RequisicaoGerada requisicao, bool elvis);
        int InsertRequisicaoObra(int requisicao, string obra, decimal valor);
        int UpdateRequisicaoObra(int requisicao, string obra, decimal valor);
        void ExcluirRequisicaoObra(int requisicaor);
        List<RequisicaoObra> ObterRequisicaoObra(int requisicao, string obra);
        List<RequisicaoElvis> ObterRequisicaoGeradaPorNota(string nota);
    }
}
