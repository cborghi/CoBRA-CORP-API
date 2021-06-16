using System;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    // [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class AprovacaoGrupoController : BaseController
    {
        public readonly IAprovacaoGrupoAppService _aprovacaoGrupoAppService;

        public AprovacaoGrupoController(
            IAprovacaoGrupoAppService aprovacaoGrupoAppService,
            ILogAppService logAppService) : base(logAppService)
        {
            _aprovacaoGrupoAppService = aprovacaoGrupoAppService;
        }

        [HttpGet("ObterAprovacaoGrupo/{id}")]
        public async Task<IActionResult> ObterItem(string id)
        {
            try
            {
                var result = await _aprovacaoGrupoAppService.ObterItem(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GravarAprovacaoGrupo")]
        public async Task<IActionResult> GravarItem([FromBody]AprovacaoGrupoViewModel dto)
        {
            try
            {
                await _aprovacaoGrupoAppService.GravarItem(dto);
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

