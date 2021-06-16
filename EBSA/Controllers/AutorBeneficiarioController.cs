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
    public class AutorBeneficiarioController : BaseController
    {
        public readonly IAutorBeneficiarioAppService _autorBeneficiarioAppService;

        public AutorBeneficiarioController(IAutorBeneficiarioAppService autorBeneficiarioAppService, ILogAppService logAppService) : base(logAppService)
        {
            _autorBeneficiarioAppService = autorBeneficiarioAppService;
        }

        [HttpPost("SalvarAutor")]
        public async Task<IActionResult> SalvarAutor([FromForm] string viewModel)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<AutorDAViewModel>(viewModel);

                json.IdUsuarioInclusao = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                json.DataInclusao = DateTime.Now;

                int idAutorBeneficiario = await _autorBeneficiarioAppService.SalvarAutor(json);
                json.IdAutorBeneficiario = idAutorBeneficiario;

                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = "AutorBeneficiario " + idAutorBeneficiario + " Incluido";
                GravarLog(log);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("AtualizarAutor")]
        public async Task<IActionResult> AtualizarAutor([FromForm] string viewModel)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<AutorDAViewModel>(viewModel);

                json.IdUsuarioInclusao = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                json.DataInclusao = DateTime.Now;

                await _autorBeneficiarioAppService.AtualizarAutor(json);

                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = "AutorBeneficiario " + json.IdAutorBeneficiario + " Alterado";
                GravarLog(log);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("SalvarBeneficiario")]
        public async Task<IActionResult> SalvarBeneficiario([FromForm] string viewModel)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<BeneficiarioDAViewModel>(viewModel);

                json.IdUsuarioInclusao = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                json.DataInclusao = DateTime.Now;

                int idAutorBeneficiario = await _autorBeneficiarioAppService.SalvarBeneficiario(json);
                json.IdAutorBeneficiario = idAutorBeneficiario;

                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = "AutorBeneficiario " + idAutorBeneficiario + " Incluido";
                GravarLog(log);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("AtualizarBeneficiario")]
        public async Task<IActionResult> AtualizarBeneficiario([FromForm] string viewModel)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<BeneficiarioDAViewModel>(viewModel);

                json.IdUsuarioInclusao = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                json.DataInclusao = DateTime.Now;

                await _autorBeneficiarioAppService.AtualizarBeneficiario(json);

                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = "AutorBeneficiario " + json.IdAutorBeneficiario + " Alterado";
                GravarLog(log);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("BuscarAutorPorId")]
        public IActionResult BuscarAutorPorId(int id)
        {
            try
            {
                AutorDAViewModel autor = _autorBeneficiarioAppService.BuscarAutorPorId(id);
                return Ok(autor);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("BuscarBeneficiarioPorId")]
        public IActionResult BuscarBeneficiarioPorId(int id)
        {
            try
            {
                BeneficiarioDAViewModel autor = _autorBeneficiarioAppService.BuscarBeneficiarioPorId(id);
                return Ok(autor);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarAutoresBeneficiarios")]
        public IActionResult ListarAutoresBeneficiarios(int? idTipoCadastro, string tipoPessoa, int? idEstado, bool? ativo, int numeroPagina, int registrosPagina)
        {
            try
            {
                var AutoresBeneficiario = _autorBeneficiarioAppService.ListarAutoresBeneficiarios(idTipoCadastro, tipoPessoa, idEstado, ativo, numeroPagina, registrosPagina);
                return Ok(AutoresBeneficiario);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarAutoresBeneficiariosPorNome")]
        public IActionResult ListarAutoresBeneficiariosPorNome(string nome)
        {
            try
            {
                var AutoresBeneficiario = _autorBeneficiarioAppService.ListarAutoresPorNome(nome);
                return Ok(AutoresBeneficiario);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarContaBancaria")]
        public IActionResult ListarContaBancaria()
        {
            try
            {
                var lstContaBancaria = _autorBeneficiarioAppService.ListarContaBancaria();
                return Ok(lstContaBancaria.OrderBy(a => a.Descricao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarEstadoCivil")]
        public IActionResult ListarEstadoCivil()
        {
            try
            {
                var lstEstadoCivil = _autorBeneficiarioAppService.ListarEstadoCivil();
                return Ok(lstEstadoCivil.OrderBy(a => a.Descricao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarAutoresSimplificado")]
        public IActionResult ListarAutoresSimplificado(string filtro)
        {
            try
            {
                var lstAutores = _autorBeneficiarioAppService.ListarAutores(filtro);
                return Ok(lstAutores.OrderBy(a => a.Nome));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarEstados")]
        public IActionResult ListarEstados()
        {
            try
            {
                var lstEstado = _autorBeneficiarioAppService.ListarEstados();
                return Ok(lstEstado.OrderBy(a => a.Descricao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("SalvarArquivoAutorBeneficiario")]
        public async Task<ActionResult> SalvarArquivoAutorBeneficiario([FromForm] int idAutorBeneficiario, [FromForm] IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = "";
#if DEBUG
                    filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug\\netcoreapp2.2", "") + "FilesProduto\\AutorBeneficiario\\";
#endif
#if (!DEBUG)
                        filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\AutorBeneficiario\\";
#endif



                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    string nomeArquivo = file.FileName;
                    Guid guid = Guid.NewGuid();

                    using (var stream = System.IO.File.Create(filePath + guid + file.FileName))
                    {
                        await file.CopyToAsync(stream);
                    }
                    ArquivoAutorBeneficiarioViewModel arq = new ArquivoAutorBeneficiarioViewModel
                    {
                        IdArquivo = guid.ToString(),
                        IdAutorBeneficiario = idAutorBeneficiario,
                        Nome = guid + file.FileName,
                        CaminhoArquivo = filePath + guid + file.FileName,
                        DataCadastro = DateTime.Now
                    };
                    await _autorBeneficiarioAppService.SalvarArquivoAutorBeneficiario(arq);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ExcluirArquivoAutorBeneficiario")]
        public IActionResult ExcluirArquivoAutorBeneficiario(int idAutorBeneficiario, string NomeArquivo)
        {
            try
            {
                _autorBeneficiarioAppService.ExcluirArquivoAutorBeneficiario(idAutorBeneficiario, NomeArquivo);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarArquivoAutorBeneficiario")]
        public IActionResult ListarArquivoAutorBeneficiario(int idAutorBeneficiario)
        {
            try
            {
                var lstArquivos = _autorBeneficiarioAppService.ListarArquivoAutorBeneficiario(idAutorBeneficiario);
                return Ok(lstArquivos.OrderBy(a => a.Nome));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarCorrespondenciaAutorBeneficiario")]
        public IActionResult ListarCorrespondenciaAutorBeneficiario(int idAutorBeneficiario)
        {
            try
            {
                var lstCorrespondencia = _autorBeneficiarioAppService.ListarCorrespondenciaAutorBeneficiario(idAutorBeneficiario);
                return Ok(lstCorrespondencia.OrderBy(a => a.Agenda));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("SalvarCorrespondenciaAutorBeneficiario")]
        public async Task<IActionResult> SalvarCorrespondenciaAutorBeneficiario([FromForm] string viewModel)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<CorrespondenciaDAViewModel>(viewModel);

                var IdUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);

                int idCorrespondenciaAutorBeneficiario = await _autorBeneficiarioAppService.SalvarCorrespondenciaAutorBeneficiario(json, IdUsuario);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("AtualizarCorrespondenciaAutorBeneficiario")]
        public async Task<IActionResult> AtualizarCorrespondenciaAutorBeneficiario([FromForm] string viewModel)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<CorrespondenciaDAViewModel>(viewModel);

                var IdUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);

                await _autorBeneficiarioAppService.AtualizarCorrespondenciaAutorBeneficiario(json, IdUsuario);

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("ExcluirCorrespondenciaAutorBeneficiario")]
        public async Task<IActionResult> ExcluirCorrespondenciaAutorBeneficiario(int idCorrespondencia)
        {
            try
            {
                await _autorBeneficiarioAppService.ExcluirCorrespondenciaAutorBeneficiario(idCorrespondencia);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("AtualizarNomeCapaPorAutor")]
        public async Task<IActionResult> AtualizarNomeCapaPorAutor([FromForm] string viewModel)
        {
            try
            {
                NomeCapaViewModel json = JsonConvert.DeserializeObject<NomeCapaViewModel>(viewModel);

                var IdUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                json.IdUsuario = IdUsuario;

                await _autorBeneficiarioAppService.AtualizarNomeCapaPorAutor(json);
                var ret = _autorBeneficiarioAppService.ListarNomeCapaPorId(json.IdNomeCapa);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("IncluirNomeCapaPorAutor")]
        public async Task<IActionResult> IncluirNomeCapaPorAutor([FromForm] string viewModel)
        {
            try
            {
                NomeCapaViewModel json = JsonConvert.DeserializeObject<NomeCapaViewModel>(viewModel);

                var IdUsuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                json.IdUsuario = IdUsuario;

                int ret = await _autorBeneficiarioAppService.IncluirNomeCapaPorAutor(json);
                var ret2 = _autorBeneficiarioAppService.ListarNomeCapaPorId(ret);
                return Ok(ret2);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ExcluirNomeCapaPorAutor")]
        public IActionResult ExcluirNomeCapaPorAutor(int idAutorBeneficiario)
        {
            try
            {
                _autorBeneficiarioAppService.ExcluirNomeCapaPorAutor(idAutorBeneficiario);
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarCorrespondenciaAutorBeneficiarioId")]
        public IActionResult ListarCorrespondenciaAutorBeneficiarioId(int idCorrespondencia)
        {
            try
            {
                var lstCorrespondencia = _autorBeneficiarioAppService.ListarCorrespondenciaAutorBeneficiarioId(idCorrespondencia);
                return Ok(lstCorrespondencia);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }
    }
}
