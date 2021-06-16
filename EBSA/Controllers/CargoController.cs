using CoBRA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    public class CargoController : BaseController
    {
        public readonly ICargoAppService _cargoAppService;

        public CargoController(ICargoAppService cargoAppService, ILogAppService logAppService) : base(logAppService)
        {
            _cargoAppService = cargoAppService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar(Guid idGrupo)
        {
            try
            {
                var listaCargos = _cargoAppService.ObterPorGrupo(idGrupo);
                return Ok(listaCargos.OrderBy(a => a.Descricao));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("Obter")]
        public IActionResult Obter()
        {
            try
            {
                var listaCargos = _cargoAppService.Obter();
                return Ok(listaCargos);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500);
            }
        }
    }
}