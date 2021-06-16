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
    public class TabelaOrigemController : BaseController
    {
        private readonly ITabelaOrigemAppService _tabelaOrigemAppService;
        public TabelaOrigemController(ILogAppService logAppService, ITabelaOrigemAppService tabelaOrigemAppService) : base(logAppService)
        {
            _tabelaOrigemAppService = tabelaOrigemAppService;
        }

        [HttpPost]
        public async Task<IActionResult> BuscarTabelaOrigem([FromBody]SistemaOrigemViewModel sistema)
        {
            try
            {
                IEnumerable<TabelaOrigemViewModel> tabelas = await _tabelaOrigemAppService.BuscarTabelaOrigem(sistema);

                return Ok(tabelas);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarTabelaOrigem()
        {
            try
            {
                IEnumerable<TabelaOrigemViewModel> tabelas = await _tabelaOrigemAppService.ListarTabelaOrigem();

                return Ok(tabelas);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}