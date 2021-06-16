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
    public class PainelMetaRealController : BaseController
    {
        private readonly IPainelMetaRealAppService _painelMetaRealAppService;

        public PainelMetaRealController(ILogAppService logAppService, IPainelMetaRealAppService painelMetaRealAppService) : base(logAppService)
        {
            _painelMetaRealAppService = painelMetaRealAppService;
        }

        [HttpGet("Consultar")]
        public async Task<IActionResult> ConsultaPainelMetaReal(Guid? MetaReallId, Guid? UsuarioId, Guid? LinhaMetaId)
        {
            try
            {
                IEnumerable<PainelMetaRealViewModel> metareal = await _painelMetaRealAppService.ConsultaPainelMetaReal(MetaReallId, UsuarioId, LinhaMetaId);

                return Ok(metareal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}