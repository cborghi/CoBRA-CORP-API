using System;
using System.Linq;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    // [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class AprovacaoIndividualController : BaseController
    {
        public readonly IAprovacaoIndividualAppService _aprovacaoIndividualAppService;

        public AprovacaoIndividualController(
            IAprovacaoIndividualAppService aprovacaoIndividualAppService,
            ILogAppService logAppService) : base(logAppService)
        {
            _aprovacaoIndividualAppService = aprovacaoIndividualAppService;
        }

        [HttpGet("ObterAprovacaoIndividual/{id}")]
        public async Task<IActionResult> ListarAprovacaoIndividualAprovado(string id)
        {
            try
            {
                var result = await _aprovacaoIndividualAppService.ObterItem(id);
                result.ListaLinhaAprovacaoIndividual = result.ListaLinhaAprovacaoIndividual.OrderBy(a => a.Indicador).ToList();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("GravarAprovacaoIndividual")]
        public async Task<IActionResult> ListarAprovacaoIndividualPendente([FromBody]MetaIndividualViewModel dto)
        {
            try
            {
                await _aprovacaoIndividualAppService.GravarItem(dto);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


    }
}

