using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.Application
{
    public class ProdutoCUPAppService : IProdutoCUPAppService
    {
        private readonly IProdutoCUPRepository _produtoCUPRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProdutoCUPAppService(IProdutoCUPRepository produtoCUPRepository, IHttpContextAccessor httpContextAccessor)
        {
            _produtoCUPRepository = produtoCUPRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Produto

        public ProdutoPaginadoCUPViewModel CarregarProdutosCUP(int NumeroPagina, int RegistrosPagina, int idUsuario, bool ebook)
        {
            var Produtos = _produtoCUPRepository.CarregarProdutosCUP(NumeroPagina, RegistrosPagina, ebook);
            var Retorno = new ProdutoPaginadoCUPViewModel();

            Retorno.Contagem = Produtos.Contagem;
            Retorno.LstProdutos = new List<ProdutoBaseCUPViewModel>();
            foreach (var item in Produtos.LstProdutos)
            {
                var ret = new ProdutoBaseCUPViewModel
                {
                    IdProduto = item.IdProduto,
                    Titulo = item.Titulo,
                    ISBN = item.ISBN,
                    EBSA = item.EBSA,
                    Publicado = item.Publicado,
                    Integrado = item.Integrado,
                    AnoEducacao = item.AnoEducacao,
                    Url = item.Url,
                    Preco = item.Preco,
                };
                Retorno.LstProdutos.Add(ret);
            }
            Retorno.RegistrosPagina = RegistrosPagina;
            Retorno.NumeroPagina = NumeroPagina;

            int resultado = System.Math.DivRem(Retorno.Contagem, RegistrosPagina, out int resto);

            Retorno.QtdePaginas = resto != 0 ? resultado + 1 : resultado;

            Retorno.lstAbaPermissao = new List<AbaCUPViewModel>();
            var abaPermissao = _produtoCUPRepository.CarregarPermissoesAbaUsuarioCUP(idUsuario);
            foreach (var ab in abaPermissao)
            {
                AbaCUPViewModel aba = new AbaCUPViewModel();
                aba.AcessoModificacao = ab.AcessoModificacao;
                aba.AcessoVisualizacao = ab.AcessoVisualizacao;
                aba.NomeAba = ab.NomeAba;

                Retorno.lstAbaPermissao.Add(aba);
            }

            return Retorno;
        }

        public ProdutoPaginadoCUPViewModel CarregarProdutosFiltroCUP(int NumeroPagina, int RegistrosPagina, string Filtro, int idUsuario, bool ebook)
        {
            var Produtos = _produtoCUPRepository.CarregarProdutosFiltroCUP(NumeroPagina, RegistrosPagina, Filtro, ebook);
            var Retorno = new ProdutoPaginadoCUPViewModel();

            Retorno.Contagem = Produtos.Contagem;
            Retorno.LstProdutos = new List<ProdutoBaseCUPViewModel>();
            foreach (var item in Produtos.LstProdutos)
            {
                var ret = new ProdutoBaseCUPViewModel
                {
                    IdProduto = item.IdProduto,
                    Titulo = item.Titulo,
                    ISBN = item.ISBN,
                    EBSA = item.EBSA,
                    AnoEducacao = item.AnoEducacao,
                    Publicado = item.Publicado,
                    Integrado = item.Integrado,
                    Url = item.Url,
                    Preco = item.Preco,
                };
                Retorno.LstProdutos.Add(ret);
            }
            Retorno.RegistrosPagina = RegistrosPagina;
            Retorno.NumeroPagina = NumeroPagina;

            int resultado = System.Math.DivRem(Retorno.Contagem, RegistrosPagina, out int resto);

            Retorno.QtdePaginas = resto != 0 ? resultado + 1 : resultado;

            Retorno.lstAbaPermissao = new List<AbaCUPViewModel>();
            var abaPermissao = _produtoCUPRepository.CarregarPermissoesAbaUsuarioCUP(idUsuario);
            foreach (var ab in abaPermissao)
            {
                AbaCUPViewModel aba = new AbaCUPViewModel();
                aba.AcessoModificacao = ab.AcessoModificacao;
                aba.AcessoVisualizacao = ab.AcessoVisualizacao;
                aba.NomeAba = ab.NomeAba;

                Retorno.lstAbaPermissao.Add(aba);
            }

            return Retorno;
        }

        public ProdutoCUPViewModel CarregarProdutosIdCUP(long IdProduto, int IdUsuario)
        {
            var produto = _produtoCUPRepository.CarregarProdutosIdCUP(IdProduto);
            ProdutoCUPViewModel prod = new ProdutoCUPViewModel();
            prod.IdProduto = IdProduto;
            prod.Mercado = produto.Mercado.ToString();
            prod.Programa = produto.Programa;
            prod.AnoPrograma = produto.AnoPrograma;
            prod.Selo = produto.Selo;
            prod.Tipo = produto.Tipo;
            prod.Segmento = produto.Segmento;
            prod.Ano = produto.Ano;
            prod.FaixaEtaria = produto.FaixaEtaria;
            prod.Composicao = produto.Composicao;
            prod.Disciplina = produto.Disciplina;
            prod.TemaTransversal = produto.TemaTransversal;
            prod.Assunto = new List<AssuntoCUPViewModel>();
            prod.TemasNorteadores = new List<TemaCUPViewModel>();
            prod.GeneroTextual = produto.GeneroTextual;
            prod.ConteudoDisciplinar = produto.ConteudoDisciplinar;
            prod.DatasEspeciais = new List<DataEspecialCUPViewModel>();
            prod.Premiacao = produto.Premiacao;
            prod.Versao = produto.Versao;
            prod.Colecao = string.IsNullOrEmpty(produto.Colecao) ? -1 : Convert.ToInt32(produto.Colecao);
            prod.Titulo = produto.Titulo;
            prod.Edicao = produto.Edicao;
            prod.Midia = produto.Midia;
            prod.UnidadeMedida = produto.UnidadeMedida == 0 ? -1 : produto.UnidadeMedida;
            prod.Plataforma = string.IsNullOrEmpty(produto.Plataforma) ? -1 : Convert.ToInt32(produto.Plataforma);
            if(produto.DataPublicacao == null)
            {
                prod.DataPublicacao = "";
            }
            else
            {
                DateTime dt = Convert.ToDateTime(produto.DataPublicacao);
                string dtEdit = dt.ToString("MM/yyyy");
                prod.DataPublicacao = dtEdit;
            }
            prod.ISBN = produto.ISBN;
            prod.Status = string.IsNullOrEmpty(produto.Status) ? -1 : Convert.ToInt32(produto.Status);
            prod.CodigoBarras = produto.CodigoBarras;
            prod.Sinopse = produto.Sinopse;
            prod.EBSA = produto.EBSA;
            prod.NomeCapa = produto.NomeCapa;
            prod.Origem = produto.Origem;
            prod.TipoProduto = produto.TipoProduto == 0 ? -1 : produto.TipoProduto;
            prod.SegmentoProtheus = produto.SegmentoProtheus == 0 ? -1 : produto.SegmentoProtheus;
            if(!string.IsNullOrEmpty(produto.DataPublicacaoProtheus))
            {
                DateTime dt = Convert.ToDateTime(produto.DataPublicacaoProtheus);
                string dtFormat = dt.ToString("dd/MM/yyyy hh:mm");
                prod.UltimaIntegracaoProtheus = dtFormat;
            }
            else
            {
                prod.UltimaIntegracaoProtheus = "";
            }
            prod.Miolo = new MioloCUPViewModel();
            prod.Capa = new CapaCUPVIewModel();
            prod.Cadernos = new List<CadernoCUPViewModel>();
            prod.Encartes = new List<EncarteCUPViewModel>();
            prod.MidiaFicha = new MidiaFichaCUPViewModel();
            prod.CtrlImpressao = new List<ControleImpressaoCUPViewModel>();
            prod.Reformulado = produto.Reformulado;
            var assunto = _produtoCUPRepository.CarregarAssuntoIdProdutoCUP(IdProduto);
            if (assunto.Count > 0)
            {
                foreach (var assuntoCUP in assunto)
                {
                    AssuntoCUPViewModel ass = new AssuntoCUPViewModel();
                    ass.IdAssunto = assuntoCUP.Id_Assunto;
                    ass.DescricaoAssunto = assuntoCUP.Descricao_Assunto;
                    prod.Assunto.Add(ass);
                }
            }

            var temasNorteadores = _produtoCUPRepository.CarregarTemaIdProdutoCUP(IdProduto);
            if (temasNorteadores.Count > 0)
            {
                foreach (var temasCUP in temasNorteadores)
                {
                    TemaCUPViewModel tem = new TemaCUPViewModel();
                    tem.IdTema = temasCUP.Id_Tema;
                    tem.DescricaoTema = temasCUP.Descricao_Tema;
                    tem.TipoTema = temasCUP.Tipo_Tema;
                    prod.TemasNorteadores.Add(tem);
                }
            }

            var datasEsp = _produtoCUPRepository.CarregarDatasEspeciaisIdProdutoCUP(IdProduto);
            if (datasEsp.Count > 0)
            {
                foreach (var datasCUP in datasEsp)
                {
                    DataEspecialCUPViewModel dat = new DataEspecialCUPViewModel();
                    dat.IdDataEspecial = datasCUP.Id_DataEspecial;
                    dat.DescricaoDataEspecial = datasCUP.Descricao_DataEspecial;
                    dat.DiaDataEspecial = datasCUP.Dia_DataEspecial;
                    prod.DatasEspeciais.Add(dat);
                }
            }

            var miolo = _produtoCUPRepository.CarregarMioloIdProdutoCUP(IdProduto);
            if (miolo != null)
            {
                prod.Miolo = new MioloCUPViewModel
                {
                    IdMiolo = miolo.IdMiolo,
                    IdProduto = miolo.IdProduto,
                    mioloPaginas = miolo.mioloPaginas,
                    mioloFormatoAltura = miolo.mioloFormatoAltura,
                    mioloFormatoLargura = miolo.mioloFormatoLargura,
                    mioloPeso = miolo.mioloPeso,
                    mioloCores = miolo.mioloCores,
                    mioloTipoPapel = miolo.mioloTipoPapel,
                    mioloGramatura = miolo.mioloGramatura,
                    mioloObservacoes = miolo.mioloObservacoes
                };
            }

            var capa = _produtoCUPRepository.CarregarCapaIdProdutoCUP(IdProduto);
            if (capa != null)
            {
                prod.Capa = new CapaCUPVIewModel
                {
                    IdCapa = capa.IdCapa,
                    IdProduto = capa.IdProduto,
                    CapaCores = capa.CapaCores,
                    CapaTipoPapel = capa.CapaTipoPapel,
                    CapaGramatura = capa.CapaGramatura,
                    CapaOrelha = capa.CapaOrelha,
                    CapaObservacoes = capa.CapaObservacoes,
                    CapaAcabamento = capa.CapaAcabamento,
                    CapaAcabamentoLombada = capa.CapaAcabamentoLombada,
                };
            }

            var cadernos = _produtoCUPRepository.CarregarCadernoIdProdutoCUP(IdProduto);
            if (cadernos.Count > 0)
            {
                foreach (var caderno in cadernos)
                {
                    CadernoCUPViewModel cad = new CadernoCUPViewModel();
                    cad.IdCaderno = caderno.IdCaderno;
                    cad.IdProduto = caderno.IdProduto;
                    cad.CadernoTipo = caderno.CadernoTipo;
                    cad.CadernoTipoOutros = caderno.CadernoTipoOutros;
                    cad.CadernoPaginas = caderno.CadernoPaginas;
                    cad.CadernoAltura = caderno.CadernoAltura;
                    cad.CadernoLargura = caderno.CadernoLargura;
                    cad.CadernoPeso = caderno.CadernoPeso;
                    cad.CadernoMioloCores = caderno.CadernoMioloCores;
                    cad.CadernoMioloTipoPapel = caderno.CadernoMioloTipoPapel;
                    cad.CadernoMioloGramatura = caderno.CadernoMioloGramatura;
                    cad.CadernoMioloObservacoes = caderno.CadernoMioloObservacoes;
                    cad.CadernoCapaCores = caderno.CadernoCapaCores;
                    cad.CadernoCapaTipoPapel = caderno.CadernoCapaTipoPapel;
                    cad.CadernoCapaGramatura = caderno.CadernoCapaGramatura;
                    cad.CadernoCapaOrelha = caderno.CadernoCapaOrelha;
                    cad.CadernoCapaAcabamento = caderno.CadernoCapaAcabamento;
                    cad.CadernoCapaObservacoes = caderno.CadernoCapaObservacoes;
                    cad.CadernoCapaAcabamentoLombada = caderno.CadernoCapaAcabamentoLombada;
                    prod.Cadernos.Add(cad);
                }
            }

            var encartes = _produtoCUPRepository.CarregarEncarteIdProdutoCUP(IdProduto);
            if (encartes.Count > 0)
            {
                foreach (var encarte in encartes)
                {
                    EncarteCUPViewModel enc = new EncarteCUPViewModel();
                    enc.IdEncarte = encarte.IdEncarte;
                    enc.IdProduto = encarte.IdProduto;
                    enc.EncarteTipo = encarte.EncarteTipo;
                    enc.EncarteAcabamento = encarte.EncarteAcabamento;
                    enc.EncartePapel = encarte.EncartePapel;
                    enc.EncarteGramatura = encarte.EncarteGramatura;
                    enc.EncarteFormato = encarte.EncarteFormato;
                    enc.EncarteCor = encarte.EncarteCor;
                    enc.EncarteOutros = encarte.EncarteOutros;
                    enc.EncartePaginas = encarte.EncartePaginas;
                    prod.Encartes.Add(enc);
                }
            }

            var midia = _produtoCUPRepository.CarregarMidiaIdProdutoCUP(IdProduto);
            if (midia != null)
            {
                prod.MidiaFicha = new MidiaFichaCUPViewModel();
                prod.MidiaFicha.IdMidia = midia.IdMidia;
                prod.MidiaFicha.IdProduto = midia.IdProduto;
                prod.MidiaFicha.MidiaTipo = midia.MidiaTipo;
                prod.MidiaFicha.MidiaOutros = midia.MidiaOutros;
            }

            var impressoes = _produtoCUPRepository.CarregarImpressaoIdProdutoCUP(IdProduto);
            if (impressoes.Count > 0)
            {
                foreach (var imp in impressoes)
                {
                    ControleImpressaoCUPViewModel impressao = new ControleImpressaoCUPViewModel();
                    impressao.IdControleImpressao = imp.IdControleImpressao;
                    impressao.IdProduto = imp.IdProduto;
                    impressao.ContImpEdicao = imp.ContImpEdicao;
                    impressao.ContImpImpressao = imp.ContImpImpressao;
                    impressao.ContImpGrafica = imp.ContImpGrafica;
                    if (imp.ContImpData == null)
                    {
                        impressao.ContImpData = "";
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(imp.ContImpData);
                        string dtEdit = dt.ToString("MM/yyyy");
                        impressao.ContImpData = dtEdit;
                    }
                    impressao.ContImpTiragem = imp.ContImpTiragem;
                    impressao.ContImpObservacoesGerais = imp.ContImpObservacoesGerais;
                    prod.CtrlImpressao.Add(impressao);
                }
            }

            prod.lstAbaPermissao = new List<AbaCUPViewModel>();
            var abaPermissao = _produtoCUPRepository.CarregarPermissoesAbaUsuarioCUP(IdUsuario);
            foreach (var ab in abaPermissao)
            {
                AbaCUPViewModel aba = new AbaCUPViewModel();
                aba.AcessoModificacao = ab.AcessoModificacao;
                aba.AcessoVisualizacao = ab.AcessoVisualizacao;
                aba.NomeAba = ab.NomeAba;

                prod.lstAbaPermissao.Add(aba);
            }

            var image = _produtoCUPRepository.CarregarCaminhoImagemCUP(IdProduto);
            if (File.Exists(image))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(image);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                prod.UrlImagem = base64ImageRepresentation;
            }

            prod.EPUB = new List<ArquivoProdutoCUPViewModel>();
            var epub = _produtoCUPRepository.CarregarCaminhoEpubCUP(prod.IdProduto);
            foreach (var item in epub)
            {
                ArquivoProdutoCUPViewModel arq = new ArquivoProdutoCUPViewModel();
                arq.IdProdutoEPUB = item.IdProdutoEPUB;
                arq.IdProduto = item.IdProduto;
                arq.ProdutoEBSA = item.ProdutoEBSA;
                arq.NomeArquivo = item.NomeArquivo;
                arq.CaminhoArquivo = item.CaminhoArquivo;
                arq.DataCadastro = item.DataCadastro;
                prod.EPUB.Add(arq);
            }

            return prod;
        }

        public ProdutoRelatorioCUPViewModel CarregarProdutosRelatorioTituloCUP(string Titulo)
        {
            var produtoRel = _produtoCUPRepository.CarregarProdutosRelatorioCabecalhoCUP(Titulo);
            ProdutoRelatorioCUPViewModel rel = new ProdutoRelatorioCUPViewModel();
            rel.Titulo = produtoRel[0].Titulo;
            rel.Colecao = _produtoCUPRepository.CarregarColecaoCUP().Where(a => a.Id_Colecao == Convert.ToInt32(produtoRel[0].Colecao)).FirstOrDefault().Descricao_Colecao;
            rel.Edicao = produtoRel[0].Edicao.ToString();
            rel.Mercado = _produtoCUPRepository.CarregarMercadoCUP().Where(a => a.Codigo_Mercado == produtoRel[0].Mercado.ToString()).FirstOrDefault().Descricao_Mercado;
            rel.PNLD = produtoRel[0].AnoPrograma.ToString();
            rel.Tipo = _produtoCUPRepository.CarregarTipoCUP().Where(a => a.Id_Tipo == produtoRel[0].Tipo).FirstOrDefault().Descricao_Tipo;
            rel.Livros = new List<ProdutoCUPViewModel>();

            foreach (var produto in produtoRel)
            {
                ProdutoCUPViewModel prod = new ProdutoCUPViewModel();
                prod.IdProduto = produto.IdProduto;
                prod.Mercado = produto.Mercado.ToString();
                prod.Programa = produto.Programa;
                prod.AnoPrograma = produto.AnoPrograma;
                prod.Selo = produto.Selo;
                prod.Tipo = produto.Tipo;
                prod.Segmento = produto.Segmento;
                prod.Ano = produto.Ano;
                prod.FaixaEtaria = produto.FaixaEtaria;
                prod.Composicao = produto.Composicao;
                prod.Disciplina = produto.Disciplina;
                prod.TemaTransversal = produto.TemaTransversal;
                prod.Assunto = new List<AssuntoCUPViewModel>();
                prod.TemasNorteadores = new List<TemaCUPViewModel>();
                prod.GeneroTextual = produto.GeneroTextual;
                prod.ConteudoDisciplinar = produto.ConteudoDisciplinar;
                prod.DatasEspeciais = new List<DataEspecialCUPViewModel>();
                prod.Premiacao = produto.Premiacao;
                prod.Versao = produto.Versao;
                prod.Colecao = string.IsNullOrEmpty(produto.Colecao) ? 0 : Convert.ToInt32(produto.Colecao);
                prod.Titulo = produto.Titulo;
                prod.Edicao = produto.Edicao;
                prod.Midia = produto.Midia;
                prod.UnidadeMedida = produto.UnidadeMedida;
                prod.Plataforma = string.IsNullOrEmpty(produto.Plataforma) ? 0 : Convert.ToInt32(produto.Plataforma);
                if (produto.DataPublicacao == null)
                {
                    prod.DataPublicacao = "";
                }
                else
                {
                    DateTime dt = Convert.ToDateTime(produto.DataPublicacao);
                    string dtEdit = dt.ToString("MM/yyyy");
                    prod.DataPublicacao = dtEdit;
                }
                prod.ISBN = produto.ISBN;
                prod.Status = string.IsNullOrEmpty(produto.Status) ? 0 : Convert.ToInt32(produto.Status);
                prod.CodigoBarras = produto.CodigoBarras;
                prod.Sinopse = produto.Sinopse;
                prod.EBSA = produto.EBSA;
                prod.NomeCapa = produto.NomeCapa;
                prod.Origem = produto.Origem;
                prod.TipoProduto = produto.TipoProduto;
                prod.SegmentoProtheus = produto.SegmentoProtheus;
                if (!string.IsNullOrEmpty(produto.DataPublicacaoProtheus))
                {
                    DateTime dt = Convert.ToDateTime(produto.DataPublicacaoProtheus);
                    string dtFormat = dt.ToString("dd/MM/yyyy hh:mm");
                    prod.UltimaIntegracaoProtheus = dtFormat;
                }
                else
                {
                    prod.UltimaIntegracaoProtheus = "";
                }
                prod.Miolo = new MioloCUPViewModel();
                prod.Capa = new CapaCUPVIewModel();
                prod.Cadernos = new List<CadernoCUPViewModel>();
                prod.Encartes = new List<EncarteCUPViewModel>();
                prod.MidiaFicha = new MidiaFichaCUPViewModel();
                prod.CtrlImpressao = new List<ControleImpressaoCUPViewModel>();
                var assunto = _produtoCUPRepository.CarregarAssuntoIdProdutoCUP(produto.IdProduto);
                if (assunto.Count > 0)
                {
                    foreach (var assuntoCUP in assunto)
                    {
                        AssuntoCUPViewModel ass = new AssuntoCUPViewModel();
                        ass.IdAssunto = assuntoCUP.Id_Assunto;
                        ass.DescricaoAssunto = assuntoCUP.Descricao_Assunto;
                        prod.Assunto.Add(ass);
                    }
                }

                var temasNorteadores = _produtoCUPRepository.CarregarTemaIdProdutoCUP(produto.IdProduto);
                if (temasNorteadores.Count > 0)
                {
                    foreach (var temasCUP in temasNorteadores)
                    {
                        TemaCUPViewModel tem = new TemaCUPViewModel();
                        tem.IdTema = temasCUP.Id_Tema;
                        tem.DescricaoTema = temasCUP.Descricao_Tema;
                        tem.TipoTema = temasCUP.Tipo_Tema;
                        prod.TemasNorteadores.Add(tem);
                    }
                }

                var datasEsp = _produtoCUPRepository.CarregarDatasEspeciaisIdProdutoCUP(produto.IdProduto);
                if (datasEsp.Count > 0)
                {
                    foreach (var datasCUP in datasEsp)
                    {
                        DataEspecialCUPViewModel dat = new DataEspecialCUPViewModel();
                        dat.IdDataEspecial = datasCUP.Id_DataEspecial;
                        dat.DescricaoDataEspecial = datasCUP.Descricao_DataEspecial;
                        dat.DiaDataEspecial = datasCUP.Dia_DataEspecial;
                        prod.DatasEspeciais.Add(dat);
                    }
                }

                var miolo = _produtoCUPRepository.CarregarMioloIdProdutoCUP(produto.IdProduto);
                if (miolo != null)
                {
                    prod.Miolo = new MioloCUPViewModel
                    {
                        IdMiolo = miolo.IdMiolo,
                        IdProduto = miolo.IdProduto,
                        mioloPaginas = miolo.mioloPaginas,
                        mioloFormatoAltura = miolo.mioloFormatoAltura,
                        mioloFormatoLargura = miolo.mioloFormatoLargura,
                        mioloPeso = miolo.mioloPeso,
                        mioloCores = miolo.mioloCores,
                        mioloTipoPapel = miolo.mioloTipoPapel,
                        mioloGramatura = miolo.mioloGramatura,
                        mioloObservacoes = miolo.mioloObservacoes
                    };
                }

                var capa = _produtoCUPRepository.CarregarCapaIdProdutoCUP(produto.IdProduto);
                if (capa != null)
                {
                    prod.Capa = new CapaCUPVIewModel
                    {
                        IdCapa = capa.IdCapa,
                        IdProduto = capa.IdProduto,
                        CapaCores = capa.CapaCores,
                        CapaTipoPapel = capa.CapaTipoPapel,
                        CapaGramatura = capa.CapaGramatura,
                        CapaOrelha = capa.CapaOrelha,
                        CapaObservacoes = capa.CapaObservacoes,
                        CapaAcabamento = capa.CapaAcabamento,
                        CapaAcabamentoLombada = capa.CapaAcabamentoLombada,
                    };
                }

                var cadernos = _produtoCUPRepository.CarregarCadernoIdProdutoCUP(produto.IdProduto);
                if (cadernos.Count > 0)
                {
                    foreach (var caderno in cadernos)
                    {
                        CadernoCUPViewModel cad = new CadernoCUPViewModel();
                        cad.IdCaderno = caderno.IdCaderno;
                        cad.IdProduto = caderno.IdProduto;
                        cad.CadernoTipo = caderno.CadernoTipo;
                        cad.CadernoTipoOutros = caderno.CadernoTipoOutros;
                        cad.CadernoPaginas = caderno.CadernoPaginas;
                        cad.CadernoAltura = caderno.CadernoAltura;
                        cad.CadernoLargura = caderno.CadernoLargura;
                        cad.CadernoPeso = caderno.CadernoPeso;
                        cad.CadernoMioloCores = caderno.CadernoMioloCores;
                        cad.CadernoMioloTipoPapel = caderno.CadernoMioloTipoPapel;
                        cad.CadernoMioloGramatura = caderno.CadernoMioloGramatura;
                        cad.CadernoMioloObservacoes = caderno.CadernoMioloObservacoes;
                        cad.CadernoCapaCores = caderno.CadernoCapaCores;
                        cad.CadernoCapaTipoPapel = caderno.CadernoCapaTipoPapel;
                        cad.CadernoCapaGramatura = caderno.CadernoCapaGramatura;
                        cad.CadernoCapaOrelha = caderno.CadernoCapaOrelha;
                        cad.CadernoCapaAcabamento = caderno.CadernoCapaAcabamento;
                        cad.CadernoCapaObservacoes = caderno.CadernoCapaObservacoes;
                        cad.CadernoCapaAcabamentoLombada = caderno.CadernoCapaAcabamentoLombada;
                        prod.Cadernos.Add(cad);
                    }
                }

                var encartes = _produtoCUPRepository.CarregarEncarteIdProdutoCUP(produto.IdProduto);
                if (encartes.Count > 0)
                {
                    foreach (var encarte in encartes)
                    {
                        EncarteCUPViewModel enc = new EncarteCUPViewModel();
                        enc.IdEncarte = encarte.IdEncarte;
                        enc.IdProduto = encarte.IdProduto;
                        enc.EncarteTipo = encarte.EncarteTipo;
                        enc.EncarteAcabamento = encarte.EncarteAcabamento;
                        enc.EncartePapel = encarte.EncartePapel;
                        enc.EncarteGramatura = encarte.EncarteGramatura;
                        enc.EncarteFormato = encarte.EncarteFormato;
                        enc.EncarteCor = encarte.EncarteCor;
                        enc.EncarteOutros = encarte.EncarteOutros;
                        enc.EncartePaginas = encarte.EncartePaginas;
                        prod.Encartes.Add(enc);
                    }
                }

                var midia = _produtoCUPRepository.CarregarMidiaIdProdutoCUP(produto.IdProduto);
                if (midia != null)
                {
                    prod.MidiaFicha = new MidiaFichaCUPViewModel();
                    prod.MidiaFicha.IdMidia = midia.IdMidia;
                    prod.MidiaFicha.IdProduto = midia.IdProduto;
                    prod.MidiaFicha.MidiaTipo = midia.MidiaTipo;
                    prod.MidiaFicha.MidiaOutros = midia.MidiaOutros;
                }

                var impressoes = _produtoCUPRepository.CarregarImpressaoIdProdutoCUP(produto.IdProduto);
                if (impressoes.Count > 0)
                {
                    foreach (var imp in impressoes)
                    {
                        ControleImpressaoCUPViewModel impressao = new ControleImpressaoCUPViewModel();
                        impressao.IdControleImpressao = imp.IdControleImpressao;
                        impressao.IdProduto = imp.IdProduto;
                        impressao.ContImpEdicao = imp.ContImpEdicao;
                        impressao.ContImpImpressao = imp.ContImpImpressao;
                        impressao.ContImpGrafica = imp.ContImpGrafica;
                        if (imp.ContImpData == null)
                        {
                            impressao.ContImpData = "";
                        }
                        else
                        {
                            DateTime dt = Convert.ToDateTime(imp.ContImpData);
                            string dtEdit = dt.ToString("MM/yyyy");
                            impressao.ContImpData = dtEdit;
                        }
                        impressao.ContImpTiragem = imp.ContImpTiragem;
                        impressao.ContImpObservacoesGerais = imp.ContImpObservacoesGerais;
                        prod.CtrlImpressao.Add(impressao);
                    }
                }
                rel.Livros.Add(prod);
            }

            rel.Autores = new List<AutoresCUPViewModel>();
            var aut = _produtoCUPRepository.CarregarAutoresProdIdCUP(produtoRel[0].IdProduto);
            foreach (var a in aut)
            {
                AutoresCUPViewModel a1 = new AutoresCUPViewModel();
                a1.IdAutor = a.IdAutor;
                a1.NomeContratual = a.NomeContratual;
                a1.CapaCodAutor = a.CapaCodAutor;
                rel.Autores.Add(a1);
            }

            var image = _produtoCUPRepository.CarregarCaminhoImagemCUP(produtoRel[0].IdProduto);
            if (File.Exists(image))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(image);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                rel.Capa = base64ImageRepresentation;
            }

            rel.EPUB = new List<ArquivoProdutoCUPViewModel>();
            var epub = _produtoCUPRepository.CarregarCaminhoEpubCUP(produtoRel[0].IdProduto);
            foreach (var item in epub)
            {
                ArquivoProdutoCUPViewModel arq = new ArquivoProdutoCUPViewModel();
                arq.IdProdutoEPUB = item.IdProdutoEPUB;
                arq.IdProduto = item.IdProduto;
                arq.ProdutoEBSA = item.ProdutoEBSA;
                arq.NomeArquivo = item.NomeArquivo;
                arq.CaminhoArquivo = item.CaminhoArquivo;
                arq.DataCadastro = item.DataCadastro;
                rel.EPUB.Add(arq);
            }

            return rel;
        }

        public async Task<string> GerarCodigoEbsa(ProdutoEBSAViewModel produto)
        {
            Produto prod = new Produto
            {
                Mercado = produto.Mercado,
                Ano = produto.Ano,
                Disciplina = produto.Disciplina,
                Midia = produto.Midia,
                Tipo = produto.Tipo,
                Edicao = produto.Edicao,
                Versao = produto.Versao,
                CodigoEbsa = produto.CodigoEbsa,
                Titulo = produto.Titulo
            };
            int digito = await _produtoCUPRepository.BuscarDigitoSequencialProduto(prod);
            string codigoEbsa = default;
            bool codigoExiste = false;

            do
            {
                codigoEbsa = string.Concat(
                    produto.Mercado,
                    produto.Ano,
                    produto.Disciplina.ToString("00"),
                    produto.Midia,
                    digito.ToString("00"),
                    produto.Tipo,
                    produto.Edicao,
                    produto.Versao);

                codigoExiste = await _produtoCUPRepository.CodigoEbsaExiste(codigoEbsa);

                if (codigoExiste)
                    digito++;

            } while (codigoExiste);

            return codigoEbsa;
        }

        public async Task<long> SalvarProdutoCUP(ProdutoCUPViewModel produto)
        {
            ProdutoCUP prod = new ProdutoCUP();
            prod.Mercado = Convert.ToInt32(produto.Mercado);
            prod.Programa = produto.Programa;
            prod.AnoPrograma = produto.AnoPrograma;
            prod.Selo = produto.Selo;
            prod.Tipo = produto.Tipo;
            prod.Segmento = produto.Segmento;
            prod.Ano = produto.Ano;
            prod.FaixaEtaria = produto.FaixaEtaria;
            prod.Composicao = produto.Composicao;
            prod.Disciplina = produto.Disciplina;
            prod.TemaTransversal = produto.TemaTransversal;
            if(string.IsNullOrEmpty(produto.DataPublicacao))
            {
                prod.DataPublicacao = null;

            }
            else
            {
                prod.DataPublicacao = Convert.ToDateTime(produto.DataPublicacao);
            }
            prod.LstAssunto = new List<AssuntoCUP>();
            prod.LstTemaNorteador = new List<TemaCUP>();
            prod.GeneroTextual = produto.GeneroTextual;
            prod.ConteudoDisciplinar = produto.ConteudoDisciplinar;
            prod.LstDatasEspeciais = new List<DataEspecialCUP>();
            prod.Premiacao = produto.Premiacao;
            prod.Versao = produto.Versao;
            prod.Colecao = produto.Colecao.ToString();
            prod.Titulo = produto.Titulo;
            prod.Edicao = produto.Edicao;
            prod.Midia = produto.Midia;
            prod.UnidadeMedida = produto.UnidadeMedida;
            prod.Plataforma = produto.Plataforma.ToString();
            prod.ISBN = produto.ISBN;
            prod.Status = produto.Status.ToString();
            prod.CodigoBarras = produto.CodigoBarras;
            prod.Sinopse = produto.Sinopse;
            prod.EBSA = produto.EBSA;
            prod.NomeCapa = produto.NomeCapa;
            prod.Origem = produto.Origem;
            prod.TipoProduto = produto.TipoProduto;
            prod.SegmentoProtheus = produto.SegmentoProtheus;
            prod.DataPublicacaoProtheus = null;
            prod.Reformulado = produto.Reformulado;

            prod.IdProduto = await _produtoCUPRepository.SalvarProdutoCUP(prod);

            if (produto.Assunto != null)
            {
                foreach (var assunto in produto.Assunto)
                {
                    await _produtoCUPRepository.SalvarAssuntoProdutoCUP(assunto.IdAssunto, prod.IdProduto);
                }
            }

            if (produto.TemasNorteadores != null)
            {
                foreach (var tema in produto.TemasNorteadores)
                {
                    await _produtoCUPRepository.SalvarTemaNorteadorProdutoCUP(tema.IdTema, prod.IdProduto);
                }
            }

            if (produto.DatasEspeciais != null)
            {
                foreach (var dataEspecial in produto.DatasEspeciais)
                {
                    await _produtoCUPRepository.SalvarDataEspecialProdutoCUP(dataEspecial.IdDataEspecial, prod.IdProduto);
                }
            }

            if (produto.Miolo != null)
            {
                if (produto.Miolo.mioloPaginas == 0
                   && produto.Miolo.mioloFormatoAltura == 0
                   && produto.Miolo.mioloFormatoLargura == 0
                   && produto.Miolo.mioloPeso == 0
                   && string.IsNullOrEmpty(produto.Miolo.mioloCores)
                   && string.IsNullOrEmpty(produto.Miolo.mioloTipoPapel)
                   && string.IsNullOrEmpty(produto.Miolo.mioloGramatura)
                   && string.IsNullOrEmpty(produto.Miolo.mioloObservacoes))
                {
                    produto.Miolo = null;
                }
                else
                {
                    MioloCUP miolo = new MioloCUP
                    {
                        IdProduto = prod.IdProduto,
                        mioloPaginas = produto.Miolo.mioloPaginas,
                        mioloFormatoAltura = produto.Miolo.mioloFormatoAltura,
                        mioloFormatoLargura = produto.Miolo.mioloFormatoLargura,
                        mioloPeso = produto.Miolo.mioloPeso,
                        mioloCores = produto.Miolo.mioloCores,
                        mioloTipoPapel = produto.Miolo.mioloTipoPapel,
                        mioloGramatura = produto.Miolo.mioloGramatura,
                        mioloObservacoes = produto.Miolo.mioloObservacoes
                    };
                    await _produtoCUPRepository.SalvarMioloProdutoCUP(miolo);
                }
            }

            if (produto.Capa != null)
            {
                if (string.IsNullOrEmpty(produto.Capa.CapaCores)
                   && string.IsNullOrEmpty(produto.Capa.CapaTipoPapel)
                   && string.IsNullOrEmpty(produto.Capa.CapaGramatura)
                   && string.IsNullOrEmpty(produto.Capa.CapaOrelha)
                   && string.IsNullOrEmpty(produto.Capa.CapaObservacoes)
                   && string.IsNullOrEmpty(produto.Capa.CapaAcabamento)
                   && string.IsNullOrEmpty(produto.Capa.CapaAcabamentoLombada))
                {
                    produto.Capa = null;
                }
                else
                {
                    CapaCUP capa = new CapaCUP
                    {
                        IdProduto = prod.IdProduto,
                        CapaCores = produto.Capa.CapaCores,
                        CapaTipoPapel = produto.Capa.CapaTipoPapel,
                        CapaGramatura = produto.Capa.CapaGramatura,
                        CapaOrelha = produto.Capa.CapaOrelha,
                        CapaObservacoes = produto.Capa.CapaObservacoes,
                        CapaAcabamento = produto.Capa.CapaAcabamento,
                        CapaAcabamentoLombada = produto.Capa.CapaAcabamentoLombada
                    };
                    await _produtoCUPRepository.SalvarCapaProdutoCUP(capa);
                }
            }

            if (produto.Cadernos != null)
            {
                if (string.IsNullOrEmpty(produto.Cadernos[0].CadernoTipo)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoTipoOutros)
                   && produto.Cadernos[0].CadernoPaginas == 0
                   && produto.Cadernos[0].CadernoAltura == 0
                   && produto.Cadernos[0].CadernoLargura == 0
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoPeso)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloCores)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloTipoPapel)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloGramatura)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloObservacoes)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaCores)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaTipoPapel)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaGramatura)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaOrelha)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaAcabamento)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaObservacoes)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaAcabamentoLombada))
                {
                    produto.Cadernos = null;
                }
                else
                {
                    foreach (var caderno in produto.Cadernos)
                    {
                        CadernoCUP cad = new CadernoCUP
                        {
                            IdProduto = prod.IdProduto,
                            CadernoTipo = caderno.CadernoTipo,
                            CadernoTipoOutros = caderno.CadernoTipoOutros,
                            CadernoPaginas = caderno.CadernoPaginas,
                            CadernoAltura = caderno.CadernoAltura,
                            CadernoLargura = caderno.CadernoLargura,
                            CadernoPeso = caderno.CadernoPeso,
                            CadernoMioloCores = caderno.CadernoMioloCores,
                            CadernoMioloTipoPapel = caderno.CadernoMioloTipoPapel,
                            CadernoMioloGramatura = caderno.CadernoMioloGramatura,
                            CadernoMioloObservacoes = caderno.CadernoMioloObservacoes,
                            CadernoCapaCores = caderno.CadernoCapaCores,
                            CadernoCapaTipoPapel = caderno.CadernoCapaTipoPapel,
                            CadernoCapaGramatura = caderno.CadernoCapaGramatura,
                            CadernoCapaOrelha = caderno.CadernoCapaOrelha,
                            CadernoCapaAcabamento = caderno.CadernoCapaAcabamento,
                            CadernoCapaObservacoes = caderno.CadernoCapaObservacoes,
                            CadernoCapaAcabamentoLombada = caderno.CadernoCapaAcabamentoLombada
                        };
                        await _produtoCUPRepository.SalvarCadernoProdutoCUP(cad);
                    }
                }
            }

            if (produto.Encartes != null)
            {
                if (string.IsNullOrEmpty(produto.Encartes[0].EncarteTipo)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteAcabamento)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncartePapel)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteGramatura)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteFormato)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteCor)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteOutros))
                {
                    produto.Encartes = null;
                }
                else
                {
                    foreach (var encarte in produto.Encartes)
                    {
                        EncarteCUP enc = new EncarteCUP
                        {
                            IdProduto = prod.IdProduto,
                            EncarteTipo = encarte.EncarteTipo,
                            EncarteAcabamento = encarte.EncarteAcabamento,
                            EncartePapel = encarte.EncartePapel,
                            EncarteGramatura = encarte.EncarteGramatura,
                            EncarteFormato = encarte.EncarteFormato,
                            EncarteCor = encarte.EncarteCor,
                            EncarteOutros = encarte.EncarteOutros,
                            EncartePaginas = encarte.EncartePaginas
                        };
                        await _produtoCUPRepository.SalvarEncarteProdutoCUP(enc);
                    }
                }
            }

            if (produto.MidiaFicha != null)
            {
                if (string.IsNullOrEmpty(produto.MidiaFicha.MidiaTipo)
                && string.IsNullOrEmpty(produto.MidiaFicha.MidiaOutros))
                {
                    produto.MidiaFicha = null;
                }
                else
                {
                    await _produtoCUPRepository.SalvarMidiaProdutoCUP(prod.IdProduto, produto.MidiaFicha.MidiaTipo, produto.MidiaFicha.MidiaOutros);
                }
            }

            if (produto.CtrlImpressao != null)
            {
                if (string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpEdicao)
                   && string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpImpressao)
                   && string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpGrafica)
                   && string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpData)
                   && produto.CtrlImpressao[0].ContImpTiragem == 0
                   && string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpObservacoesGerais))
                {
                    produto.CtrlImpressao = null;
                }
                else
                {
                    foreach (var controle in produto.CtrlImpressao)
                    {
                        ControleImpressaoCUP cont = new ControleImpressaoCUP
                        {
                            IdProduto = prod.IdProduto,
                            ContImpEdicao = controle.ContImpEdicao,
                            ContImpImpressao = controle.ContImpImpressao,
                            ContImpGrafica = controle.ContImpGrafica,
                            ContImpTiragem = controle.ContImpTiragem,
                            ContImpObservacoesGerais = controle.ContImpObservacoesGerais
                        };
                        if (string.IsNullOrEmpty(controle.ContImpData))
                        {
                            cont.ContImpData = null;

                        }
                        else
                        {
                            cont.ContImpData = Convert.ToDateTime("01/" + controle.ContImpData.Substring(0, 2) + "/" + controle.ContImpData.Substring(2, 4));
                        }
                        await _produtoCUPRepository.SalvarControleImpressaoProdutoCUP(cont);
                    }
                }
            }

            return prod.IdProduto;
        }

        public async Task AtualizarProdutoCUP(ProdutoCUPViewModel produto)
        {
            ProdutoCUP prod = new ProdutoCUP();
            prod.IdProduto = produto.IdProduto;
            prod.Mercado = Convert.ToInt32(produto.Mercado);
            prod.Programa = produto.Programa;
            prod.AnoPrograma = produto.AnoPrograma;
            prod.Selo = produto.Selo;
            prod.Tipo = produto.Tipo;
            prod.Segmento = produto.Segmento;
            prod.Ano = produto.Ano;
            prod.FaixaEtaria = produto.FaixaEtaria;
            prod.Composicao = produto.Composicao;
            prod.Disciplina = produto.Disciplina;
            prod.TemaTransversal = produto.TemaTransversal;
            prod.LstAssunto = new List<AssuntoCUP>();
            prod.LstTemaNorteador = new List<TemaCUP>();
            prod.GeneroTextual = produto.GeneroTextual;
            prod.ConteudoDisciplinar = produto.ConteudoDisciplinar;
            prod.LstDatasEspeciais = new List<DataEspecialCUP>();
            prod.Premiacao = produto.Premiacao;
            prod.Versao = produto.Versao;
            prod.Colecao = produto.Colecao.ToString();
            prod.Titulo = produto.Titulo;
            prod.Edicao = produto.Edicao;
            prod.Midia = produto.Midia;
            prod.UnidadeMedida = produto.UnidadeMedida;
            prod.Plataforma = produto.Plataforma.ToString();
            if (string.IsNullOrEmpty(produto.DataPublicacao))
            {
                prod.DataPublicacao = null;

            }
            else
            {
                prod.DataPublicacao = Convert.ToDateTime(produto.DataPublicacao);
            }
            prod.ISBN = produto.ISBN;
            prod.Status = produto.Status.ToString();
            prod.CodigoBarras = produto.CodigoBarras;
            prod.Sinopse = produto.Sinopse;
            prod.EBSA = produto.EBSA;
            prod.NomeCapa = produto.NomeCapa;
            prod.Origem = produto.Origem;
            prod.TipoProduto = produto.TipoProduto;
            prod.SegmentoProtheus = produto.SegmentoProtheus;
            prod.DataPublicacaoProtheus = null;
            prod.Miolo = new MioloCUP();
            prod.Capa = new CapaCUP();
            prod.LstCaderno = new List<CadernoCUP>();
            prod.LstEncarte = new List<EncarteCUP>();
            prod.MidiaFicha = new MidiaFichaCUP();
            prod.LstControleImpressao = new List<ControleImpressaoCUP>();
            prod.Reformulado = produto.Reformulado;

            await _produtoCUPRepository.AtualizarProdutoCUP(prod);

            if (produto.Assunto != null)
            {
                await _produtoCUPRepository.ExcluirAssuntoProdutoCUP(prod.IdProduto);
                foreach (var assunto in produto.Assunto)
                {
                    await _produtoCUPRepository.SalvarAssuntoProdutoCUP(assunto.IdAssunto, prod.IdProduto);
                }
            }

            if (produto.TemasNorteadores != null)
            {
                await _produtoCUPRepository.ExcluirTemaNorteadorProdutoCUP(prod.IdProduto);
                foreach (var tema in produto.TemasNorteadores)
                {
                    await _produtoCUPRepository.SalvarTemaNorteadorProdutoCUP(tema.IdTema, prod.IdProduto);
                }
            }

            if (produto.DatasEspeciais != null)
            {
                await _produtoCUPRepository.ExcluirDataEspecialProdutoCUP(prod.IdProduto);
                foreach (var dataEspecial in produto.DatasEspeciais)
                {
                    await _produtoCUPRepository.SalvarDataEspecialProdutoCUP(dataEspecial.IdDataEspecial, prod.IdProduto);
                }
            }

            bool existeEspec = await _produtoCUPRepository.VerificaExisteEscpeCUP(prod.IdProduto);

            if (produto.Miolo != null)
            {
                if (produto.Miolo.mioloPaginas == 0
                   && produto.Miolo.mioloFormatoAltura == 0
                   && produto.Miolo.mioloFormatoLargura == 0
                   && produto.Miolo.mioloPeso == 0
                   && string.IsNullOrEmpty(produto.Miolo.mioloCores)
                   && string.IsNullOrEmpty(produto.Miolo.mioloTipoPapel)
                   && string.IsNullOrEmpty(produto.Miolo.mioloGramatura)
                   && string.IsNullOrEmpty(produto.Miolo.mioloObservacoes))
                {
                    produto.Miolo = null;
                }
                else
                {
                    MioloCUP miolo = new MioloCUP
                    {
                        IdProduto = prod.IdProduto,
                        mioloPaginas = produto.Miolo.mioloPaginas,
                        mioloFormatoAltura = produto.Miolo.mioloFormatoAltura,
                        mioloFormatoLargura = produto.Miolo.mioloFormatoLargura,
                        mioloPeso = produto.Miolo.mioloPeso,
                        mioloCores = produto.Miolo.mioloCores,
                        mioloTipoPapel = produto.Miolo.mioloTipoPapel,
                        mioloGramatura = produto.Miolo.mioloGramatura,
                        mioloObservacoes = produto.Miolo.mioloObservacoes
                    };
                    if(existeEspec)
                    {
                        await _produtoCUPRepository.AtualizarMioloProdutoCUP(miolo);
                    }
                    else
                    {
                        await _produtoCUPRepository.SalvarMioloProdutoCUP(miolo);
                    }
                }
            }

            if (produto.Capa != null)
            {
                if (string.IsNullOrEmpty(produto.Capa.CapaCores)
                   && string.IsNullOrEmpty(produto.Capa.CapaTipoPapel)
                   && string.IsNullOrEmpty(produto.Capa.CapaGramatura)
                   && string.IsNullOrEmpty(produto.Capa.CapaOrelha)
                   && string.IsNullOrEmpty(produto.Capa.CapaObservacoes)
                   && string.IsNullOrEmpty(produto.Capa.CapaAcabamento)
                   && string.IsNullOrEmpty(produto.Capa.CapaAcabamentoLombada))
                {
                    produto.Capa = null;
                }
                else
                {
                    CapaCUP capa = new CapaCUP
                    {
                        IdProduto = prod.IdProduto,
                        CapaCores = produto.Capa.CapaCores,
                        CapaTipoPapel = produto.Capa.CapaTipoPapel,
                        CapaGramatura = produto.Capa.CapaGramatura,
                        CapaOrelha = produto.Capa.CapaOrelha,
                        CapaObservacoes = produto.Capa.CapaObservacoes,
                        CapaAcabamento = produto.Capa.CapaAcabamento,
                        CapaAcabamentoLombada = produto.Capa.CapaAcabamentoLombada
                    };
                    if (existeEspec)
                    {
                        await _produtoCUPRepository.AtualizarCapaProdutoCUP(capa);
                    }
                    else
                    {
                        await _produtoCUPRepository.SalvarCapaProdutoCUP(capa);
                    }
                    
                }
            }

            if (produto.Cadernos != null)
            {
                if (string.IsNullOrEmpty(produto.Cadernos[0].CadernoTipo)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoTipoOutros)
                   && produto.Cadernos[0].CadernoPaginas == 0
                   && produto.Cadernos[0].CadernoAltura == 0
                   && produto.Cadernos[0].CadernoLargura == 0
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoPeso)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloCores)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloTipoPapel)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloGramatura)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoMioloObservacoes)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaCores)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaTipoPapel)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaGramatura)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaOrelha)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaAcabamento)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaObservacoes)
                   && string.IsNullOrEmpty(produto.Cadernos[0].CadernoCapaAcabamentoLombada))
                {
                    produto.Cadernos = null;
                }
                else
                {
                    if (existeEspec)
                    {
                        await _produtoCUPRepository.ExcluirCadernoProdutoCUP(prod.IdProduto);
                    }
                    foreach (var caderno in produto.Cadernos)
                    {
                        CadernoCUP cad = new CadernoCUP
                        {
                            IdProduto = prod.IdProduto,
                            CadernoTipo = caderno.CadernoTipo,
                            CadernoTipoOutros = caderno.CadernoTipoOutros,
                            CadernoPaginas = caderno.CadernoPaginas,
                            CadernoAltura = caderno.CadernoAltura,
                            CadernoLargura = caderno.CadernoLargura,
                            CadernoPeso = caderno.CadernoPeso,
                            CadernoMioloCores = caderno.CadernoMioloCores,
                            CadernoMioloTipoPapel = caderno.CadernoMioloTipoPapel,
                            CadernoMioloGramatura = caderno.CadernoMioloGramatura,
                            CadernoMioloObservacoes = caderno.CadernoMioloObservacoes,
                            CadernoCapaCores = caderno.CadernoCapaCores,
                            CadernoCapaTipoPapel = caderno.CadernoCapaTipoPapel,
                            CadernoCapaGramatura = caderno.CadernoCapaGramatura,
                            CadernoCapaOrelha = caderno.CadernoCapaOrelha,
                            CadernoCapaAcabamento = caderno.CadernoCapaAcabamento,
                            CadernoCapaObservacoes = caderno.CadernoCapaObservacoes,
                            CadernoCapaAcabamentoLombada = caderno.CadernoCapaAcabamentoLombada
                        };

                        await _produtoCUPRepository.SalvarCadernoProdutoCUP(cad);
                    }
                }
            }

            if (produto.Encartes != null)
            {
                if (string.IsNullOrEmpty(produto.Encartes[0].EncarteTipo)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteAcabamento)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncartePapel)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteGramatura)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteFormato)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteCor)
                   && string.IsNullOrEmpty(produto.Encartes[0].EncarteOutros))
                {
                    produto.Encartes = null;
                }
                else
                {
                    if (existeEspec)
                    {
                        await _produtoCUPRepository.ExcluirEncarteProdutoCUP(prod.IdProduto);
                    }
                    foreach (var encarte in produto.Encartes)
                    {
                        EncarteCUP enc = new EncarteCUP
                        {
                            IdProduto = prod.IdProduto,
                            EncarteTipo = encarte.EncarteTipo,
                            EncarteAcabamento = encarte.EncarteAcabamento,
                            EncartePapel = encarte.EncartePapel,
                            EncarteGramatura = encarte.EncarteGramatura,
                            EncarteFormato = encarte.EncarteFormato,
                            EncarteCor = encarte.EncarteCor,
                            EncarteOutros = encarte.EncarteOutros,
                            EncartePaginas = encarte.EncartePaginas
                        };
                        await _produtoCUPRepository.SalvarEncarteProdutoCUP(enc);
                    }
                }
            }

            if (produto.MidiaFicha != null)
            {
                if (string.IsNullOrEmpty(produto.MidiaFicha.MidiaTipo)
                && string.IsNullOrEmpty(produto.MidiaFicha.MidiaOutros))
                {
                    produto.MidiaFicha = null;
                }
                else
                {
                    if (existeEspec)
                    {
                        await _produtoCUPRepository.AtualizarMidiaProdutoCUP(prod.IdProduto, produto.MidiaFicha.MidiaTipo, produto.MidiaFicha.MidiaOutros);
                    }
                    else
                    {
                        await _produtoCUPRepository.SalvarMidiaProdutoCUP(prod.IdProduto, produto.MidiaFicha.MidiaTipo, produto.MidiaFicha.MidiaOutros);
                    }
                }
            }

            if (produto.CtrlImpressao != null)
            {
                if (string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpEdicao)
                   && string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpImpressao)
                   && string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpGrafica)
                   && produto.CtrlImpressao[0].ContImpData == null
                   && produto.CtrlImpressao[0].ContImpTiragem == 0
                   && string.IsNullOrEmpty(produto.CtrlImpressao[0].ContImpObservacoesGerais))
                {
                    produto.CtrlImpressao = null;
                }
                else
                {
                    if (existeEspec)
                    {
                        await _produtoCUPRepository.ExcluirControleImpressaoProdutoCUP(prod.IdProduto);
                    }
                    foreach (var controle in produto.CtrlImpressao)
                    {
                        ControleImpressaoCUP cont = new ControleImpressaoCUP
                        {
                            IdProduto = prod.IdProduto,
                            ContImpEdicao = controle.ContImpEdicao,
                            ContImpImpressao = controle.ContImpImpressao,
                            ContImpGrafica = controle.ContImpGrafica,
                            ContImpTiragem = controle.ContImpTiragem,
                            ContImpObservacoesGerais = controle.ContImpObservacoesGerais
                        };
                        if (string.IsNullOrEmpty(controle.ContImpData))
                        {
                            cont.ContImpData = null;
                        }
                        else
                        {
                            cont.ContImpData = Convert.ToDateTime("01/" + controle.ContImpData.Substring(0, 2) + "/" + controle.ContImpData.Substring(2, 4));
                        }
                        await _produtoCUPRepository.SalvarControleImpressaoProdutoCUP(cont);
                    }
                }

            }
        }

        public async Task ExcluirProdutoCUP(long IdProduto)
        {
            await _produtoCUPRepository.ExcluirProdutoCUP(IdProduto);
        }

        public async Task PublicarProdutoCUP(long IdProduto)
        {
            await _produtoCUPRepository.PublicarProdutoCUP(IdProduto);
        }

        public async Task SalvarProdutoLinkCUP(long IdProduto, string Url, decimal? Preco)
        {
            await _produtoCUPRepository.SalvarProdutoLinkCUP(IdProduto, Url, Preco);
        }

        public void SalvarPreferenciasRelatorioCUP(string viewmodel, int idUsuario)
        {
            _produtoCUPRepository.SalvarPreferenciasRelatorioCUP(viewmodel, idUsuario);
        }

        public void AtualizarPreferenciasRelatorioCUP(string viewmodel, int idUsuario)
        {
            _produtoCUPRepository.AtualizarPreferenciasRelatorioCUP(viewmodel, idUsuario);
        }

        public string CarregarPreferenciasRelatorioCUP(int idUsuario)
        {
            return _produtoCUPRepository.CarregarPreferenciasRelatorioCUP(idUsuario);
        }

        #endregion

        #region Arquivo

        public async Task SalvarArquivoCUP(ArquivoProdutoCUPViewModel arquivo)
        {
            ArquivoProdutoCUP arq = new ArquivoProdutoCUP
            {
                IdProduto = arquivo.IdProduto,
                ProdutoEBSA = arquivo.ProdutoEBSA,
                NomeArquivo = arquivo.NomeArquivo,
                CaminhoArquivo = arquivo.CaminhoArquivo,
                DataCadastro = arquivo.DataCadastro
            };
            await _produtoCUPRepository.SalvarArquivoCUP(arq);
        }

        public async Task SalvarEpubCUP(ArquivoProdutoCUPViewModel arquivo)
        {
            ArquivoProdutoCUP arq = new ArquivoProdutoCUP
            {
                IdProduto = arquivo.IdProduto,
                ProdutoEBSA = arquivo.ProdutoEBSA,
                NomeArquivo = arquivo.NomeArquivo,
                CaminhoArquivo = arquivo.CaminhoArquivo,
                DataCadastro = arquivo.DataCadastro
            };
            await _produtoCUPRepository.SalvarEpubCUP(arq);
        }

        public async Task<int> SalvarArquivoAutoriaCUP(ArquivoProdutoCUPViewModel arquivo)
        {
            ArquivoProdutoCUP arq = new ArquivoProdutoCUP
            {
                IdProduto = arquivo.IdProduto,
                NomeArquivo = arquivo.NomeArquivo,
                CaminhoArquivo = arquivo.CaminhoArquivo,
                DataCadastro = arquivo.DataCadastro
            };
            return await _produtoCUPRepository.SalvarArquivoAutoriaCUP(arq);
        }

        public async Task AtualizarArquivoCUP(ArquivoProdutoCUPViewModel arquivo)
        {
            ArquivoProdutoCUP arq = new ArquivoProdutoCUP
            {
                IdProduto = arquivo.IdProduto,
                ProdutoEBSA = arquivo.ProdutoEBSA,
                NomeArquivo = arquivo.NomeArquivo,
                CaminhoArquivo = arquivo.CaminhoArquivo,
                DataCadastro = arquivo.DataCadastro
            };
            await _produtoCUPRepository.ExcluirArquivoCUP(arquivo.IdProduto);
            await _produtoCUPRepository.SalvarArquivoCUP(arq);
        }

        public async Task ExcluirEpubCUP(long idProduto, string nomeArquivo)
        {
            await _produtoCUPRepository.ExcluirEpubCUP(idProduto, nomeArquivo);
        }

        public async Task ExcluirArquivoAutoriaCUP(long IdDoc)
        {
            await _produtoCUPRepository.ExcluirArquivoAutoriaCUP(IdDoc);
        }

        public List<ArquivoAutoriaCUPViewModel> BuscarArquivoAutoriaCUP(long IdAutoria)
        {
            List<ArquivoAutoriaCUPViewModel> lstArq = new List<ArquivoAutoriaCUPViewModel>();
            var lst = _produtoCUPRepository.BuscarArquivosAutoria(IdAutoria);
            foreach (var item in lst)
            {
                ArquivoAutoriaCUPViewModel a = new ArquivoAutoriaCUPViewModel();
                a.IdAutoria = item.IdAutoria;
                a.IdDoc = item.IdDoc;
                a.Caminho = item.Caminho;
                a.Nome = item.Nome;
                lstArq.Add(a);
            }
            return lstArq;
        }

        #endregion

        #region Autoria

        public List<AutoriaCUPViewModel> CarregarAutoriaPorIdProdutoCUP(long IdProduto)
        {
            var autoria = _produtoCUPRepository.CarregarAutoriaPorIdProdutoCUP(IdProduto);
            List<AutoriaCUPViewModel> lst = new List<AutoriaCUPViewModel>();
            foreach (var item in autoria)
            {
                AutoriaCUPViewModel aut = new AutoriaCUPViewModel();
                aut.IdAutoria = item.IdAutoria;
                aut.NomeContratual = item.NomeContratual;
                aut.DataLiberacao = item.DataLiberacao;
                aut.DataVencimento = item.DataVencimento;
                aut.RenovacaoAuto = item.RenovacaoAuto;
                aut.QtdeMeses = item.QtdeMeses;
                aut.DireitosAutorais = item.DireitosAutorais;
                aut.DataLimite = item.DataLimite;
                aut.IdProduto = item.IdProduto;
                aut.Nacionalidade = item.Nacionalidade;
                aut.Naturalidade = item.Naturalidade;
                aut.ReparteImpressao = item.ReparteImpressao;
                aut.ReparteReimpressao = item.ReparteReimpressao;
                aut.CapaCodAutor = item.CapaCodAutor;

                aut.lstFuncoes = new List<FuncoesCUPViewModel>();
                var lstF = _produtoCUPRepository.CarregarFuncaoAutoriaCUP(aut.IdAutoria);
                foreach (var item2 in lstF)
                {
                    FuncoesCUPViewModel f = new FuncoesCUPViewModel();
                    f.IdFuncao = item2.IdFuncao;
                    f.DescricaoFuncao = item2.DescricaoFuncao;
                    aut.lstFuncoes.Add(f);
                }

                lst.Add(aut);
            }



            return lst;
        }

        public List<AutoresCUPViewModel> CarregarAutoresCUP(string NomeContratual)
        {
            var lstAutores = _produtoCUPRepository.CarregarAutoresCUP(NomeContratual);
            var lstRetorno = new List<AutoresCUPViewModel>();

            foreach (var item in lstAutores)
            {
                var ret = new AutoresCUPViewModel
                {
                    IdAutor = item.IdAutor,
                    NomeContratual = item.NomeContratual,
                    CapaCodAutor = item.CapaCodAutor
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        public List<FuncoesCUPViewModel> CarregarFuncoesCUP()
        {
            var lstFuncoes = _produtoCUPRepository.CarregarFuncoesCUP();
            var lstRetorno = new List<FuncoesCUPViewModel>();

            foreach (var item in lstFuncoes)
            {
                var ret = new FuncoesCUPViewModel
                {
                    IdFuncao = item.IdFuncao,
                    DescricaoFuncao = item.DescricaoFuncao,
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        public async Task<long> SalvarAutoriaCUP(AutoriaCUPViewModel autoria)
        {
            AutoriaCUP aut = new AutoriaCUP();
            aut.NomeContratual = autoria.NomeContratual;
            aut.DataLiberacao = autoria.DataLiberacao;
            aut.DataVencimento = autoria.DataVencimento;
            aut.RenovacaoAuto = autoria.RenovacaoAuto;
            aut.QtdeMeses = autoria.QtdeMeses;
            aut.DireitosAutorais = autoria.DireitosAutorais;
            aut.DataLimite = autoria.DataLimite;
            aut.IdProduto = autoria.IdProduto;
            aut.Nacionalidade = autoria.Nacionalidade;
            aut.Naturalidade = autoria.Naturalidade;
            aut.ReparteImpressao = autoria.ReparteImpressao;
            aut.ReparteReimpressao = autoria.ReparteReimpressao;
            aut.CapaCodAutor = autoria.CapaCodAutor;

            aut.IdAutoria = await _produtoCUPRepository.SalvarAutoriaCUP(aut);

            foreach (var f in autoria.lstFuncoes)
            {
                await _produtoCUPRepository.SalvarAutoriaFuncaoCUP(f.IdFuncao, aut.IdAutoria);
            }

            return aut.IdAutoria;
        }

        public async Task AtualizarAutoriaCUP(AutoriaCUPViewModel autoria)
        {
            AutoriaCUP aut = new AutoriaCUP();
            aut.NomeContratual = autoria.NomeContratual;
            aut.DataLiberacao = autoria.DataLiberacao;
            aut.DataVencimento = autoria.DataVencimento;
            aut.RenovacaoAuto = autoria.RenovacaoAuto;
            aut.QtdeMeses = autoria.QtdeMeses;
            aut.DireitosAutorais = autoria.DireitosAutorais;
            aut.DataLimite = autoria.DataLimite;
            aut.IdProduto = autoria.IdProduto;
            aut.Nacionalidade = autoria.Nacionalidade;
            aut.Naturalidade = autoria.Naturalidade;
            aut.ReparteImpressao = autoria.ReparteImpressao;
            aut.ReparteReimpressao = autoria.ReparteReimpressao;
            aut.CapaCodAutor = autoria.CapaCodAutor;
            aut.IdAutoria = autoria.IdAutoria;

            await _produtoCUPRepository.AtualizarAutoriaCUP(aut);
            await _produtoCUPRepository.ExcluirAutoriaFuncaoCUP(aut.IdAutoria);
            foreach (var f in autoria.lstFuncoes)
            {
                await _produtoCUPRepository.SalvarAutoriaFuncaoCUP(f.IdFuncao, aut.IdAutoria);
            }
        }

        public async Task ExcluirAutoriaCUP(long IdAutoria)
        {
            await _produtoCUPRepository.ExcluirAutoriaFuncaoCUP(IdAutoria);
            await _produtoCUPRepository.ExcluirAutoriaCUP(IdAutoria);
        }

        #endregion

        #region Mercado

        public List<MercadoCUPViewModel> CarregarMercadoCUP()
        {
            var lstMercado = _produtoCUPRepository.CarregarMercadoCUP();
            var lstRetorno = new List<MercadoCUPViewModel>();

            foreach (var item in lstMercado)
            {
                var ret = new MercadoCUPViewModel
                {
                    CodigoMercado = item.Codigo_Mercado,
                    DescricaoMercado = item.Descricao_Mercado
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Selo

        public List<SeloCUPViewModel> CarregarSeloCUP()
        {
            var lstSelos = _produtoCUPRepository.CarregarSeloCUP();
            var lstRetorno = new List<SeloCUPViewModel>();

            foreach (var item in lstSelos)
            {
                var ret = new SeloCUPViewModel
                {
                    IdSelo = item.Id_Selo,
                    DescricaoSelo = item.Descricao_Selo,
                    ChaveGrupoSelo = item.Chave_Grupo_Selo
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Tipo

        public List<TipoCUPViewModel> CarregarTipoCUP()
        {
            var lstTipos = _produtoCUPRepository.CarregarTipoCUP();
            var lstRetorno = new List<TipoCUPViewModel>();

            foreach (var item in lstTipos)
            {
                var ret = new TipoCUPViewModel
                {
                    IdTipo = item.Id_Tipo,
                    DescricaoTipo = item.Descricao_Tipo
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        public List<TipoEspecCUPViewModel> CarregarTipoEspecCUP(string TipoEspec)
        {
            var lstTipos = _produtoCUPRepository.CarregarTipoEspecCUP(TipoEspec);
            var lstRetorno = new List<TipoEspecCUPViewModel>();

            foreach (var item in lstTipos)
            {
                var ret = new TipoEspecCUPViewModel
                {
                    IdTipoEspec = item.IdTipoEspec,
                    Descricao = item.Descricao,
                    TipoEspec = item.TipoEspec
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Segmento

        public List<SegmentoCUPViewModel> CarregarSegmentoCUP()
        {
            var lstSegmentos = _produtoCUPRepository.CarregarSegmentoCUP();
            var lstRetorno = new List<SegmentoCUPViewModel>();

            foreach (var item in lstSegmentos)
            {
                var ret = new SegmentoCUPViewModel
                {
                    IdSegmento = item.Id_Segmento,
                    DescricaoSegmento = item.Descricao_Segmento,
                    AbreviacaoSegmento = item.Abreviacao_Segmento
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Composição

        public List<ComposicaoCUPViewModel> CarregarComposicaoCUP()
        {
            var lstComposicao = _produtoCUPRepository.CarregarComposicaoCUP();
            var lstRetorno = new List<ComposicaoCUPViewModel>();

            foreach (var item in lstComposicao)
            {
                var ret = new ComposicaoCUPViewModel
                {
                    IdComposicao = item.Id_Composicao,
                    DescricaoComposicao = item.Descricao_Composicao
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Ano

        public List<AnoCUPViewModel> CarregarAnoCUP()
        {
            var lstAno = _produtoCUPRepository.CarregarAnoCUP();
            var lstRetorno = new List<AnoCUPViewModel>();

            foreach (var item in lstAno)
            {
                var ret = new AnoCUPViewModel
                {
                    IdAno = item.Id_Ano,
                    DescricaoAno = item.Descricao_Ano,
                    SegmentoAno = item.Segmento_Ano,
                    AbreviacaoAno = item.Abreviacao_Ano
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        public List<AnoCUPViewModel> CarregarAnoPorIdSegmentoCUP(int IdSegmento)
        {
            var lstAno = _produtoCUPRepository.CarregarAnoPorIdSegmentoCUP(IdSegmento);
            var lstRetorno = new List<AnoCUPViewModel>();

            foreach (var item in lstAno)
            {
                var ret = new AnoCUPViewModel
                {
                    IdAno = item.Id_Ano,
                    DescricaoAno = item.Descricao_Ano,
                    SegmentoAno = item.Segmento_Ano,
                    AbreviacaoAno = item.Abreviacao_Ano
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Faixa Etaria

        public List<FaixaEtariaCUPViewModel> CarregarFaixaEtariaCUP()
        {
            var lstFaixaEtaria = _produtoCUPRepository.CarregarFaixaEtariaCUP();
            var lstRetorno = new List<FaixaEtariaCUPViewModel>();

            foreach (var item in lstFaixaEtaria)
            {
                var ret = new FaixaEtariaCUPViewModel
                {
                    IdFaixaEtaria = item.Id_FaixaEtaria,
                    DescricaoFaixaEtaria = item.Descricao_FaixaEtaria,
                    IdAno = item.Id_Ano
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        public List<FaixaEtariaCUPViewModel> CarregarFaixaEtariaPorIdAnoCUP(int IdAno)
        {
            var lstFaixaEtaria = _produtoCUPRepository.CarregarFaixaEtariaPorIdAnoCUP(IdAno);
            var lstRetorno = new List<FaixaEtariaCUPViewModel>();

            foreach (var item in lstFaixaEtaria)
            {
                var ret = new FaixaEtariaCUPViewModel
                {
                    IdFaixaEtaria = item.Id_FaixaEtaria,
                    DescricaoFaixaEtaria = item.Descricao_FaixaEtaria,
                    IdAno = item.Id_Ano
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Disciplina

        public List<DisciplinaCUPViewModel> CarregarDisciplinaCUP()
        {
            var lstDisciplina = _produtoCUPRepository.CarregarDisciplinaCUP();
            var lstRetorno = new List<DisciplinaCUPViewModel>();

            foreach (var item in lstDisciplina)
            {
                var ret = new DisciplinaCUPViewModel
                {
                    IdDisciplina = item.Id_Disciplina,
                    DescricaoDisciplina = item.Descricao_Disciplina
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Conteudo Disciplinar

        public List<ConteudoDisciplinarCUPViewModel> CarregarConteudoDisciplinarCUP()
        {
            var lstConteudoDisciplinar = _produtoCUPRepository.CarregarConteudoDisciplinarCUP();
            var lstRetorno = new List<ConteudoDisciplinarCUPViewModel>();

            foreach (var item in lstConteudoDisciplinar)
            {
                var ret = new ConteudoDisciplinarCUPViewModel
                {
                    IdConteudoDisciplinar = item.Id_ConteudoDisciplinar,
                    DescricaoConteudoDisciplinar = item.Descricao_ConteudoDisciplinar
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Tema

        public List<TemaCUPViewModel> CarregarTemaTransversalCUP()
        {
            var lstTemaTransversal = _produtoCUPRepository.CarregarTemaTransversalCUP();
            var lstRetorno = new List<TemaCUPViewModel>();

            foreach (var item in lstTemaTransversal)
            {
                var ret = new TemaCUPViewModel
                {
                    IdTema = item.Id_Tema,
                    DescricaoTema = item.Descricao_Tema,
                    TipoTema = item.Tipo_Tema
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        public List<TemaCUPViewModel> CarregarTemaNorteadorCUP()
        {
            var lstTemaNorteador = _produtoCUPRepository.CarregarTemaNorteadorCUP();
            var lstRetorno = new List<TemaCUPViewModel>();

            foreach (var item in lstTemaNorteador)
            {
                var ret = new TemaCUPViewModel
                {
                    IdTema = item.Id_Tema,
                    DescricaoTema = item.Descricao_Tema,
                    TipoTema = item.Tipo_Tema
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Data Especial

        public List<DataEspecialCUPViewModel> CarregarDataEspecialCUP()
        {
            var lstDataEspecial = _produtoCUPRepository.CarregarDataEspecialCUP();
            var lstRetorno = new List<DataEspecialCUPViewModel>();

            foreach (var item in lstDataEspecial)
            {
                var ret = new DataEspecialCUPViewModel
                {
                    IdDataEspecial = item.Id_DataEspecial,
                    DescricaoDataEspecial = item.Descricao_DataEspecial,
                    DiaDataEspecial = item.Dia_DataEspecial
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Assunto

        public List<AssuntoCUPViewModel> CarregarAssuntoCUP()
        {
            var lstAssunto = _produtoCUPRepository.CarregarAssuntoCUP();
            var lstRetorno = new List<AssuntoCUPViewModel>();

            foreach (var item in lstAssunto)
            {
                var ret = new AssuntoCUPViewModel
                {
                    IdAssunto = item.Id_Assunto,
                    DescricaoAssunto = item.Descricao_Assunto
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Genero Textual

        public List<GeneroTextualCUPViewModel> CarregarGeneroTextualCUP()
        {
            var lstGeneroTextual = _produtoCUPRepository.CarregarGeneroTextualCUP();
            var lstRetorno = new List<GeneroTextualCUPViewModel>();

            foreach (var item in lstGeneroTextual)
            {
                var ret = new GeneroTextualCUPViewModel
                {
                    IdGeneroTextual = item.Id_GeneroTextual,
                    DescricaoGeneroTextual = item.Descricao_GeneroTextual
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Versao

        public List<VersaoCUPViewModel> CarregarVersaoCUP()
        {
            var lstVersao = _produtoCUPRepository.CarregarVersaoCUP();
            var lstRetorno = new List<VersaoCUPViewModel>();

            foreach (var item in lstVersao)
            {
                var ret = new VersaoCUPViewModel
                {
                    IdVersao = item.Id_Versao,
                    DescricaoVersao = item.Descricao_Versao,
                    AbreviacaoVersao = item.Abreviacao_Versao
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Colecao

        public List<ColecaoCUPViewModel> CarregarColecaoCUP()
        {
            var lstColecao = _produtoCUPRepository.CarregarColecaoCUP();
            var lstRetorno = new List<ColecaoCUPViewModel>();

            foreach (var item in lstColecao)
            {
                var ret = new ColecaoCUPViewModel
                {
                    IdColecao = item.Id_Colecao,
                    DescricaoColecao = item.Descricao_Colecao,
                    TipoColecao = item.Tipo_Colecao,
                    SegmentoColecao = item.Segmento_Colecao
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Midia

        public List<MidiaCUPViewModel> CarregarMidiaCUP()
        {
            var lstMidia = _produtoCUPRepository.CarregarMidiaCUP();
            var lstRetorno = new List<MidiaCUPViewModel>();

            foreach (var item in lstMidia)
            {
                var ret = new MidiaCUPViewModel
                {
                    IdMidia = item.Id_Midia,
                    DescricaoMidia = item.Descricao_Midia,
                    UnidadeVendaMidia = item.UnidadeVenda_Midia,
                    PlataformaMidia = item.Plataforma_Midia
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Unidade de Medida

        public List<UnidadeMedidaCUPViewModel> CarregarUnidadeMedidaCUP()
        {
            var lstUnidadeMedida = _produtoCUPRepository.CarregarUnidadeMedidaCUP();
            var lstRetorno = new List<UnidadeMedidaCUPViewModel>();

            foreach (var item in lstUnidadeMedida)
            {
                var ret = new UnidadeMedidaCUPViewModel
                {
                    IdUnidadeMedida = item.Id_UnidadeMedida,
                    DescricaoUnidadeMedida = item.Descricao_UnidadeMedida,
                    SiglaUnidadeMedida = item.Sigla_UnidadeMedida
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Plataforma

        public List<PlataformaCUPViewModel> CarregarPlataformaCUP()
        {
            var lstPlataforma = _produtoCUPRepository.CarregarPlataformaCUP();
            var lstRetorno = new List<PlataformaCUPViewModel>();

            foreach (var item in lstPlataforma)
            {
                var ret = new PlataformaCUPViewModel
                {
                    IdPlataforma = item.Id_Plataforma,
                    DescricaoPlataforma = item.Descricao_Plataforma
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        public List<PlataformaCUPViewModel> CarregarPlataformaPorIdMidiaCUP(int IdMidia)
        {
            var lstPlataforma = _produtoCUPRepository.CarregarPlataformaPorIdMidiaCUP(IdMidia);
            var lstRetorno = new List<PlataformaCUPViewModel>();

            foreach (var item in lstPlataforma)
            {
                var ret = new PlataformaCUPViewModel
                {
                    IdPlataforma = item.Id_Plataforma,
                    DescricaoPlataforma = item.Descricao_Plataforma
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Status

        public List<StatusCUPViewModel> CarregarStatusCUP()
        {
            var lstStatus = _produtoCUPRepository.CarregarStatusCUP();
            var lstRetorno = new List<StatusCUPViewModel>();

            foreach (var item in lstStatus)
            {
                var ret = new StatusCUPViewModel
                {
                    IdStatus = item.Id_Status,
                    DescricaoStatus = item.Descricao_Status,
                    CododigoStatus = item.Cododigo_Status
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Origem

        public List<OrigemCUPViewModel> CarregarOrigemCUP()
        {
            var lstOrigem = _produtoCUPRepository.CarregarOrigemCUP();
            var lstRetorno = new List<OrigemCUPViewModel>();

            foreach (var item in lstOrigem)
            {
                var ret = new OrigemCUPViewModel
                {
                    IdOrigem = item.Id_Origem,
                    DescricaoOrigem = item.Descricao_Origem,
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Tipo Produto

        public List<TipoProdutoCUPViewModel> CarregarTipoProdutoCUP()
        {
            var lstTipoProduto = _produtoCUPRepository.CarregarTipoProdutoCUP();
            var lstRetorno = new List<TipoProdutoCUPViewModel>();

            foreach (var item in lstTipoProduto)
            {
                var ret = new TipoProdutoCUPViewModel
                {
                    IdTipoProduto = item.Id_TipoProduto,
                    DescricaoTipoProduto = item.Descricao_TipoProduto,
                    SiglaTipoProduto = item.Sigla_TipoProduto,
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion

        #region Segmento Protheus

        public List<SegmentoProtheusCUPViewModel> CarregarSegmentoProtheusCUP()
        {
            var lstSegmentoProtheus = _produtoCUPRepository.CarregarSegmentoProtheusCUP();
            var lstRetorno = new List<SegmentoProtheusCUPViewModel>();

            foreach (var item in lstSegmentoProtheus)
            {
                var ret = new SegmentoProtheusCUPViewModel
                {
                    IdSegmentoProtheus = item.Id_SegmentoProtheus,
                    DescricaoSegmentoProtheus = item.Descricao_SegmentoProtheus,
                    CodigoSegmentoProtheus = item.Codigo_SegmentoProtheus,
                };
                lstRetorno.Add(ret);
            }
            return lstRetorno;
        }

        #endregion
    }
}
