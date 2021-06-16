using System;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    // [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class AprovacaoMetaFinanceiraController : BaseController
    {
        public readonly IAprovacaoMetaFinanceiraAppService _aprovacaoMetaFinanceiraAppService;

        public AprovacaoMetaFinanceiraController(
            IAprovacaoMetaFinanceiraAppService aprovacaoMetaFinanceiraAppService,
            ILogAppService logAppService) : base(logAppService)
        {
            _aprovacaoMetaFinanceiraAppService = aprovacaoMetaFinanceiraAppService;
        }

        [HttpGet("ObterAprovacaoMetaFinanceira/{id}")]
        public async Task<IActionResult> ObterItem(string id)
        {
            try
            {
                var result = await _aprovacaoMetaFinanceiraAppService.ObterItem(id);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("GravarAprovacaoMetaFinanceira")]
        public async Task<IActionResult> GravarItem([FromBody]MetaFinanceiraViewModel dto)
        {
            try
            {
                await _aprovacaoMetaFinanceiraAppService.GravarItem(dto);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


    }
}

