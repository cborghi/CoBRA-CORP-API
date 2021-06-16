using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface ICadastroProdutoService
    {
        Task<string> GerarCodigoEbsa(ProdutoViewModel viewModel);
        Task<CadastroProdutoViewModel> CarregarInformacoesTela();
        Task SalvarCodigoEbsa(ProdutoViewModel viewModel);
        Task<ProdutosViewModel> Consultar(long ID);
        Task<IEnumerable<ProdutosDocumentosAutoriasViewModel>> Listar();
        Task<IEnumerable<ProdutoControleConteudoViewModel>> ListarControleConteudo(int IdEscola);
        Task<IEnumerable<AssuntosViewModel>> Assuntos();
        Task<IEnumerable<AutoresViewModel>> Autores();
        Task<IEnumerable<ConteudosViewModel>> Conteudos();
        Task BloquearEscola(int idEscola, int idProduto);
        Task<IEnumerable<ProdutosDocumentosAutoriasViewModel>> ListarIdEscola(int idEscola);
    }
}