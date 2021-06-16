using CoBRA.Domain.Entities;
using System.Collections.Generic;

namespace CoBRA.Domain.Interfaces
{
    public interface IRequisicaoGenericaRepository
    {
        List<RequisicaoGenerica> ObterRequisicoes();
        int InserirRequisicao(RequisicaoGenerica requisicao);
    }
}
