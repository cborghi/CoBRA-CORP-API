using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/[controller]")]
    public class RegiaoConsultorController : BaseController
    {
        private readonly IRegiaoConsultorAppService _regiaoConsultorAppService;

        public RegiaoConsultorController(ILogAppService logAppService, IRegiaoConsultorAppService regiaoConsultorAppService) : base(logAppService)
        {
            _regiaoConsultorAppService = regiaoConsultorAppService;
        }


        [HttpGet("ListaRegiaoConsultor/{idUsuarioRM}")]
        public async Task<IActionResult> ListarRegiaoConsultor(Guid idUsuarioRM)
        {
            try
            {
                IEnumerable<RegiaoConsultorViewModel> regioes =
                    await _regiaoConsultorAppService.ListarRegiaoConsutor(idUsuarioRM);

                return Ok(regioes);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("ListaRegiaoConsultor")]
        public async Task<IActionResult> ListarRegiaoConsultor()
        {
            try
            {
                IEnumerable<RegiaoConsultorViewModel> regioes =
                    await _regiaoConsultorAppService.ListarRegiaoConsutor();

                return Ok(regioes);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}