using CoBRA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    public class ColaboradorController : BaseController
    {
        public readonly IColaboradorAppService _colaboradorAppService;

        public ColaboradorController(IColaboradorAppService colaboradorAppService, ILogAppService logAppService) : base(logAppService)
        {
            _colaboradorAppService = colaboradorAppService;
        }

        [HttpGet("Listar")]
        public IActionResult Listar(Guid idCargo)
        {
            try
            {
                var listaColaboradores = _colaboradorAppService.ObterPorCargo(idCargo);
                return Ok(listaColaboradores.OrderBy(a => a.Nome));
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("ListarFuncionarios")]
        public async Task<IActionResult> ListarFuncionarios(Guid idUsuario, Guid? idRegiao)
        {
            try
            {
                var listaColaboradores = await _colaboradorAppService.ObterPorSupervisor(idUsuario, idRegiao);
                return Ok(listaColaboradores);
            }
            catch (Exception ex)
            {
                GravarLog(ex);
                return StatusCode(500);
            }
        }
    }
}