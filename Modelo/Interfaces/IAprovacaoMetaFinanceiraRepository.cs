using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IAprovacaoMetaFinanceiraRepository
    {
        Task AprovacaoPainelMeta(PainelMetaFinanceira painel);
    }
}
