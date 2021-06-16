using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadoPagamentoMetaController : BaseController
    {
        private readonly IResultadoPagamentoMetaAppService _resultadoPagamentoMetaAppService;

        public ResultadoPagamentoMetaController(ILogAppService logAppService, IResultadoPagamentoMetaAppService resultadoPagamentoMetaAppService) : base(logAppService)
        {
            _resultadoPagamentoMetaAppService = resultadoPagamentoMetaAppService;
        }

        [HttpGet("Consultar")]
        public async Task<IActionResult> ConsultarResultadoPagamentoMeta(int Percentual)
        {
            try
            {
                ResultadoPagamentoMetaViewModel resultadoPagamentoMeta = await _resultadoPagamentoMetaAppService.ConsultarResultadoPagamentoMeta(Percentual);

                return Ok(resultadoPagamentoMeta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}