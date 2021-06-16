using CoBRA.Application.ViewModels;

namespace CoBRA.Application.Interfaces
{
    public interface IRequisicaoGenericaAppService
    {
        object ObterRequisicoes();
        int GerarRequisicao(RequisicaoGenericaViewModel requisicao);
    }
}
