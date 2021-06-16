using System;
using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class RequisicaoGenericaController : BaseController
    {
        public readonly IRequisicaoGenericaAppService _requisicaoGenericaAppService;

        public RequisicaoGenericaController(IRequisicaoGenericaAppService requisicaoGenericaAppService, ILogAppService logAppService) : base(logAppService)
        {
            _requisicaoGenericaAppService = requisicaoGenericaAppService;
        }

        [HttpGet("Requisicao")]
        public IActionResult RequisicaoGenerica()
        {
            try
            {
                return Ok(_requisicaoGenericaAppService.ObterRequisicoes());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("GerarRequisicao")]
        public ActionResult<RequisicaoGenericaViewModel> GerarRequisicao([FromBody] RequisicaoGenericaViewModel requisicao)
        {
            try
            {
                return Ok(_requisicaoGenericaAppService.GerarRequisicao(requisicao));
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }

}
