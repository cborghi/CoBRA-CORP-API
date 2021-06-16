using CoBRA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Domain.Interfaces
{
    public interface ICadastroProdutoRepository
    {
        Task<int> BuscarDigitoSequencialProduto(Produto produto);
        Task<CadastroProduto> CarregarInformacoesTela();
        Task<bool> CodigoEbsaExiste(string codigoEbsa);
        Task SalvarCodigoEbsa(Produto produto);
        Task<Produtos> Consultar(long id);
        Task<IEnumerable<ProdutosDocumentosAutorias>> Listar();
        Task<IEnumerable<ProdutoControleConteudo>> ListarControleConteudo(int IdEscola);
        Task<IEnumerable<Assuntos>> Assuntos();
        Task<IEnumerable<Autores>> Autores();
        Task<IEnumerable<Conteudos>> Conteudos();
        Task BloquearEscola(int idEscola, int idProduto);
        Task<IEnumerable<ProdutosDocumentosAutorias>> ListarIdEscola(int idEscola);
    }
}