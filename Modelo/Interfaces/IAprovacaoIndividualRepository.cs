using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IAprovacaoIndividualRepository
    {
        Task AprovacaoPainelMeta(PainelMetaAnual painel);
    }
}
