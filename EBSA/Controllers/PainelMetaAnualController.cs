using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PainelMetaAnualController : BaseController
    {
        private readonly IPainelMetaAnualAppService _painelMetaAnualAppService;
        IAcompanhamentoMetaConsultorAppService _acompanhamentoMetaConsultorAppService;

        public PainelMetaAnualController(ILogAppService logAppService, IPainelMetaAnualAppService painelMetaAnualAppService, IAcompanhamentoMetaConsultorAppService acompanhamentoMetaConsultorAppService) : base(logAppService)
        {
            _painelMetaAnualAppService = painelMetaAnualAppService;
            _acompanhamentoMetaConsultorAppService = acompanhamentoMetaConsultorAppService;
        }

        [HttpGet("Consultar")]
        public IActionResult ConsultaPainelMetaAnual(Guid? MetaAnualId, Guid? MetaId, Guid? LinhaMetaId, Guid? GrupoPainelId, Guid? CargoId, Guid? UsuarioId, Guid? StatusId, Guid? PeriodoId)
        {
            try
            {
                IEnumerable<PainelMetaAnualViewModel> metasanuais = _acompanhamentoMetaConsultorAppService.CalculoPotencial(MetaAnualId, MetaId, LinhaMetaId, GrupoPainelId, CargoId, UsuarioId, StatusId, PeriodoId);
                return Ok(metasanuais.OrderBy(a => a.Indicador));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Salvar")]
        public async Task<IActionResult> SalvarPainelMetaAnual([FromBody] PainelMetaAnualViewModel meta)
        {
            try
            {
                await _painelMetaAnualAppService.SalvarPainelMetaAnual(meta);
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