using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IRelatorioElvisAppService
    {
        Task<IList<RelatorioElvisViewModel>> ListarRelatorio();

        Task<IList<RelatorioElvisViewModel>> FiltrarRelatorio(FiltroRelatorioElvisViewModel filtro);

        Task<IList<ObraAndamentoViewModel>> FiltrarRelatorioObrasAndamento(FiltroObraAndamentoViewModel filtro);

        Task<IList<FiltroRelatorioElvisViewModel>> CarregarPrograma();

        Task<IList<FiltroRelatorioElvisViewModel>> CarregarProgramaObraAndamento();

        Task<IList<FiltroRelatorioElvisViewModel>> CarregarDisciplina();

        Task<IList<FiltroRelatorioElvisViewModel>> CarregarColecao();

        Task<IList<FiltroRelatorioElvisViewModel>> CarregarAnoEscolar();
    }
}