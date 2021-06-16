using CoBRA.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application.Interfaces
{
    public interface IProdutoCUPAppService
    {
        ProdutoPaginadoCUPViewModel CarregarProdutosCUP(int NumeroPagina, int RegistrosPagina, int idUsuario, bool ebook);
        ProdutoPaginadoCUPViewModel CarregarProdutosFiltroCUP(int NumeroPagina, int RegistrosPagina, string Filtro, int idUsuario, bool ebook);
        ProdutoCUPViewModel CarregarProdutosIdCUP(long IdProduto, int IdUsuario);
        ProdutoRelatorioCUPViewModel CarregarProdutosRelatorioTituloCUP(string Titulo);
        Task<long> SalvarProdutoCUP(ProdutoCUPViewModel produto);
        Task AtualizarProdutoCUP(ProdutoCUPViewModel produto);
        Task ExcluirProdutoCUP(long IdProduto);
        Task PublicarProdutoCUP(long IdProduto);
        Task SalvarProdutoLinkCUP(long IdProduto, string Url, decimal? Preco);
        Task<string> GerarCodigoEbsa(ProdutoEBSAViewModel produto);
        void SalvarPreferenciasRelatorioCUP(string viewmodel, int idUsuario);
        void AtualizarPreferenciasRelatorioCUP(string viewmodel, int idUsuario);
        string CarregarPreferenciasRelatorioCUP(int idUsuario);
        Task SalvarArquivoCUP(ArquivoProdutoCUPViewModel arquivo);
        Task SalvarEpubCUP(ArquivoProdutoCUPViewModel arquivo);
        Task<int> SalvarArquivoAutoriaCUP(ArquivoProdutoCUPViewModel arquivo);
        Task AtualizarArquivoCUP(ArquivoProdutoCUPViewModel arquivo);
        Task ExcluirEpubCUP(long idProduto, string nomeArquivo);
        Task ExcluirArquivoAutoriaCUP(long IdAutoria);
        List<ArquivoAutoriaCUPViewModel> BuscarArquivoAutoriaCUP(long IdAutoria);
        List<AutoriaCUPViewModel> CarregarAutoriaPorIdProdutoCUP(long IdProduto);
        List<AutoresCUPViewModel> CarregarAutoresCUP(string NomeContratual);
        List<FuncoesCUPViewModel> CarregarFuncoesCUP();
        Task<long> SalvarAutoriaCUP(AutoriaCUPViewModel produto);
        Task AtualizarAutoriaCUP(AutoriaCUPViewModel produto);
        Task ExcluirAutoriaCUP(long IdAutoria);
        List<MercadoCUPViewModel> CarregarMercadoCUP();
        List<SeloCUPViewModel> CarregarSeloCUP();
        List<TipoCUPViewModel> CarregarTipoCUP();
        List<TipoEspecCUPViewModel> CarregarTipoEspecCUP(string TipoEspec);
        List<SegmentoCUPViewModel> CarregarSegmentoCUP();
        List<ComposicaoCUPViewModel> CarregarComposicaoCUP();
        List<AnoCUPViewModel> CarregarAnoCUP();
        List<AnoCUPViewModel> CarregarAnoPorIdSegmentoCUP(int IdSegmento);
        List<FaixaEtariaCUPViewModel> CarregarFaixaEtariaCUP();
        List<FaixaEtariaCUPViewModel> CarregarFaixaEtariaPorIdAnoCUP(int IdAno);
        List<DisciplinaCUPViewModel> CarregarDisciplinaCUP();
        List<ConteudoDisciplinarCUPViewModel> CarregarConteudoDisciplinarCUP();
        List<TemaCUPViewModel> CarregarTemaTransversalCUP();
        List<TemaCUPViewModel> CarregarTemaNorteadorCUP();
        List<DataEspecialCUPViewModel> CarregarDataEspecialCUP();
        List<AssuntoCUPViewModel> CarregarAssuntoCUP();
        List<GeneroTextualCUPViewModel> CarregarGeneroTextualCUP();
        List<VersaoCUPViewModel> CarregarVersaoCUP();
        List<ColecaoCUPViewModel> CarregarColecaoCUP();
        List<MidiaCUPViewModel> CarregarMidiaCUP();
        List<UnidadeMedidaCUPViewModel> CarregarUnidadeMedidaCUP();
        List<PlataformaCUPViewModel> CarregarPlataformaCUP();
        List<PlataformaCUPViewModel> CarregarPlataformaPorIdMidiaCUP(int IdMidia);
        List<StatusCUPViewModel> CarregarStatusCUP();
        List<OrigemCUPViewModel> CarregarOrigemCUP();
        List<TipoProdutoCUPViewModel> CarregarTipoProdutoCUP();
        List<SegmentoProtheusCUPViewModel> CarregarSegmentoProtheusCUP();

    }
}