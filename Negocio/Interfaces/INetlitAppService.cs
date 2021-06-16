using CoBRA.Application.ViewModels;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface INetlitAppService
    {
        ConteudoPaginadoPPViewModel CarregarConteudoPP(int NumeroPagina, int RegistrosPagina, string Filtro);
        Task<ConteudoPaginadoPPViewModel> CarregarConteudoNetLitPP(int NumeroPagina, int RegistrosPagina, string Filtro);
        Task SalvarConteudoPP(ConteudoPPViewModel arquivo);
        Task ExcluirConteudoPP(int IdConteudo);
        CaminhoFlippingViewModel CarregarPastasFlippId(long idProduto);
        void SalvarCaminhoFlipp(CaminhoFlippingViewModel caminho);
    }
}
