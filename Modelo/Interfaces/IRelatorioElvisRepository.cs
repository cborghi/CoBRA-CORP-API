using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface IRelatorioElvisRepository
    {
        Task<IList<RelatorioElvis>> ListarItensRelatorio();

        Task<IList<RelatorioElvis>> FiltrarItensRelatorio(FiltroRelatorioElvis filtro);

        Task<IList<ObraAndamento>> FiltrarRelatorioObrasAndamento(FiltroObraAndamento filtro);

        Task<IList<FiltroRelatorioElvis>> CarregarPrograma();

        Task<IList<FiltroRelatorioElvis>> CarregarProgramaObraAndamento();

        Task<IList<FiltroRelatorioElvis>> CarregarDisciplina();

        Task<IList<FiltroRelatorioElvis>> CarregarColecao();

        Task<IList<FiltroRelatorioElvis>> CarregarAnoEscolar();
    }
}