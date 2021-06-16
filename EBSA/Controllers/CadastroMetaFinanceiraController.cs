using System;
using System.Threading.Tasks;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    // [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class CadastroMetaFinanceiraController : BaseController
    {
        public readonly ICadastroMetaFinanceiraAppService _cadastroMetaFinanceiraAppService;

        public CadastroMetaFinanceiraController(
            ICadastroMetaFinanceiraAppService cadastroMetaFinanceiraAppService,
            ILogAppService logAppService) : base(logAppService)
        {
            _cadastroMetaFinanceiraAppService = cadastroMetaFinanceiraAppService;
        }

        [HttpGet("ObterMetaFinanceira")]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var result = await _cadastroMetaFinanceiraAppService.ObterTodos();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("ListarMetaFinanceira")]
        public async Task<IActionResult> ListarMetaFinanceira(Guid? PeriodoId)
        {
            try
            {
                if (PeriodoId == null)
                    PeriodoId = new Guid("00000000-0000-0000-0000-000000000000");
                AgrupadorMetaFinanceiraViewModel metasFinanceiras = 
                    await _cadastroMetaFinanceiraAppService.ListarMetaFinanceira(PeriodoId);

                return Ok(metasFinanceiras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("ConsultarMetaFinanceira")]
        public async Task<IActionResult> ConultarMetaFinanceira(Guid? UsuarioId, decimal? ValorMeta, Guid? MetaFinanceiraId)
        {
            try
            {
                var result = await _cadastroMetaFinanceiraAppService.ConsultarMetaFinanceira(UsuarioId, ValorMeta, MetaFinanceiraId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        
        [HttpPost("aprovarMetaFinanceira")]
        public async Task<IActionResult> AprovarMetaFinanceira([FromBody]MetaFinanceiraViewModel dto)
        {
            try
            {                
                var result = await _cadastroMetaFinanceiraAppService.AprovarItem(dto);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPost("GravarMetaFinanceira")]
        public async Task<IActionResult> GravarMetaFinanceira([FromBody]MetaFinanceiraViewModel dto)
        {
            try
            {

                var result = await _cadastroMetaFinanceiraAppService.GravarItem(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AlterarMetaFinanceira")]
        public async Task<IActionResult> AlterarMetaFinanceira([FromBody]MetaFinanceiraViewModel dto)
        {
            try
            {
                var result = await _cadastroMetaFinanceiraAppService.ModificarItem(dto);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}

