using CoBRA.Domain.Entities;

namespace CoBRA.Domain.Interfaces
{
    public interface IBaseRepositoryRequisicaoElvis
    {
        int InserirRequisicao(RequisicaoGerada requisicao, bool elvis);
        int InserirRequisicaoObra(int requisicao, string obra, decimal valor);
        int InserirRequisicaoServico(int requisicao, string servico);
        int InserirParcelaRequisicao(string requisicaoID, ParcelaRequisicao parcela);
        int AlterarRequisicaoObra(int requisicao, string obra, decimal valor);
    }
}