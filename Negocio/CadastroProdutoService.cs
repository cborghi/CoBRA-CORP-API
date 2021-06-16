using AutoMapper;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class CadastroProdutoService : ICadastroProdutoService
    {
        private readonly ICadastroProdutoRepository _cadastroProdutoRepository;
        private readonly IIntranetRepository _intranetRepository;
        private readonly IMapper _mapper;

        public CadastroProdutoService(ICadastroProdutoRepository cadastroProdutoRepository, IIntranetRepository intranetRepository, IMapper mapper)
        {
            _cadastroProdutoRepository = cadastroProdutoRepository;
            _intranetRepository = intranetRepository;
            _mapper = mapper;
        }

        public async Task<CadastroProdutoViewModel> CarregarInformacoesTela()
        {
            return _mapper.Map<CadastroProdutoViewModel>(await _cadastroProdutoRepository.CarregarInformacoesTela());
        }

        public async Task<string> GerarCodigoEbsa(ProdutoViewModel viewModel)
        {
            int digito = await _cadastroProdutoRepository.BuscarDigitoSequencialProduto(_mapper.Map<Produto>(viewModel));
            string codigoEbsa = default;

            bool codigoExiste = false;

            do
            {

                codigoEbsa = string.Concat(
                    viewModel.Mercado,
                    viewModel.Ano,
                    viewModel.Disciplina.ToString("00"),
                    viewModel.Midia,
                    digito.ToString("00"),
                    viewModel.Tipo,
                    viewModel.Edicao,
                    viewModel.Versao);

                codigoExiste = await _cadastroProdutoRepository.CodigoEbsaExiste(codigoEbsa);

                if (codigoExiste)
                    digito++;

            } while (codigoExiste);

            return codigoEbsa;
        }

        public async Task SalvarCodigoEbsa(ProdutoViewModel viewModel)
        {
            await _cadastroProdutoRepository.SalvarCodigoEbsa(_mapper.Map<Produto>(viewModel));

            await _intranetRepository.CriarLivro(new Livro()
            {
                CODIGO = viewModel.CodigoEbsa,
                TITULO = viewModel.Titulo
            });

            await _intranetRepository.CriarEnvioObra(new EnvioObra()
            {
                OBRA = viewModel.CodigoEbsa
            });

            await _intranetRepository.CriarEnvioObraResumo(new EnvioObraResumo()
            {
                OBRA = viewModel.CodigoEbsa
            });
        }

        public async Task<ProdutosViewModel> Consultar(long ID)
        {
            return _mapper.Map<ProdutosViewModel>(await _cadastroProdutoRepository.Consultar(ID));
        }

        public async Task<IEnumerable<ProdutosDocumentosAutoriasViewModel>> Listar()
        {
            return _mapper.Map<IEnumerable<ProdutosDocumentosAutoriasViewModel>>(await _cadastroProdutoRepository.Listar());
        }

        public async Task<IEnumerable<ProdutosDocumentosAutoriasViewModel>> ListarIdEscola(int IdEscola)
        {
            return _mapper.Map<IEnumerable<ProdutosDocumentosAutoriasViewModel>>(await _cadastroProdutoRepository.ListarIdEscola(IdEscola));
        }

        public async Task<IEnumerable<ProdutoControleConteudoViewModel>> ListarControleConteudo(int IdEscola)
        {
            return _mapper.Map<IEnumerable<ProdutoControleConteudoViewModel>>(await _cadastroProdutoRepository.ListarControleConteudo(IdEscola));
        }

        public async Task BloquearEscola(int idEscola, int idProduto)
        {
            await _cadastroProdutoRepository.BloquearEscola(idEscola, idProduto);
        }

        public async Task<IEnumerable<AssuntosViewModel>> Assuntos()
        {
            return _mapper.Map<IEnumerable<AssuntosViewModel>>(await _cadastroProdutoRepository.Assuntos());
        }

        public async Task<IEnumerable<AutoresViewModel>> Autores()
        {
            return _mapper.Map<IEnumerable<AutoresViewModel>>(await _cadastroProdutoRepository.Autores());
        }

        public async Task<IEnumerable<ConteudosViewModel>> Conteudos()
        {
            return _mapper.Map<IEnumerable<ConteudosViewModel>>(await _cadastroProdutoRepository.Conteudos());
        }
    }
}
