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
    public class SistemaOrigemController : BaseController
    {
        private readonly ISistemaOrigemAppService _sistemaOrigemAppService;
        public SistemaOrigemController(ILogAppService logAppService, ISistemaOrigemAppService sistemaOrigemAppService): base(logAppService)
        {
            _sistemaOrigemAppService = sistemaOrigemAppService;
        }

        [HttpGet]
        public async Task<IActionResult> LisarSistemOrigem()
        {
            try
            {
                IEnumerable<SistemaOrigemViewModel> sistemas = await _sistemaOrigemAppService.ListarSistemaOrigem();

                return Ok(sistemas);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}