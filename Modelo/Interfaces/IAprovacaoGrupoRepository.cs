using CoBRA.Domain.Entities;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IAprovacaoGrupoRepository
    {
        Task AprovacaoPainelMeta(CabecalhoPainelMeta cabecalho);
    }
}
