using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DadosOrigemPainelController : BaseController
    {
        private readonly IDadosOrigemPainelAppService _dadosOrigemAppService;

        public DadosOrigemPainelController(ILogAppService logAppService, IDadosOrigemPainelAppService dadosOrigemAppService) : base(logAppService)
        {
            _dadosOrigemAppService = dadosOrigemAppService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarDadosOrigemPainel()
        {
            try
            {
                IEnumerable<DadosOrigemPainelViewModel> origens = await _dadosOrigemAppService.ListarDadosOrigemPainel();

                return Ok(origens);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}