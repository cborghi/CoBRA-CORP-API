using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AmarracaoController : BaseController
    {
        public readonly IAmarracaoAppService _amarracaoAppService;

        public AmarracaoController(IAmarracaoAppService amarracaoAppService, ILogAppService logAppService) : base(logAppService)
        {
            _amarracaoAppService = amarracaoAppService;
        }

        [HttpGet("ListarRegraPagamento")]
        public IActionResult ListarRegraPagamento()
        {
            try
            {
                var lst = _amarracaoAppService.ListarRegraPagamento();
                return Ok(lst.OrderBy(a => a.DescricaoRegraPagamento));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarPrazoValidade")]
        public IActionResult ListarPrazoValidade()
        {
            try
            {
                var lst = _amarracaoAppService.ListarPrazoValidade();
                return Ok(lst.OrderBy(a => a.DescricaoPrazoValidade));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarBloqueioPagamento")]
        public IActionResult ListarBloqueioPagamento()
        {
            try
            {
                var lst = _amarracaoAppService.ListarBloqueioPagamento();
                return Ok(lst.OrderBy(a => a.DescricaoBloqueioPagamento));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarTipoContrato")]
        public IActionResult ListarTipoContrato()
        {
            try
            {
                var lst = _amarracaoAppService.ListarTipoContrato();
                return Ok(lst.OrderBy(a => a.DescricaoTipoContrato));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpGet("ListarTipoParticipacao")]
        public IActionResult ListarTipoParticipacao()
        {
            try
            {
                var lst = _amarracaoAppService.ListarTipoParticipacao();
                return Ok(lst.OrderBy(a => a.DescricaoTipoParticipacao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }

        [HttpPost("SalvarAmarracao")]
        public async Task<IActionResult> SalvarAmarracao([FromForm] string viewModel)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<AmarracaoViewModel>(viewModel);

                var usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);

                int idAmarracao = await _amarracaoAppService.SalvarAmarracao(json, usuario);
                json.IdAmarracao = idAmarracao;

                LogViewModel log = new LogViewModel();
                log.Usuario = Convert.ToInt32(ObterClaim(x => x.Type == "Id").Value);
                log.Data = DateTime.Now;
                log.Descricao = "Amarracao " + idAmarracao + " Incluido";
                GravarLog(log);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                ErroViewModel err = new ErroViewModel { Mensagem = "Erro ao executar a operação, contate o administrador do sistema", Trace = ex.Message }; return StatusCode(500, err);
            }
        }
    }
}
