using System;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    // [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class PainelMetaController : BaseController
    {
        public readonly IPainelIndicadorMetaAppService _painelIndicadorMetaAppService;
        public readonly IPainelMetaFinanceiraAppService _painelMetaFinanceiraAppService;
        public readonly IPainelIndividualAppService _painelIndividualAppService;

        public PainelMetaController(
            IPainelIndicadorMetaAppService painelIndicadorMetaAppService,
            IPainelMetaFinanceiraAppService painelMetaFinanceiraAppService,
            IPainelIndividualAppService painelIndividualAppService,
            ILogAppService logAppService) : base(logAppService)
        {
            _painelIndicadorMetaAppService = painelIndicadorMetaAppService;
            _painelMetaFinanceiraAppService = painelMetaFinanceiraAppService;
            _painelIndividualAppService = painelIndividualAppService;
        }

        //Painel Indicador Meta
        [HttpGet("PainelIndicadorMetaAprovado")]
        public async Task<IActionResult> ListarPainelIndicadorMetaAprovado()
        {
            try
            {
                var result = await _painelIndicadorMetaAppService.ListarPainelIndicadorMetaAprovado();
                return Ok(result);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("PainelIndicadorMetaPendente")]
        public async Task<IActionResult> ListarPainelIndicadorMetaPendente()
        {
            try
            {
                var result = await _painelIndicadorMetaAppService.ListarPainelIndicadorMetaPendente();
                return Ok(result);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("PainelMetaFinanceiraAprovado")]
        public async Task<IActionResult> ListarPainelMetaFinanceiraAprovado()
        {
            try
            {
                var result = await _painelMetaFinanceiraAppService.ListarPainelMetaFinanceiraAprovado();
                return Ok(result);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("PainelMetaFinanceiraPendente")]
        public async Task<IActionResult> ListarPainelMetaFinanceiraPendente()
        {
            try
            {
                var result = await _painelMetaFinanceiraAppService.ListarPainelMetaFinanceiraPendente();
                return Ok(result);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("painelIndividualAprovado")]
        public async Task<IActionResult> ListarPainelIndividualAprovado()
        {
            try
            {
                var result = await _painelIndividualAppService.ListarPainelIndividualAprovado();
                return Ok(result);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("painelIndividualPendente")]
        public async Task<IActionResult> ListarPainelIndividualPendente()
        {
            try
            {
                var result = await _painelIndividualAppService.ListarPainelIndividualPendente();
                return Ok(result);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

