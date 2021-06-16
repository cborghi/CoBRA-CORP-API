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
    public class UnidadeMedidaController : BaseController
    {
        private readonly IUnidadeMedidaAppService _unidadeMedidaAppService;

        public UnidadeMedidaController(ILogAppService logAppService, IUnidadeMedidaAppService unidadeMedidaAppService): base(logAppService)
        {
            _unidadeMedidaAppService = unidadeMedidaAppService;
        }

        [HttpGet]
        public async Task<IActionResult> LisarUnidademedida()
        {
            try
            {
                IEnumerable<UnidadeMedidaViewModel> unidades = await _unidadeMedidaAppService.ListarUnidadeMedida();

                return Ok(unidades);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}