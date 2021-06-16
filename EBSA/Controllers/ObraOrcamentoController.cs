using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class ObraOrcamentoController : BaseController
    {
        public ObraOrcamentoController(ILogAppService logAppService) : base(logAppService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> ListarDadosOrigemPainel([FromServices] IObraOrcamentoAppService obraOrcamentoAppService)
        {
           IEnumerable<ObraOrcamentoViewModel> obras = await obraOrcamentoAppService.ListarObraOrcamento();

            return Ok(obras);
        }

        [HttpPost]
        public async Task<IActionResult> GravarOrcamentoLivro([FromServices] IObraOrcamentoAppService obraOrcamentoAppService, [FromBody] ObraOrcamentoViewModel obra)
        {
            obra.IdUsuario = Convert.ToInt32(User.Claims.Where(c => c.Type.Equals("Id")).First().Value);
            await obraOrcamentoAppService.GravarOrcamentoLivro(obra);

            return Ok();
        }




    }
}
