using System;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    // [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class AcompanhamentoMetaConsultorController : BaseController
    {
        public readonly IAcompanhamentoMetaConsultorAppService _acompanhamentoMetaConsultorAppService;

        public AcompanhamentoMetaConsultorController(
            IAcompanhamentoMetaConsultorAppService acompanhamentoMetaConsultorAppService,
            ILogAppService logAppService) : base(logAppService)
        {
            _acompanhamentoMetaConsultorAppService = acompanhamentoMetaConsultorAppService;
        }

        [HttpGet("ObterAcompanhamentoMetaConsultor")]
        public async Task<IActionResult> ObterAcompanhamento(string idUsuario, string idStatus)
        {
            try
            {
                var result = await _acompanhamentoMetaConsultorAppService.ObterAcompanhamento(idUsuario, idStatus, null);
                var cabecalhoRelatorio = await _acompanhamentoMetaConsultorAppService.BuscarDadosCabecalhoRelatorio(Guid.Parse(idUsuario));
                return Ok(new { cabecalho = cabecalhoRelatorio, resultado = result });
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObterAcompanhamentoMetaConsultorInit/{login}")]
        public IActionResult ObterAcompanhamentoInit(string login)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

