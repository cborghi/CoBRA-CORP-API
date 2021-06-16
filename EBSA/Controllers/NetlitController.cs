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

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class NetlitController : BaseController
    {
        public readonly INetlitAppService _netlitAppService;

        public NetlitController(INetlitAppService netlitAppService, ILogAppService logAppService) : base(logAppService)
        {
            _netlitAppService = netlitAppService;
        }

        #region Pais e Professores


        [HttpGet("CarregarConteudoPP")]
        public IActionResult CarregarConteudoPP(int NumeroPagina, int RegistrosPagina, string Filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(Filtro)) Filtro = "";
                var Conteudo = _netlitAppService.CarregarConteudoPP(NumeroPagina, RegistrosPagina, Filtro.Replace("\r", "").Replace("\n", ""));
                return Ok(Conteudo);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("CarregarConteudoNetLitPP")]
        public async Task<IActionResult> CarregarConteudoNetLitPP(int NumeroPagina, int RegistrosPagina)
        {
            try
            {
                var Conteudo = await _netlitAppService.CarregarConteudoNetLitPP(NumeroPagina, RegistrosPagina, "");
                return Ok(Conteudo);
            } 
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("SalvarConteudoPP")]
        public async Task<ActionResult> SalvarConteudoPP([FromForm] IFormFile File, [FromForm] string PrimeiraPagina)
        {
            try
            {
                int idUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);

                if (File.Length > 0)
                {
                    string filePath = "";
#if DEBUG
                    filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "Files\\ConteudoPPUpload\\";
#endif
#if (!DEBUG)
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\ConteudoPPUpload\\";
#endif



                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    Guid guid = Guid.NewGuid();

                    using (var stream = System.IO.File.Create(filePath + guid + File.FileName))
                    {
                        await File.CopyToAsync(stream);
                    }
                    ConteudoPPViewModel arq = new ConteudoPPViewModel
                    {
                        Nome = File.FileName,
                        NomeGuid = guid + File.FileName,
                        Caminho = filePath + guid + File.FileName,
                        DtCadastro = DateTime.Now,
                        Usuario = idUsuario,
                        Ativo = true,
                        PrimeiraPagina = PrimeiraPagina
                    };
                    await _netlitAppService.SalvarConteudoPP(arq);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("ExcluirConteudoPP")]
        public async Task<IActionResult> ExcluirConteudoPP([FromForm] int IdConteudo)
        {
            try
            {
                await _netlitAppService.ExcluirConteudoPP(IdConteudo);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("DownloadConteudoPP")]
        public IActionResult DownloadConteudoPP([FromForm] string Caminho)
        {
            try
            {
                FileStream file = null;
                if (System.IO.File.Exists(Caminho))
                {
                    file = System.IO.File.OpenRead(Caminho);
                }

                byte[] result = new byte[file.Length];

                file.Read(result, 0, result.Length);
                return Ok(File(fileContents: result,
                            contentType: "application/pdf",
                            fileDownloadName: Path.GetFileName(Caminho)
                            ));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        #endregion

        #region FlippingBook

        [HttpGet("CarregarPastasFlipp")]
        public IActionResult CarregarPastasFlipp()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(System.Reflection.Assembly.GetEntryAssembly().Location.Replace(@"\bin\Debug\netcoreapp2.2", "").Replace(@"\CoBRA.API.dll", "") + @"\Files\FlippingBook\", "*.*", SearchOption.TopDirectoryOnly);
                List<CaminhoFlippingViewModel> lstCaminho = new List<CaminhoFlippingViewModel>();
                foreach (var item in dirs)
                {
                    CaminhoFlippingViewModel cam = new CaminhoFlippingViewModel();
                    cam.Caminho = item;
                    lstCaminho.Add(cam);
                }
                return Ok(lstCaminho);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("CarregarPastasFlippIdProduto")]
        public IActionResult CarregarPastasFlippIdProduto(long IdProduto)
        {
            try
            {
                var Conteudo = _netlitAppService.CarregarPastasFlippId(IdProduto);
                return Ok(Conteudo);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("SalvarCaminhoFlipp")]
        public async Task<ActionResult> SalvarCaminhoFlipp([FromBody] CaminhoFlippingViewModel caminho)
        {
            try
            {
                _netlitAppService.SalvarCaminhoFlipp(caminho);
                return Ok();
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
