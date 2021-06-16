using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoPainelController : BaseController
    {
        private readonly IGrupoPainelAppService _grupoPainelAppService;

        public GrupoPainelController(ILogAppService logAppService, IGrupoPainelAppService grupoPainelAppService) : base(logAppService)
        {
            _grupoPainelAppService = grupoPainelAppService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarGrupoPainel(Guid? StatusId)
        {
            try
            {
                IEnumerable<GrupoPainelViewModel> grupos = await _grupoPainelAppService.ListarGrupoPainel(StatusId);
                return Ok(grupos.OrderBy(a => a.Descricao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}