using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampoOrigemController : BaseController
    {
        private readonly ICampoOrigemAppService _campoOrigemAppService;

        public CampoOrigemController(ILogAppService logAppService, ICampoOrigemAppService campoOrigemAppService) : base(logAppService)
        {
            _campoOrigemAppService = campoOrigemAppService;
        }

        [HttpPost]
        public async Task<IActionResult> BuscarCampoOrigem([FromBody]TabelaOrigemViewModel tabela)
        {
            try
            {
                IEnumerable<CampoOrigemViewModel> campos = await _campoOrigemAppService.BuscarCampoOrigem(tabela);

                return Ok(campos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarCampoOrigem()
        {
            try
            {
                IEnumerable<CampoOrigemViewModel> campos = await _campoOrigemAppService.ListarCampoOrigem();

                return Ok(campos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}