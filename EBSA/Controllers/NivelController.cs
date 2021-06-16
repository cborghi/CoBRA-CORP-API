using CoBRA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    public class NivelController : BaseController
    {
        public readonly INivelAppService _nivelAppService;

        public NivelController(INivelAppService nivelAppService, ILogAppService logAppService) : base(logAppService)
        {
            _nivelAppService = nivelAppService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            try
            {
                var listaNiveis = _nivelAppService.Obter();

                return Ok(listaNiveis);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}