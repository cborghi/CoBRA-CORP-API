using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoCUPController : BaseController
    {
        public readonly IProdutoCUPAppService _produtoCUPAppService;

        public ProdutoCUPController(IProdutoCUPAppService produtoCUPAppService, ILogAppService logAppService) : base(logAppService)
        {
            _produtoCUPAppService = produtoCUPAppService;
        }

        #region Produto

        /// <summary>
        /// Carrega todos os produtos do CUP
        /// </summary>
        /// <returns>lista de ProdutoCUPViewModel</returns>
        [HttpGet("CarregarProdutos")]
        public IActionResult CarregarProdutos(int NumeroPagina, int RegistrosPagina)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                var Produtos = _produtoCUPAppService.CarregarProdutosCUP(NumeroPagina, RegistrosPagina, idUsuario, false);
                return Ok(Produtos);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega todos os produtos do CUP que são E-BOOK
        /// </summary>
        /// <returns>lista de ProdutoCUPViewModel</returns>
        [HttpGet("CarregarProdutosEbook")]
        public IActionResult CarregarProdutosEbook(int NumeroPagina, int RegistrosPagina)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                var Produtos = _produtoCUPAppService.CarregarProdutosCUP(NumeroPagina, RegistrosPagina, idUsuario, true);
                return Ok(Produtos);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega todos os produtos do CUP
        /// com filtro para IdProduto, Titulo
        /// ISBN, EBSA e Ano Educação
        /// </summary>
        /// <returns>lista de ProdutoCUPViewModel</returns>
        [HttpGet("CarregarProdutosFiltro")]
        public IActionResult CarregarProdutosFiltro(int NumeroPagina, int RegistrosPagina, string Filtro)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                var Produtos = _produtoCUPAppService.CarregarProdutosFiltroCUP(NumeroPagina, RegistrosPagina, Filtro.Replace("\r", "").Replace("\n", ""), idUsuario, false);
                return Ok(Produtos);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega todos os produtos do CUP que forem ebooks
        /// com filtro para IdProduto, Titulo
        /// ISBN, EBSA e Ano Educação
        /// </summary>
        /// <returns>lista de ProdutoCUPViewModel</returns>
        [HttpGet("CarregarProdutosEbookFiltro")]
        public IActionResult CarregarProdutosEbookFiltro(int NumeroPagina, int RegistrosPagina, string Filtro)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                var Produtos = _produtoCUPAppService.CarregarProdutosFiltroCUP(NumeroPagina, RegistrosPagina, Filtro.Replace("\r", "").Replace("\n", ""), idUsuario, true);
                return Ok(Produtos);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// carrega um produto usando como parametro
        /// o id do produto
        /// </summary>
        /// <param name="IdProduto"></param>
        /// <returns></returns>
        [HttpGet("CarregarProdutosId")]
        public IActionResult CarregarProdutosID(long IdProduto)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                var Produto = _produtoCUPAppService.CarregarProdutosIdCUP(IdProduto, idUsuario);
                return Ok(Produto);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// carrega um produto usando como parametro
        /// o id do produto
        /// </summary>
        /// <param name="IdProduto"></param>
        /// <returns></returns>
        [HttpGet("ExcluirArquivoEbook")]
        public IActionResult ExcluirArquivoEbook(long IdProduto, string NomeArquivo)
        {
            try
            {
                _produtoCUPAppService.ExcluirEpubCUP(IdProduto, NomeArquivo);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// A partir de alguns parametros, gera um codigo EBSA para 
        /// um novo produto verificando se o mesmo não existe na base
        /// </summary>
        /// <param name="viewModel">string Mercado,
        ///string Ano,
        ///int Disciplina,
        ///string Midia,
        ///string Tipo,
        ///string Edicao,
        ///string Versao,
        ///string CodigoEbsa,
        ///string Titulo</param>
        /// <returns>COdigo EBSA</returns>
        [HttpPost("GerarCodigoEbsa")]
        public async Task<IActionResult> GerarCodigoEbsa([FromBody] ProdutoEBSAViewModel viewModel)
        {
            try
            {
                string codigoEbsa = await _produtoCUPAppService.GerarCodigoEbsa(viewModel);
                viewModel.CodigoEbsa = codigoEbsa;
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// /Salva um produto e todas as especificações tecnicas
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("SalvarProduto")]
        public async Task<IActionResult> SalvarProduto([FromForm] string viewModel, [FromForm] IFormFile File)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<ProdutoCUPViewModel>(viewModel);
                long codigoProduto = await _produtoCUPAppService.SalvarProdutoCUP(json);
                json.IdProduto = codigoProduto;

                if (File != null)
                {
                    long size = File.Length;

                    if (File.Length > 0)
                    {
                        string filePath = "";
#if DEBUG
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "FilesProduto\\Upload\\";
#endif
#if (!DEBUG)
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\Upload\\";
#endif

                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        string nomeArquivo = File.FileName;
                        Guid guid = Guid.NewGuid();

                        using (var stream = System.IO.File.Create(filePath + guid + File.FileName))
                        {
                            await File.CopyToAsync(stream);
                        }

                        ArquivoProdutoCUPViewModel arq = new ArquivoProdutoCUPViewModel
                        {
                            IdProduto = json.IdProduto,
                            ProdutoEBSA = json.EBSA,
                            NomeArquivo = guid + File.FileName,
                            CaminhoArquivo = filePath + guid + File.FileName,
                            DataCadastro = DateTime.Now
                        };

                        await _produtoCUPAppService.SalvarArquivoCUP(arq);
                    }
                }
                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = codigoProduto + "S";
                GravarLog(log);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// /Exclui um produto e todas as especificações tecnicas
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("ExcluirProduto")]
        public async Task<IActionResult> ExcluirProduto([FromForm] long IdProduto)
        {
            try
            {
                await _produtoCUPAppService.ExcluirProdutoCUP(IdProduto);
                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = IdProduto + "E";
                GravarLog(log);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Atualiza um produto e todas as especificações tecnicas
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("AtualizarProduto")]
        public async Task<IActionResult> AtualizarProduto([FromForm] string viewModel, [FromForm] IFormFile File)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<ProdutoCUPViewModel>(viewModel);
                await _produtoCUPAppService.AtualizarProdutoCUP(json);


                if (File != null)
                {
                    long size = File.Length;

                    if (File.Length > 0)
                    {
                        string filePath = "";
#if DEBUG
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "FilesProduto\\Upload\\";
#endif
#if (!DEBUG)
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\Upload\\";
#endif

                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        string nomeArquivo = File.FileName;
                        Guid guid = Guid.NewGuid();

                        using (var stream = System.IO.File.Create(filePath + guid + File.FileName))
                        {
                            await File.CopyToAsync(stream);
                        }

                        ArquivoProdutoCUPViewModel arq = new ArquivoProdutoCUPViewModel
                        {
                            IdProduto = json.IdProduto,
                            ProdutoEBSA = json.EBSA,
                            NomeArquivo = guid + File.FileName,
                            CaminhoArquivo = filePath + guid + File.FileName,
                            DataCadastro = DateTime.Now
                        };

                        await _produtoCUPAppService.AtualizarArquivoCUP(arq);
                    }
                }
                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = json.IdProduto + "A";
                GravarLog(log);

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// /Publica um produto e todas as especificações tecnicas
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("PublicarProduto")]
        public async Task<IActionResult> PublicarProduto([FromForm] long IdProduto)
        {
            try
            {
                await _produtoCUPAppService.PublicarProdutoCUP(IdProduto);

                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// /Salva o link e o preco e todas as especificações tecnicas
        /// </summary>
        /// <returns></returns>
        [HttpPost("SalvarProdutoLink")]
        public async Task<IActionResult> SalvarProdutoLink([FromBody] ProdutoLinkViewModel viewModel)
        {
            try
            {
                await _produtoCUPAppService.SalvarProdutoLinkCUP(viewModel.IdProduto, viewModel.Url, viewModel.Preco);
                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = viewModel.IdProduto + "A";
                GravarLog(log);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega todos os produtos do CUP
        /// com filtro para IdProduto, Titulo
        /// ISBN, EBSA e Ano Educação
        /// </summary>
        /// <returns>lista de ProdutoCUPViewModel</returns>
        [HttpGet("CarregarProdutosRelatorioTitulo")]
        public IActionResult CarregarProdutosRelatorioTitulo(string Titulo)
        {
            try
            {
                var Produto = _produtoCUPAppService.CarregarProdutosRelatorioTituloCUP(Titulo.Replace("\r", "").Replace("\n", ""));
                return Ok(Produto);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Salva as preferencias do usuario
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("SalvarPreferenciasRelatorio")]
        public IActionResult SalvarPreferenciasRelatorio([FromForm] string viewModel)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                _produtoCUPAppService.SalvarPreferenciasRelatorioCUP(viewModel, idUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("AtualizarPreferenciasRelatorio")]
        public IActionResult AtualizarPreferenciasRelatorio([FromForm] string viewModel)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                _produtoCUPAppService.AtualizarPreferenciasRelatorioCUP(viewModel, idUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Salva as preferencias do usuario
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpGet("CarregarPreferenciasRelatorio")]
        public IActionResult CarregarPreferenciasRelatorio()
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                string preferencia = _produtoCUPAppService.CarregarPreferenciasRelatorioCUP(idUsuario);
                return Ok(preferencia);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Autoria

        /// <summary>
        /// carrega autoria de um produto usando como parametro
        /// o id do produto
        /// </summary>
        /// <param name="IdProduto"></param>
        /// <returns></returns>
        [HttpGet("CarregarAutoriaPorIdProduto")]
        public IActionResult CarregarAutoriaPorIdProduto(long IdProduto)
        {
            try
            {
                var Autorias = _produtoCUPAppService.CarregarAutoriaPorIdProdutoCUP(IdProduto);
                return Ok(Autorias);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega os autor dos produtos CUP
        /// </summary>
        /// <returns>lista de AutorCUPViewModel</returns>
        [HttpGet("CarregarAutores")]
        public IActionResult CarregarAutores(string NomeContratual)
        {
            try
            {
                var lstAutores = _produtoCUPAppService.CarregarAutoresCUP(NomeContratual);
                return Ok(lstAutores.OrderBy(a => a.NomeContratual));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega as funcoes da autoria CUP
        /// </summary>
        /// <returns>lista de FUncaoCUPViewModel</returns>
        [HttpGet("CarregarFuncoes")]
        public IActionResult CarregarFuncoes()
        {
            try
            {
                var lstFuncoes = _produtoCUPAppService.CarregarFuncoesCUP();
                return Ok(lstFuncoes.OrderBy(a => a.DescricaoFuncao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// /Salva um autoria e todas as especificações tecnicas
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("SalvarAutoria")]
        public async Task<IActionResult> SalvarAutoria([FromBody] AutoriaCUPViewModel viewModel)
        {
            try
            {
                long codigoAutoria = await _produtoCUPAppService.SalvarAutoriaCUP(viewModel);
                viewModel.IdAutoria = codigoAutoria;
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Atualiza um autoria e todas as especificações tecnicas
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("AtualizarAutoria")]
        public async Task<IActionResult> AtualizarAutoria([FromBody] AutoriaCUPViewModel viewModel)
        {
            try
            {
                await _produtoCUPAppService.AtualizarAutoriaCUP(viewModel);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Exclui um autoria e todas as especificações tecnicas
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("ExcluirAutoria")]
        public async Task<IActionResult> ExcluirAutoria([FromForm] long IdAutoria)
        {
            try
            {
                await _produtoCUPAppService.ExcluirAutoriaCUP(IdAutoria);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Arquivo

        /// <summary>
        /// salva um arquivo ou imagem, necessário o 
        /// envio da origem
        /// </summary>
        /// <param name="IdProduto"></param>
        /// <param name="Ebsa"></param>
        /// <param name="Origem"></param>
        /// <param name="Files"></param>
        /// <returns></returns>
        [HttpPost("SalvarArquivoEbook")]
        public async Task<ActionResult> SalvarArquivoEbook([FromForm] string IdProduto, [FromForm] string Ebsa, [FromForm] List<IFormFile> Files)
        {
            try
            {
                long size = Files.Sum(f => f.Length);

                foreach (var formFile in Files)
                {
                    if (formFile.Length > 0)
                    {
                        string filePath = "";
#if DEBUG
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "FilesProduto\\Upload\\";
#endif
#if (!DEBUG)
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\Upload\\";
#endif



                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        string nomeArquivo = formFile.FileName;
                        Guid guid = Guid.NewGuid();

                        using (var stream = System.IO.File.Create(filePath + guid + formFile.FileName))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        ArquivoProdutoCUPViewModel arq = new ArquivoProdutoCUPViewModel
                        {
                            IdProduto = Convert.ToInt64(IdProduto.Substring(1, IdProduto.Length - 2)),
                            ProdutoEBSA = Ebsa.Substring(1, Ebsa.Length - 2),
                            NomeArquivo = guid + formFile.FileName,
                            CaminhoArquivo = filePath + guid + formFile.FileName,
                            DataCadastro = DateTime.Now
                        };
                        await _produtoCUPAppService.SalvarEpubCUP(arq);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpPost("SalvarDocAutoria")]
        public async Task<ActionResult> SalvarDocAutoria([FromForm] long IdAutoria, [FromForm] IFormFile File)
        {
            try
            {
                ArquivoAutoriaCUPViewModel arquivo = new ArquivoAutoriaCUPViewModel();

                if (File != null)
                {
                    long size = File.Length;

                    if (File.Length > 0)
                    {
                        string filePath = "";
#if DEBUG
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "FilesAutoria\\Upload\\";
#endif
#if (!DEBUG)
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\FilesAutoria\\Upload\\";
#endif

                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        string nomeArquivo = File.FileName;
                        Guid guid = Guid.NewGuid();

                        using (var stream = System.IO.File.Create(filePath + guid + File.FileName))
                        {
                            await File.CopyToAsync(stream);
                        }

                        ArquivoProdutoCUPViewModel arq = new ArquivoProdutoCUPViewModel
                        {
                            IdProduto = IdAutoria,
                            NomeArquivo = guid + File.FileName,
                            CaminhoArquivo = filePath + guid + File.FileName,
                            DataCadastro = DateTime.Now
                        };

                        arquivo.IdDoc = await _produtoCUPAppService.SalvarArquivoAutoriaCUP(arq);
                        arquivo.IdAutoria = IdAutoria;
                        arquivo.Caminho = filePath + guid + File.FileName;
                        arquivo.Nome = guid + File.FileName;
                    }
                }


                return Ok(arquivo);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpPost("ExcluirDocAutoria")]
        public async Task<ActionResult> ExcluirDocAutoria([FromForm] long IdDoc)
        {
            try
            {
                await _produtoCUPAppService.ExcluirArquivoAutoriaCUP(IdDoc);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpGet("BuscarDocAutoria")]
        public IActionResult BuscarDocAutoria(long IdAutoria)
        {
            try
            {
                var ret = new List<ArquivoAutoriaCUPViewModel>();
                ret = _produtoCUPAppService.BuscarArquivoAutoriaCUP(IdAutoria);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("DownloadArquivoAutoria")]
        public IActionResult DownloadArquivoAutoria([FromForm] string arquivo)
        {
            try
            {
                FileStream file = null;

                if (System.IO.File.Exists(arquivo))
                {
                    file = System.IO.File.OpenRead(arquivo);
                }

                byte[] result = new byte[file.Length];

                file.Read(result, 0, result.Length);

                if (Path.GetExtension(arquivo).Equals(".pdf"))
                {
                    return Ok(File(fileContents: result,
                                contentType: "application/pdf",
                                fileDownloadName: Path.GetFileName(arquivo)
                                ));
                }
                else
                {
                    return Ok(File(fileContents: result,
                               contentType: "image/" + Path.GetExtension(arquivo).Replace(".", ""),
                               fileDownloadName: Path.GetFileName(arquivo)
                               ));
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        #endregion

        #region Mercado

        /// <summary>
        /// Carrega os mercados dos produtos CUP
        /// </summary>
        /// <returns>lista de MercadoCUPViewModel</returns>
        [HttpGet("CarregarMercados")]
        public IActionResult CarregarMercados()
        {
            try
            {
                var lstMercados = _produtoCUPAppService.CarregarMercadoCUP();
                return Ok(lstMercados.OrderBy(a => a.DescricaoMercado));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Selo

        /// <summary>
        /// Carrega os selos protheus dos produtos CUP
        /// </summary>
        /// <returns>lista de SeloCUPViewModel</returns>
        [HttpGet("CarregarSelos")]
        public IActionResult CarregarSelos()
        {
            try
            {
                var lstSelos = _produtoCUPAppService.CarregarSeloCUP();
                return Ok(lstSelos.OrderBy(a => a.DescricaoSelo));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Tipo

        /// <summary>
        /// Carrega os tipos dos produtos CUP
        /// </summary>
        /// <returns>lista de TipoCUPViewModel</returns>
        [HttpGet("CarregarTipos")]
        public IActionResult CarregarTipos()
        {
            try
            {
                var lstTipos = _produtoCUPAppService.CarregarTipoCUP();
                return Ok(lstTipos.OrderBy(a => a.DescricaoTipo));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega os tipos dos produtos CUP
        /// </summary>
        /// <returns>lista de TipoCUPViewModel</returns>
        [HttpGet("CarregarTiposEspec")]
        public IActionResult CarregarTiposEspec(string TipoEspec)
        {
            try
            {
                var lstTipos = _produtoCUPAppService.CarregarTipoEspecCUP(TipoEspec.Replace("\n", ""));
                return Ok(lstTipos.OrderBy(a => a.Descricao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Segmento

        /// <summary>
        /// Carrega os segmentos dos produtos CUP
        /// </summary>
        /// <returns>lista de SegmentoCUPViewModel</returns>
        [HttpGet("CarregarSegmentos")]
        public IActionResult CarregarSegmentos()
        {
            try
            {
                var lstSegmentos = _produtoCUPAppService.CarregarSegmentoCUP();
                return Ok(lstSegmentos.OrderBy(a => a.DescricaoSegmento));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Composição

        /// <summary>
        /// Carrega os Composicao dos produtos CUP
        /// </summary>
        /// <returns>lista de ComposicaoCUPViewModel</returns>
        [HttpGet("CarregarComposicao")]
        public IActionResult CarregarComposicao()
        {
            try
            {
                var lstComposicao = _produtoCUPAppService.CarregarComposicaoCUP();
                return Ok(lstComposicao.OrderBy(a => a.DescricaoComposicao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Ano

        /// <summary>
        /// Carrega os Ano dos produtos CUP
        /// </summary>
        /// <returns>lista de AnoCUPViewModel</returns>
        [HttpGet("CarregarAno")]
        public IActionResult CarregarAno()
        {
            try
            {
                var lstAno = _produtoCUPAppService.CarregarAnoCUP();
                return Ok(lstAno.OrderBy(a => a.DescricaoAno));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega os Ano dos produtos CUP a partir do IdSegmento
        /// </summary>
        /// <returns>lista de AnoCUPViewModel</returns>
        [HttpGet("CarregarAnoPorIdSegmento")]
        public IActionResult CarregarAnoPorIdSegmento(int IdSegmento)
        {
            try
            {
                var lstAno = _produtoCUPAppService.CarregarAnoPorIdSegmentoCUP(IdSegmento);
                return Ok(lstAno.OrderBy(a => a.DescricaoAno));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Faixa Etaria

        /// <summary>
        /// Carrega os Faixa Etaria dos produtos CUP
        /// </summary>
        /// <returns>lista de FaixaEtariaCUPViewModel</returns>
        [HttpGet("CarregarFaixaEtaria")]
        public IActionResult CarregarFaixaEtaria()
        {
            try
            {
                var lstFaixaEtaria = _produtoCUPAppService.CarregarFaixaEtariaCUP();
                return Ok(lstFaixaEtaria.OrderBy(a => a.DescricaoFaixaEtaria));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega os Faixa Etaria dos produtos CUP pelo IdAno
        /// </summary>
        /// <returns>lista de FaixaEtariaCUPViewModel</returns>
        [HttpGet("CarregarFaixaEtariaPorIdAno")]
        public IActionResult CarregarFaixaEtariaPorIdAno(int IdAno)
        {
            try
            {
                var lstFaixaEtaria = _produtoCUPAppService.CarregarFaixaEtariaPorIdAnoCUP(IdAno);
                return Ok(lstFaixaEtaria.OrderBy(a => a.DescricaoFaixaEtaria));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Disciplina

        /// <summary>
        /// Carrega os Disciplina dos produtos CUP
        /// </summary>
        /// <returns>lista de DisciplinaCUPViewModel</returns>
        [HttpGet("CarregarDisciplina")]
        public IActionResult CarregarDisciplina()
        {
            try
            {
                var lstDisciplina = _produtoCUPAppService.CarregarDisciplinaCUP();
                return Ok(lstDisciplina.OrderBy(a => a.DescricaoDisciplina));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Conteudo Disciplinar

        /// <summary>
        /// Carrega os Conteudos Disciplinares dos produtos CUP
        /// </summary>
        /// <returns>lista de ConteudoDisciplinarCUPViewModel</returns>
        [HttpGet("CarregarConteudoDisciplinar")]
        public IActionResult CarregarConteudoDisciplinar()
        {
            try
            {
                var lstConteudoDisciplinar = _produtoCUPAppService.CarregarConteudoDisciplinarCUP();
                return Ok(lstConteudoDisciplinar.OrderBy(a => a.DescricaoConteudoDisciplinar));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Tema

        /// <summary>
        /// Carrega os Tema Transversal dos produtos CUP
        /// </summary>
        /// <returns>lista de TemaCUPViewModel</returns>
        [HttpGet("CarregarTemaTransversal")]
        public IActionResult CarregarTemaTransversal()
        {
            try
            {
                var lstTemaTransversal = _produtoCUPAppService.CarregarTemaTransversalCUP();
                return Ok(lstTemaTransversal.OrderBy(a => a.DescricaoTema));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega os Tema Norteador dos produtos CUP
        /// </summary>
        /// <returns>lista de TemaCUPViewModel</returns>
        [HttpGet("CarregarTemaNorteador")]
        public IActionResult CarregarTemaNorteador()
        {
            try
            {
                var lstTemaNorteador = _produtoCUPAppService.CarregarTemaNorteadorCUP();
                return Ok(lstTemaNorteador.OrderBy(a => a.DescricaoTema));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Data Especial

        /// <summary>
        /// Carrega os Data Especial dos produtos CUP
        /// </summary>
        /// <returns>lista de DataEspecialCUPViewModel</returns>
        [HttpGet("CarregarDataEspecial")]
        public IActionResult CarregarDataEspecial()
        {
            try
            {
                var lstDataEspecial = _produtoCUPAppService.CarregarDataEspecialCUP();
                return Ok(lstDataEspecial.OrderBy(a => a.DescricaoDataEspecial));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Assunto

        /// <summary>
        /// Carrega os Assuntos dos produtos CUP
        /// </summary>
        /// <returns>lista de AssuntoCUPViewModel</returns>
        [HttpGet("CarregarAssunto")]
        public IActionResult CarregarAssunto()
        {
            try
            {
                var lstAssunto = _produtoCUPAppService.CarregarAssuntoCUP();
                return Ok(lstAssunto.OrderBy(a => a.DescricaoAssunto));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Genero Textual

        /// <summary>
        /// Carrega os Genero Textual dos produtos CUP
        /// </summary>
        /// <returns>lista de GeneroTextualCUPViewModel</returns>
        [HttpGet("CarregarGeneroTextual")]
        public IActionResult CarregarGeneroTextual()
        {
            try
            {
                var lstGeneroTextual = _produtoCUPAppService.CarregarGeneroTextualCUP();
                return Ok(lstGeneroTextual.OrderBy(a => a.DescricaoGeneroTextual));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Versao

        /// <summary>
        /// Carrega os Versao dos produtos CUP
        /// </summary>
        /// <returns>lista de VersaoCUPViewModel</returns>
        [HttpGet("CarregarVersao")]
        public IActionResult CarregarVersao()
        {
            try
            {
                var lstVersao = _produtoCUPAppService.CarregarVersaoCUP();
                return Ok(lstVersao.OrderBy(a => a.DescricaoVersao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Colecao

        /// <summary>
        /// Carrega os Colecao dos produtos CUP
        /// </summary>
        /// <returns>lista de ColecaoCUPViewModel</returns>
        [HttpGet("CarregarColecao")]
        public IActionResult CarregarColecao()
        {
            try
            {
                var lstColecao = _produtoCUPAppService.CarregarColecaoCUP();
                return Ok(lstColecao.OrderBy(a => a.DescricaoColecao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Midia

        /// <summary>
        /// Carrega os Midia dos produtos CUP
        /// </summary>
        /// <returns>lista de MidiaCUPViewModel</returns>
        [HttpGet("CarregarMidia")]
        public IActionResult CarregarMidia()
        {
            try
            {
                var lstMidia = _produtoCUPAppService.CarregarMidiaCUP();
                return Ok(lstMidia.OrderBy(a => a.DescricaoMidia));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Unidade de Medida

        /// <summary>/
        /// Carrega os Unidade de Medida dos produtos CUP
        /// </summary>
        /// <returns>lista de UnidadeMedidaCUPViewModel</returns>
        [HttpGet("CarregarUnidadeMedida")]
        public IActionResult CarregarUnidadeMedida()
        {
            try
            {
                var lstMidia = _produtoCUPAppService.CarregarUnidadeMedidaCUP();
                return Ok(lstMidia.OrderBy(a => a.DescricaoUnidadeMedida));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Plataforma

        /// <summary>/
        /// Carrega os Plataforma dos produtos CUP
        /// </summary>
        /// <returns>lista de PlataformaCUPViewModel</returns>
        [HttpGet("CarregarPlataforma")]
        public IActionResult CarregarPlataforma()
        {
            try
            {
                var lstPlataforma = _produtoCUPAppService.CarregarPlataformaCUP();
                return Ok(lstPlataforma.OrderBy(a => a.DescricaoPlataforma));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        /// <summary>
        /// Carrega as plataformas referentes a midia escolhida
        /// </summary>
        /// <param name="IdMidia">identificador da midia</param>
        /// <returns>lista de PlataformaCUPViewModel</returns>
        [HttpGet("CarregarPlataformaPorIdMidia")]
        public IActionResult CarregarPlataformaPorIdMidia(int IdMidia)
        {
            try
            {
                var lstPlataforma = _produtoCUPAppService.CarregarPlataformaPorIdMidiaCUP(IdMidia);
                return Ok(lstPlataforma.OrderBy(a => a.DescricaoPlataforma));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Status

        /// <summary>/
        /// Carrega os Status dos produtos CUP
        /// </summary>
        /// <returns>lista de StatusCUPViewModel</returns>
        [HttpGet("CarregarStatus")]
        public IActionResult CarregarStatus()
        {
            try
            {
                var lstStatus = _produtoCUPAppService.CarregarStatusCUP();
                return Ok(lstStatus.OrderBy(a => a.DescricaoStatus));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Origem

        /// <summary>/
        /// Carrega os Origem dos produtos CUP
        /// </summary>
        /// <returns>lista de OrigemCUPViewModel</returns>
        [HttpGet("CarregarOrigem")]
        public IActionResult CarregarOrigem()
        {
            try
            {
                var lstOrigem = _produtoCUPAppService.CarregarOrigemCUP();
                return Ok(lstOrigem.OrderBy(a => a.DescricaoOrigem));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message };
                return StatusCode(500, err);
            }
        }

        #endregion

        #region Tipo Produto

        /// <summary>/
        /// Carrega os Tipo Produto dos produtos CUP
        /// </summary>
        /// <returns>lista de TipoProdutoCUPViewModel</returns>
        [HttpGet("CarregarTipoProduto")]
        public IActionResult CarregarTipoProduto()
        {
            try
            {
                var lstTipoProduto = _produtoCUPAppService.CarregarTipoProdutoCUP();
                return Ok(lstTipoProduto.OrderBy(a => a.DescricaoTipoProduto));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

        #region Segmento Protheus

        /// <summary>/
        /// Carrega os Segmento Protheus dos produtos CUP
        /// </summary>
        /// <returns>lista de SegmentoProtheusCUPViewModel</returns>
        [HttpGet("CarregarSegmentoProtheus")]
        public IActionResult CarregarSegmentoProtheus()
        {
            try
            {
                var lstSegmentoProtheus = _produtoCUPAppService.CarregarSegmentoProtheusCUP();
                return Ok(lstSegmentoProtheus.OrderBy(a => a.DescricaoSegmentoProtheus));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        #endregion

    }
}

